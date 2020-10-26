using System;
using GerenciadorEscolar.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GerenciadorEscolar.Api.Repository
{
    public class TurmaRepository : ITurmaRepository
    {
        public GerenciadorEscolarDbContext DbContext { get; set; }

        public TurmaRepository(GerenciadorEscolarDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task Inserir(Turma turma)
        {
            turma.Id = Guid.NewGuid();
            await DbContext.Turma.AddAsync(turma);
            await DbContext.SaveChangesAsync();
        }

        public async Task Atualizar(Turma turma)
        {
            DbContext.Entry(turma).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task Excluir(Turma turma)
        {
            DbContext.Entry(turma).State = EntityState.Deleted;
            await DbContext.SaveChangesAsync();
        }

        public async Task<Turma> PesquisarPorId(Guid id)
        {
            return await DbContext
                .Turma
                .Include(x => x.Escola)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Turma>> ListarTodosPorEscola(Guid escolaId)
        {
            return await DbContext
                .Turma
                .Include(x => x.Escola)
                .Where(x => x.EscolaId == escolaId)
                .ToListAsync();
        }

        public async Task<Turma> PesquisarPorEscolaAnoCursoSerieNomeTurma(Guid escolaId, int ano, string curso, string serie, string nome)
        {
            return await DbContext
                .Turma
                .Include(x => x.Escola)
                .Where(x => x.EscolaId == escolaId && x.Ano == ano && x.Curso == curso && x.Serie == serie && x.Nome == nome)
                .FirstOrDefaultAsync();
        }
    }
}