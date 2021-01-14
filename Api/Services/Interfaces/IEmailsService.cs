using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IEmailsService
    {
        Task<IEnumerable<TblEmails>> GetAll();
        Task<TblEmails> GetById(int idEmail);
        Task<TblEmails> GetByEmail(string email);
        Task<IEnumerable<TblEmails>> GetAllByIdPessoa(int idPessoa);
        Task<TblEmails> Create(TblEmails email);
        Task<TblEmails> Update(TblEmails email);
        Task<TblEmails> Delete(int idEmail);
    }
}
