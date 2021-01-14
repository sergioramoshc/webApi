using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface ISenhasRepository
    {
        Task<IEnumerable<TblSenhas>> GetAll();
        Task<TblSenhas> GetByIdPessoa(int idPessoa);
        Task<TblSenhas> GetBySenha(int idPessoa, string senha);
        Task<TblSenhas> Create(TblSenhas senha);
        Task<TblSenhas> Update(TblSenhas senha);
    }
}
