using Api.Security;
using Api.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Api.Services
{
    public class TokenService : ITokenService
    {
        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private IDistributedCache _cache;

        public TokenService(
            SigningConfiguration signingConfiguration,
            TokenConfiguration tokenConfiguration,
            IDistributedCache cache)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _cache = cache;
        }

        public object CreateToken(int idPessoa)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(idPessoa.ToString(), "Login"),
                    new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.Sid, idPessoa.ToString())
                    }
                );

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

            // Calcula o tempo máximo de validade do refresh token
            // (o mesmo será invalidado automaticamente pelo Redis)
            TimeSpan finalExpiration = TimeSpan.FromSeconds(_tokenConfiguration.FinalExpiration);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);

            return SuccessObject(createDate, expirationDate, token, idPessoa, finalExpiration);
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, int idUsuario, TimeSpan finalExpiration)
        {
            var resultado = new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                refreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                message = "OK"
            };

            // Armazena o refresh token em cache através do Redis 
            var refreshTokenData = new RefreshTokenModel();
            refreshTokenData.RefreshToken = resultado.refreshToken;
            refreshTokenData.UserID = idUsuario;

            DistributedCacheEntryOptions opcoesCache =
                new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(finalExpiration);

            _cache.SetString(resultado.refreshToken,
                JsonConvert.SerializeObject(refreshTokenData),
                opcoesCache);

            return resultado;
        }

        public RefreshTokenModel GetRefreshToken(string refreshToken)
        {
            RefreshTokenModel refreshTokenBase = null;

            string strTokenArmazenado =
                _cache.GetString(refreshToken);

            if (!string.IsNullOrWhiteSpace(strTokenArmazenado))
            {
                refreshTokenBase = JsonConvert
                    .DeserializeObject<RefreshTokenModel>(strTokenArmazenado);
            }

            return refreshTokenBase;
        }

        public bool DeleteToken(string refreshToken, int idPessoa)
        {
            RefreshTokenModel refreshTokenBase = GetRefreshToken(refreshToken);

            bool credencialValida = (refreshTokenBase != null &&
                idPessoa == refreshTokenBase.UserID &&
                refreshToken == refreshTokenBase.RefreshToken);

            // Elimina o token de refresh já que um novo será gerado
            if (credencialValida)
            {
                _cache.Remove(refreshToken);
            }

            return credencialValida;
        }
    }
}
