using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Api.Models;
using Api.Services.Interfaces;
using Api.Repository.Interfaces;

namespace Api.Services
{
    public class DocumentosService : IDocumentosService
    {
        private readonly IDocumentosRepository _repository;

        public DocumentosService(IDocumentosRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TblDocumentos>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TblDocumentos> GetById(int idDocumento)
        {
            return await _repository.GetById(idDocumento);
        }

        public async Task<TblDocumentos> GetByDocumento(string documento)
        {
            return await _repository.GetByDocumento(documento);
        }

        public async Task<IEnumerable<TblDocumentos>> GetAllByIdPessoa(int idPessoa)
        {
            return await _repository.GetAllByIdPessoa(idPessoa);

        }

        public async Task<TblDocumentos> Create(TblDocumentos documento)
        {
            documento.DInicio = DateTime.Now;

            return await _repository.Create(documento);
        }

        public async Task<TblDocumentos> Update(TblDocumentos documento)
        {
            documento.DAlteracao = DateTime.Now;

            return await _repository.Update(documento);
        }

        public async Task<TblDocumentos> Delete(int idDocumento)
        {
            TblDocumentos documento = await _repository.GetById(idDocumento);

            documento.DFim = DateTime.Now;

            return await _repository.Update(documento);
        }
    }
}
