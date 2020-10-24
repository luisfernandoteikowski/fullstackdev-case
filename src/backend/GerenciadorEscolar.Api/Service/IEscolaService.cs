using System;
using GerenciadorEscolar.Entity;
using System.Threading.Tasks;
using GerenciadorEscolar.Dto;
using System.Collections.Generic;

namespace GerenciadorEscolar.Api.Service
{
    public interface IEscolaService
    {
        Task<Escola> Inserir(EscolaDto escola);
        Task Atualizar(EscolaDto escola);
        Task Excluir(Guid id);
        Task<EscolaDto> PesquisarPorId(Guid id);
        Task<List<EscolaDto>> ListarTodos();
    }
}