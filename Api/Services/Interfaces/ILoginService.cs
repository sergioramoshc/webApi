using Api.Models;
using Api.Security;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface ILoginService
    {
        Task<object> Login(CredentialsModel credentials);
        Task<object> NovaSenha(QpNovaSenha parametros);
    }
}
