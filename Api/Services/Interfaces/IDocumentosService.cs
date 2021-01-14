using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IDocumentosService
    {
        Task<IEnumerable<TblDocumentos>> GetAll();
        Task<TblDocumentos> GetById(int idDocumento);
        Task<TblDocumentos> GetByDocumento(string documento);
        Task<IEnumerable<TblDocumentos>> GetAllByIdPessoa(int idPessoa);
        Task<TblDocumentos> Create(TblDocumentos documento);
        Task<TblDocumentos> Update(TblDocumentos documento);
        Task<TblDocumentos> Delete(int idDocumento);
    }
}
