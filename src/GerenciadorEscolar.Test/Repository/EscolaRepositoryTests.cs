using System;
using Xunit;
using System.Threading.Tasks;
using GerenciadorEscolar.Entity;
using GerenciadorEscolar.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEscolar.Test.Repository
{
    public class EscolaRepositoryTests : TestComSqlite
    {
        private readonly Escola _primeira;
        private readonly Escola _segunda;
        private readonly Escola[] _escolas;
        private IEscolaRepository repository;
        public EscolaRepositoryTests()
        {
            _primeira = new Escola() { Id = Guid.NewGuid(), Nome = "EMEF Santa Barbara", NumeroInep = 4789 };
            _segunda = new Escola() { Id = Guid.NewGuid(), Nome = "EMEF João Pedro", NumeroInep = 3199 };

            _escolas = new[] { _primeira, _segunda };
            Context.Escola.AddRange(_escolas);
            Context.SaveChanges();

            repository = new EscolaRepository(Context);
        }

        [Fact]
        public async Task PesquisarPorIdDeveRetornarEscolaCorreta()
        {
            //Act
            var itemAtual = await repository.PesquisarPorId(_primeira.Id);

            //Assert
            Assert.Equal(_primeira.Id, itemAtual.Id);
        }

        [Fact]
        public async Task PesquisarPorNomeDeveRetornarEscolaCorreta()
        {
            //Act
            var itemAtual = await repository.PesquisarPorNome(_segunda.Nome);

            //Assert
            Assert.Equal(_segunda.Id, itemAtual.Id);
            Assert.Equal(_segunda.Nome, itemAtual.Nome);
        }


        [Fact]
        public async Task ListarTodosDeveRetornarTodasEscolas()
        {
            //Act
            var escolasBd = await repository.ListarTodos();

            //Assert
            Assert.Contains(escolasBd, x => x.Id.Equals(_primeira.Id));
            Assert.Contains(escolasBd, x => x.Id.Equals(_segunda.Id));
            Assert.Equal(_escolas.Length, escolasBd.Count);
        }


        [Fact]
        public async Task DeveInserirUmaEscola()
        {
            //Arrange
            var terceira = new Escola()
            {
                Nome = "EMEF Paulo Anchieta",
                NumeroInep = 18456
            };
            Guid id;

            //Act
            await repository.Inserir(terceira);
            id = terceira.Id;

            //Assert
            var dbEscola = await repository.PesquisarPorId(id);
            Assert.Equal(terceira.Nome, dbEscola.Nome);
            Assert.Equal(terceira.NumeroInep, dbEscola.NumeroInep);
            Assert.Equal(3, repository.ListarTodos().Result.Count);
        }

        [Fact]
        public async Task InserirEscolaComNomeNuloDeveLancarException()
        {
            //Arrange
            var terceira = new Escola();

            //Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => repository.Inserir(terceira));
        }

        [Theory]
        [InlineData("escola municipal santa barbara")]
        [InlineData("ESCOLA MUNICIPAL SANTA BARBARA")]
        [InlineData("E.M.E.F santa barbara")]
        public async Task DeveAtualizarUmaEscola(string nomeAtual)
        {
            //Arrange
            var escolaParaAtualizar = await repository.PesquisarPorId(_primeira.Id);
            escolaParaAtualizar.Nome = nomeAtual;

            //Act
            await repository.Atualizar(escolaParaAtualizar);

            //Assert
            var escolaAtualizada = await repository.PesquisarPorId(_primeira.Id);
            Assert.Equal(nomeAtual, escolaAtualizada.Nome);
        }

        [Fact]
        public async Task AtualizarEscolaComNomeNuloDeveLancarException()
        {
            //Arrange
            var escolaParaAtualizar = await repository.PesquisarPorId(_primeira.Id);
            escolaParaAtualizar.Nome = null;

            //Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => repository.Atualizar(escolaParaAtualizar));
        }


        [Fact]
        public async Task DeveExcluirUmaEscola()
        {
            //Act
            await repository.Excluir(_primeira);
            var escolasBd = await repository.ListarTodos();

            //Assert
            Assert.DoesNotContain(escolasBd, x => x.Id.Equals(_primeira.Id));
        }
    }
}