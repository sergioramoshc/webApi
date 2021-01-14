using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Api.Models;
using Api.Services.Interfaces;
using Api.Repository.Interfaces;

namespace Api.Services
{
    public class PessoasService : IPessoasService
    {
        private readonly IPessoasRepository _repository;


        public PessoasService(IPessoasRepository repository)
        {
            _repository = repository;

        }

        public async Task<IEnumerable<TblPessoas>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TblPessoas> GetById(int idPessoa)
        {
            return await _repository.GetById(idPessoa);
        }

        public async Task<TblPessoas> Create(TblPessoas pessoa)
        {
            pessoa.DInicio = DateTime.Now;

            return await _repository.Create(pessoa);
        }

        public async Task<TblPessoas> Update(TblPessoas pessoa)
        {
            pessoa.DAlteracao = DateTime.Now;

            return await _repository.Update(pessoa);
        }

        public async Task<TblPessoas> Delete(int idPessoa)
        {
            TblPessoas pessoa = await _repository.GetById(idPessoa);

            return await _repository.Update(pessoa);
        }
    }
}
