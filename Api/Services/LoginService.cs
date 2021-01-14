using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Api.Models;
using Api.Services.Interfaces;
using Api.Security;

namespace Api.Services
{
    public class LoginService : ILoginService
    {
        private IPessoasService _pessoasService;
        private IDocumentosService _documentosService;
        private IEmailsService _emailsService;
        private ISenhasService _senhasService;
        private ITokenService _tokenService;

        public LoginService(IPessoasService pessoasService,
            IDocumentosService documentosService,
            IEmailsService emailsService,
            ISenhasService senhasService,
            ITokenService tokenService)
        {
            _pessoasService = pessoasService;
            _documentosService = documentosService;
            _emailsService = emailsService;
            _senhasService = senhasService;
            _tokenService = tokenService;
        }

        public async Task<object> Login(CredentialsModel credentials)
        {
            // Variável auxiliar
            TblPessoas baseUser = new TblPessoas();

            // Altera o fluxo dependendo a validação solicitada
            if (credentials.GrantType == "password")
            {
                // Verifica se o usuário ou senha são nulos
                if (!string.IsNullOrWhiteSpace(credentials.User) && !string.IsNullOrWhiteSpace(credentials.Password))
                {
                    // Variáveis auxiliares
                    bool userExiste = false;
                    int idPessoa = 0;

                    // Determina se é email ou cpf e busca a existência do dado
                    if (credentials.User.All(char.IsDigit))
                    {
                        TblDocumentos documento = await _documentosService.GetByDocumento(credentials.User);
                        if (documento != null)
                        {
                            userExiste = true;
                            idPessoa = documento.NIdPessoa;
                        }
                    }
                    else
                    {
                        TblEmails email = await _emailsService.GetByEmail(credentials.User);
                        if (email != null)
                        {
                            userExiste = true;
                            idPessoa = email.NIdPessoa;
                        }
                    }

                    if (userExiste)
                    {
                        // Converte a senha em base64
                        var plainTextBytes = Encoding.UTF8.GetBytes(credentials.Password);
                        string encodedText = Convert.ToBase64String(plainTextBytes);
                        credentials.Password = encodedText;

                        // Verifica a existência
                        bool senhaExiste = (await _senhasService.GetBySenha(idPessoa, credentials.Password) != null);

                        if (senhaExiste && idPessoa > 0)
                        {
                            baseUser = await _pessoasService.GetById(idPessoa);
                        }
                        else
                        {
                            throw new ArgumentException("Usuário e/ou senha incorreto(s).", "mensagem");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Usuário não existe.", "mensagem");
                    }
                }
                else
                {
                    throw new ArgumentException("Usuário e/ou senha incorreto(s).", "mensagem");
                }
            }
            else if (credentials.GrantType == "refresh_token")
            {
                // Verifica se o usuário ou refresh token são nulos
                if (!string.IsNullOrWhiteSpace(credentials.User) && !string.IsNullOrWhiteSpace(credentials.RefreshToken))
                {
                    bool tokenValido = false;
                    // Busca a pessoa relacionada ao token (User = ID)
                    baseUser = await _pessoasService.GetById(Convert.ToInt32(credentials.User));

                    tokenValido = _tokenService.DeleteToken(credentials.RefreshToken, baseUser.NIdPessoa);

                    if (!tokenValido)
                    {
                        throw new ArgumentException("Usuário e/ou token inválido(s).", "mensagem");
                    }
                }
                else
                {
                    throw new ArgumentException("Usuário e/ou token inválido(s).", "mensagem");
                }
            }

            return _tokenService.CreateToken(baseUser.NIdPessoa);
        }

        public async Task<object> NovaSenha(QpNovaSenha parametros)
        {
            TblDocumentos documento = await _documentosService.GetByDocumento(parametros.Documento);

            if (documento != null)
            {
                TblPessoas pessoa = await _pessoasService.GetById(documento.NIdPessoa);

                if (pessoa.DNascimento == parametros.DataNascimento)
                {
                    var plainTextBytes = Encoding.UTF8.GetBytes(parametros.Senha);
                    string encodedText = Convert.ToBase64String(plainTextBytes);
                    string senhaCriptografada = encodedText;

                    TblSenhas senha = new TblSenhas { NIdPessoa = pessoa.NIdPessoa, SSenha = senhaCriptografada };

                    try
                    {
                        bool senhaCriada = (await _senhasService.Create(senha) != null);

                        if (senhaCriada)
                        {
                            return _tokenService.CreateToken(pessoa.NIdPessoa);
                        }
                        else
                        {
                            throw new ArgumentException("Falha ao criar senha.", "mensagem");
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new ArgumentException(exception.Message, "mensagem");
                    }
                }
                else
                {
                    throw new ArgumentException("Data de nascimento inválida.", "mensagem");
                }
            }
            else
            {
                throw new ArgumentException("Documento não localizado.", "mensagem");
            }

        }
    }
}
