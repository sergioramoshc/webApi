using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using Api.Repository.Interfaces;

namespace Api.Repository
{
    public class SenhasRepository : ISenhasRepository
    {
        private readonly DbProdContext _context;

        public SenhasRepository(DbProdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblSenhas>> GetAll()
        {
            return await _context.TblSenhas
                .Include(senha => senha.NIdPessoaNavigation)
                .Where(x => !x.DFim.HasValue)
                .ToListAsync();
        }

        public async Task<TblSenhas> GetByIdPessoa(int idPessoa)
        {
            return await _context.TblSenhas
                .Include(senha => senha.NIdPessoaNavigation)
                .SingleOrDefaultAsync(x => x.NIdPessoa == idPessoa && !x.DFim.HasValue);
        }

        public async Task<TblSenhas> GetBySenha(int idPessoa, string senha)
        {
            // IdPessoa previne que multiplas senhas retornem nessa consulta
            return await _context.TblSenhas
                .Include(senha => senha.NIdPessoaNavigation)
                .SingleOrDefaultAsync(x =>
                x.SSenha == senha &&
                x.NIdPessoa == idPessoa &&
                !x.DFim.HasValue);
        }

        public async Task<TblSenhas> Create(TblSenhas senha)
        {
            _context.TblSenhas.Add(senha);

            try
            {
                await _context.SaveChangesAsync();
                return senha;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<TblSenhas> Update(TblSenhas senha)
        {
            _context.Entry(senha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return senha;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
