using System.Collections.Generic;
using System.Threading.Tasks;

using Api.Models;
using Api.Services.Interfaces;
using Api.Repository.Interfaces;
using System;

namespace Api.Services
{
    public class SenhasService : ISenhasService
    {
        private readonly ISenhasRepository _repository;


        public SenhasService(ISenhasRepository repository)
        {
            _repository = repository;

        }

        public async Task<IEnumerable<TblSenhas>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TblSenhas> GetByIdPessoa(int idPessoa)
        {
            return await _repository.GetByIdPessoa(idPessoa);
        }

        public async Task<TblSenhas> GetBySenha(int idPessoa, string senha)
        {
            return await _repository.GetBySenha(idPessoa, senha);
        }

        public async Task<TblSenhas> Create(TblSenhas senha)
        {
            TblSenhas senhaAtual = await _repository.GetByIdPessoa(senha.NIdPessoa);

            if (senhaAtual != null)
            {
                senhaAtual.DFim = DateTime.Now;
                await _repository.Update(senhaAtual);
            }

            senha.DInicio = DateTime.Now;
            senha.DAlteracao = DateTime.Now;

            return await _repository.Create(senha);
        }

        public async Task<TblSenhas> Update(TblSenhas senha)
        {
            senha.DAlteracao = DateTime.Now;

            return await _repository.Update(senha);
        }

        public async Task<TblSenhas> Delete(int idSenha)
        {
            TblSenhas senha = await _repository.GetByIdPessoa(idSenha);

            senha.DFim = DateTime.Now;

            return await _repository.Update(senha);
        }
    }
}
