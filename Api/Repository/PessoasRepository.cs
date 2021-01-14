using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using Api.Repository.Interfaces;

namespace Api.Repository
{
    public class PessoasRepository : IPessoasRepository
    {
        private readonly DbProdContext _context;

        public PessoasRepository(DbProdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblPessoas>> GetAll()
        {
            return await _context.TblPessoas
                .Include(pessoa => pessoa.TblEmailsNIdPessoaNavigation)
                .Include(pessoa => pessoa.TblDocumentosNIdPessoaNavigation)
                .Where(x => !x.DFim.HasValue)
                .ToListAsync();
        }

        public async Task<TblPessoas> GetById(int id)
        {
            return await _context.TblPessoas
            .Include(pessoa => pessoa.TblEmailsNIdPessoaNavigation)
            .Include(pessoa => pessoa.TblDocumentosNIdPessoaNavigation)
            .SingleOrDefaultAsync(x => x.NIdPessoa == id && !x.DFim.HasValue);
        }

        public async Task<TblPessoas> Create(TblPessoas pessoa)
        {
            _context.TblPessoas.Add(pessoa);

            try
            {
                await _context.SaveChangesAsync();
                return pessoa;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<TblPessoas> Update(TblPessoas pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return pessoa;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
