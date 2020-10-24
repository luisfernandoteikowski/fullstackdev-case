using System;
using GerenciadorEscolar.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GerenciadorEscolar.Api.Repository
{
    public class EscolaRepository : IEscolaRepository
    {
        public GerenciadorEscolarDbContext DbContext { get; set; }

        public EscolaRepository(GerenciadorEscolarDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task Inserir(Escola escola)
        {
            escola.Id = Guid.NewGuid();
            await DbContext.Escola.AddAsync(escola);
            await DbContext.SaveChangesAsync();
        }

        public async Task Atualizar(Escola escola)
        {
            DbContext.Entry(escola).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task Excluir(Escola escola)
        {
            DbContext.Entry(escola).State = EntityState.Deleted;
            await DbContext.SaveChangesAsync();
        }

        public async Task<Escola> PesquisarPorId(Guid id)
        {
            return await DbContext.Escola.FindAsync(id);
        }

        public async Task<Escola> PesquisarPorNome(string nome)
        {
            return await DbContext.Escola.Where(x => x.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<List<Escola>> ListarTodos()
        {
            return await DbContext.Escola
                .Include(x => x.Turmas)
                .ToListAsync();
        }
    }
}