using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IDocumentosRepository
    {
        Task<IEnumerable<TblDocumentos>> GetAll();
        Task<TblDocumentos> GetById(int idDocumento);
        Task<IEnumerable<TblDocumentos>> GetAllByIdPessoa(int idPessoa);
        Task<TblDocumentos> GetByDocumento(string documento);
        Task<TblDocumentos> Create(TblDocumentos documento);
        Task<TblDocumentos> Update(TblDocumentos documento);
    }
}
