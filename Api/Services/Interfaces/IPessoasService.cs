using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IPessoasService
    {
        Task<IEnumerable<TblPessoas>> GetAll();
        Task<TblPessoas> GetById(int idPessoa);
        Task<TblPessoas> Create(TblPessoas pessoa);
        Task<TblPessoas> Update(TblPessoas pessoa);
        Task<TblPessoas> Delete(int idPessoa);
    }
}
