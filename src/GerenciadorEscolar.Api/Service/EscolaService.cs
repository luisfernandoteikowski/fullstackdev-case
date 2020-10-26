
using System;
using GerenciadorEscolar.Entity;
using System.Threading.Tasks;
using GerenciadorEscolar.Dto;
using System.Collections.Generic;
using GerenciadorEscolar.Api.Repository;
using GerenciadorEscolar.Api.Exceptions;
using System.Linq;

namespace GerenciadorEscolar.Api.Service
{
    public class EscolaService : IEscolaService
    {
        public IEscolaRepository EscolaRepository { get; }

        public EscolaService(IEscolaRepository escolaRepository)
        {
            EscolaRepository = escolaRepository;
        }

        public async Task<Escola> Inserir(EscolaDto escola)
        {
            if (await EscolaRepository.PesquisarPorNome(escola.Nome) != null)
            {
                throw new EscolaJaExisteException();
            }
            var novaEscola = new Escola()
            {
                Nome = escola.Nome,
                NumeroInep = escola.NumeroInep
            };
            await EscolaRepository.Inserir(novaEscola);
            return novaEscola;
        }

        public async Task Atualizar(EscolaDto escola)
        {
            var escolaExistente = await EscolaRepository.PesquisarPorNome(escola.Nome);
            if (escolaExistente != null && escolaExistente.Id != escola.Id)
            {
                throw new EscolaJaExisteException();
            }
            var escolaAtualizar = await EscolaRepository.PesquisarPorId(escola.Id);
            escolaAtualizar.Nome = escola.Nome;
            escolaAtualizar.NumeroInep = escola.NumeroInep;
            await EscolaRepository.Atualizar(escolaAtualizar);
        }

        public async Task Excluir(Guid id)
        {
            var escola = await EscolaRepository.PesquisarPorId(id);
            if (escola == null)
            {
                throw new EscolaNaoEncontradaException();
            }
            await EscolaRepository.Excluir(escola);
        }

        public async Task<EscolaDto> PesquisarPorId(Guid id)
        {

            var escola = await EscolaRepository.PesquisarPorId(id);
            return EscolaDto.deEscola(escola);

        }

        public async Task<List<EscolaDto>> ListarTodos()
        {
            return (await EscolaRepository.ListarTodos())
                .Select(x => EscolaDto.deEscola(x))
                .ToList();
        }
    }
}