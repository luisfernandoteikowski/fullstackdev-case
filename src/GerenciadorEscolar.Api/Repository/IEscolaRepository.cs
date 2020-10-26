using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorEscolar.Entity;

namespace GerenciadorEscolar.Api.Repository
{
    public interface IEscolaRepository
    {
        Task Inserir(Escola escola);
        Task Atualizar(Escola escola);
        Task Excluir(Escola escola);
        Task<Escola> PesquisarPorId(Guid id);
        Task<Escola> PesquisarPorNome(string nome);
        Task<List<Escola>> ListarTodos();
    }
}