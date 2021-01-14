using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using Api.Repository.Interfaces;

namespace Api.Repository
{
    public class EmailsRepository : IEmailsRepository
    {
        private readonly DbProdContext _context;

        public EmailsRepository(DbProdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblEmails>> GetAll()
        {
            return await _context.TblEmails
                .Include(email => email.NIdPessoaNavigation)
                .Where(x => !x.DFim.HasValue)
                .ToListAsync();
        }

        public async Task<TblEmails> GetById(int idEmail)
        {
            return await _context.TblEmails
                .Include(email => email.NIdPessoaNavigation)
                .SingleOrDefaultAsync(x => x.NIdEmail == idEmail && !x.DFim.HasValue);
        }

        public async Task<IEnumerable<TblEmails>> GetAllByIdPessoa(int idPessoa)
        {
            return await _context.TblEmails
                .Include(email => email.NIdPessoaNavigation)
                .Where(x => x.NIdPessoa == idPessoa && !x.DFim.HasValue)
                .ToListAsync();
        }

        public async Task<TblEmails> GetByEmail(string email)
        {
            return await _context.TblEmails
                .Include(email => email.NIdPessoaNavigation)
                .SingleOrDefaultAsync(x => x.SEmail == email && !x.DFim.HasValue);
        }

        public async Task<TblEmails> Create(TblEmails email)
        {
            _context.TblEmails.Add(email);

            try
            {
                await _context.SaveChangesAsync();
                return email;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<TblEmails> Update(TblEmails email)
        {
            _context.Entry(email).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return email;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
