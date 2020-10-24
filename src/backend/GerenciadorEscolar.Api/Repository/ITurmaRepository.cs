using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorEscolar.Entity;

namespace GerenciadorEscolar.Api.Repository
{
    public interface ITurmaRepository
    {
        Task Inserir(Turma turma);
        Task Atualizar(Turma turma);
        Task Excluir(Turma turma);
        Task<Turma> PesquisarPorId(Guid id);
        Task<List<Turma>> ListarTodosPorEscola(Guid escolaId);
        Task<Turma> PesquisarPorEscolaAnoCursoSerieNomeTurma(Guid escolaId, int ano, string curso, string serie, string nome);
    }
}