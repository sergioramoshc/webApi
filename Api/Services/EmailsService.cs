using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Api.Models;
using Api.Services.Interfaces;
using Api.Repository.Interfaces;

namespace Api.Services
{
    public class EmailsService : IEmailsService
    {
        private readonly IEmailsRepository _repository;


        public EmailsService(IEmailsRepository repository)
        {
            _repository = repository;

        }


        public async Task<IEnumerable<TblEmails>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TblEmails> GetById(int idEmail)
        {
            return await _repository.GetById(idEmail);
        }

        public async Task<TblEmails> GetByEmail(string email)
        {
            return await _repository.GetByEmail(email);
        }

        public async Task<IEnumerable<TblEmails>> GetAllByIdPessoa(int idPessoa)
        {
            return await _repository.GetAllByIdPessoa(idPessoa);
        }

        public async Task<TblEmails> Create(TblEmails email)
        {
            email.DInicio = DateTime.Now;

            return await _repository.Create(email);
        }

        public async Task<TblEmails> Update(TblEmails email)
        {
            email.DAlteracao = DateTime.Now;

            return await _repository.Update(email);
        }

        public async Task<TblEmails> Delete(int idEmail)
        {
            TblEmails email = await _repository.GetById(idEmail);

            email.DFim = DateTime.Now;

            return await _repository.Update(email);
        }
    }
}
