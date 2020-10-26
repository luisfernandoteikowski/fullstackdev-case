using System;
using GerenciadorEscolar.Entity;
using System.Threading.Tasks;
using GerenciadorEscolar.Dto;
using System.Collections.Generic;

namespace GerenciadorEscolar.Api.Service
{
    public interface ITurmaService
    {
        Task<Turma> Inserir(TurmaDto turma);
        Task Atualizar(TurmaDto turma);
        Task Excluir(Guid id);
        Task<TurmaDto> PesquisarPorId(Guid id);
        Task<List<TurmaDto>> ListarTodosPorEscola(Guid escolaId);
    }
}