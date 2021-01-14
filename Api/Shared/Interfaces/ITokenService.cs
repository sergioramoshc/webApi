using Api.Security;

namespace Api.Services.Interfaces
{
    public interface ITokenService
    {
        object CreateToken(int idPessoa);
        RefreshTokenModel GetRefreshToken(string refreshToken);
        bool DeleteToken(string refreshToken, int idPessoa);
    }
}
