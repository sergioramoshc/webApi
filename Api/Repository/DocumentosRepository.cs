using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using Api.Repository.Interfaces;

namespace Api.Repository
{
    public class DocumentosRepository : IDocumentosRepository
    {
        private readonly DbProdContext _context;

        public DocumentosRepository(DbProdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblDocumentos>> GetAll()
        {
            return await _context.TblDocumentos
                .Include(documento => documento.NIdPessoaNavigation)
                .Where(x => !x.DFim.HasValue)
                .ToListAsync();
        }

        public async Task<TblDocumentos> GetById(int idDocumento)
        {
            return await _context.TblDocumentos
                .Include(documento => documento.NIdPessoaNavigation)
                .SingleOrDefaultAsync(x => x.NIdDocumento == idDocumento && !x.DFim.HasValue);
        }

        public async Task<IEnumerable<TblDocumentos>> GetAllByIdPessoa(int idPessoa)
        {
            return await _context.TblDocumentos
                .Include(documento => documento.NIdPessoaNavigation)
                .Where(x => x.NIdPessoa == idPessoa && !x.DFim.HasValue)
                .ToListAsync();
        }

        public async Task<TblDocumentos> GetByDocumento(string documento)
        {
            return await _context.TblDocumentos
                .Include(documento => documento.NIdPessoaNavigation)
                .SingleOrDefaultAsync(x => x.SDocumento == documento && !x.DFim.HasValue);
        }

        public async Task<TblDocumentos> Create(TblDocumentos documento)
        {
            _context.TblDocumentos.Add(documento);

            try
            {
                await _context.SaveChangesAsync();
                return documento;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<TblDocumentos> Update(TblDocumentos documento)
        {
            _context.Entry(documento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return documento;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
