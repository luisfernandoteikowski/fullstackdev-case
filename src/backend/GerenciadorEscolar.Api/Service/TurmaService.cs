
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
    public class TurmaService : ITurmaService
    {
        public ITurmaRepository TurmaRepository { get; }

        public TurmaService(ITurmaRepository turmaRepository)
        {
            TurmaRepository = turmaRepository;
        }

        public async Task<Turma> Inserir(TurmaDto turma)
        {
            if (await TurmaRepository.PesquisarPorEscolaAnoCursoSerieNomeTurma(turma.EscolaId, turma.Ano, turma.Curso, turma.Serie, turma.Nome) != null)
            {
                throw new TurmaJaExisteException();
            }
            var novaTurma = new Turma()
            {
                EscolaId = turma.EscolaId,
                Ano = turma.Ano,
                Curso = turma.Curso,
                Serie = turma.Serie,
                Nome = turma.Nome,
            };
            await TurmaRepository.Inserir(novaTurma);
            return novaTurma;
        }

        public async Task Atualizar(TurmaDto turma)
        {
            var turmaExistente = await TurmaRepository.PesquisarPorEscolaAnoCursoSerieNomeTurma(turma.EscolaId, turma.Ano, turma.Curso, turma.Serie, turma.Nome);
            if (turmaExistente != null && turmaExistente.Id != turma.Id)
            {
                throw new TurmaJaExisteException();
            }
            var turmaAtualizar = await TurmaRepository.PesquisarPorId(turma.Id);
            turmaAtualizar.EscolaId = turma.EscolaId;
            turmaAtualizar.Ano = turma.Ano;
            turmaAtualizar.Curso = turma.Curso;
            turmaAtualizar.Serie = turma.Serie;
            turmaAtualizar.Nome = turma.Nome;
            await TurmaRepository.Atualizar(turmaAtualizar);
        }

        public async Task Excluir(Guid id)
        {
            var turma = await TurmaRepository.PesquisarPorId(id);
            if (turma == null)
            {
                throw new TurmaNaoEncontradaException();
            }
            await TurmaRepository.Excluir(turma);
        }

        public async Task<TurmaDto> PesquisarPorId(Guid id)
        {
            var turma = await TurmaRepository.PesquisarPorId(id);
            return TurmaDto.deTurma(turma);
        }

        public async Task<List<TurmaDto>> ListarTodosPorEscola(Guid escolaId)
        {
            return (await TurmaRepository.ListarTodosPorEscola(escolaId))
                .Select(x => TurmaDto.deTurma(x))
                .ToList();
        }
    }
}