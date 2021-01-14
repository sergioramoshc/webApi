using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IEmailsRepository
    {
        Task<IEnumerable<TblEmails>> GetAll();
        Task<TblEmails> GetById(int idEmail);
        Task<IEnumerable<TblEmails>> GetAllByIdPessoa(int idPessoa);
        Task<TblEmails> GetByEmail(string email);
        Task<TblEmails> Create(TblEmails email);
        Task<TblEmails> Update(TblEmails email);
    }
}
