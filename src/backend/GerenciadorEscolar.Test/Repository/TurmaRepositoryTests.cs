using System;
using Xunit;
using System.Threading.Tasks;
using GerenciadorEscolar.Entity;
using GerenciadorEscolar.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEscolar.Test.Repository
{
    public class TurmaRepositoryTests : TestComSqlite
    {
        private readonly Escola _escola;
        private readonly Turma _primeira;
        private readonly Turma _segunda;
        private readonly Turma[] _turmas;


        private ITurmaRepository repository;

        public TurmaRepositoryTests()
        {
            _escola = new Escola() { Id = Guid.NewGuid(), Nome = "EMEF Santa Barbara", NumeroInep = 4789 };
            _primeira = new Turma() { Id = Guid.NewGuid(), EscolaId = _escola.Id, Ano = 2020, Curso = "Educação Infantil", Serie = "Berçario I", Nome = "B1" };
            _segunda = new Turma() { Id = Guid.NewGuid(), EscolaId = _escola.Id, Ano = 2020, Curso = "Ensino Fundamental de 9 Anos", Serie = "8° ANO", Nome = "8A" };

            _turmas = new[] { _primeira, _segunda };
            repository = new TurmaRepository(Context);

            Context.Escola.AddAsync(_escola);
            Context.Turma.AddRangeAsync(_turmas);
            Context.SaveChangesAsync();
        }

        [Fact]
        public async Task PesquisarPorIdDeveRetornarTurmaCorreta()
        {

            //Act
            var itemAtual = await repository.PesquisarPorId(_primeira.Id);

            //Assert
            Assert.Equal(_primeira.Id, itemAtual.Id);
            Assert.Equal(_primeira.EscolaId, itemAtual.EscolaId);
            Assert.Equal(_primeira.Ano, itemAtual.Ano);
            Assert.Equal(_primeira.Curso, itemAtual.Curso);
            Assert.Equal(_primeira.Serie, itemAtual.Serie);
            Assert.Equal(_primeira.Nome, itemAtual.Nome);
        }


        [Fact]
        public async Task ListarTodosDeveRetornarTodasTurmasDaEscola()
        {
            //Act
            var turmasBd = await repository.ListarTodosPorEscola(_escola.Id);

            //Assert
            Assert.Contains(turmasBd, x => x.Id.Equals(_primeira.Id));
            Assert.Contains(turmasBd, x => x.Id.Equals(_segunda.Id));
            Assert.Equal(_turmas.Length, turmasBd.Count);
        }

        [Fact]
        public async Task DeveInserirUmaTurma()
        {

            //Arrange
            var terceira = new Turma()
            {
                EscolaId = _escola.Id,
                Ano = 2021,
                Curso = "Ensino Fundamental",
                Serie = "6° ANO",
                Nome = "6A"
            };
            Guid id;

            //Act
            await repository.Inserir(terceira);
            id = terceira.Id;

            //Assert
            var dbEscola = await repository.PesquisarPorId(id);
            Assert.Equal(terceira.Id, dbEscola.Id);
            Assert.Equal(terceira.EscolaId, dbEscola.EscolaId);
            Assert.Equal(terceira.Ano, dbEscola.Ano);
            Assert.Equal(terceira.Curso, dbEscola.Curso);
            Assert.Equal(terceira.Serie, dbEscola.Serie);
            Assert.Equal(terceira.Nome, dbEscola.Nome);
            Assert.Equal(3, repository.ListarTodosPorEscola(_escola.Id).Result.Count);
        }

        [Theory]
        [InlineData(2021, "ENSINO FUNDAMENTAL DE 9 ANO ANOS FINAIS", "8 ANO", "OITAVO A")]
        [InlineData(2019, "Ensino Fundamental de 9 anos", "8° ano", "8a")]
        [InlineData(2020, "E. Fundamental", "8", "8AA")]
        public async Task DeveAtualizarUmaTurma(int anoAtual, string cursoAtual, string serieAtual, string nomeAtual)
        {
            //Arrange
            var turmaParaAtualizar = await repository.PesquisarPorId(_segunda.Id);
            turmaParaAtualizar.Ano = anoAtual;
            turmaParaAtualizar.Curso = cursoAtual;
            turmaParaAtualizar.Serie = serieAtual;
            turmaParaAtualizar.Nome = nomeAtual;

            //Act
            await repository.Atualizar(turmaParaAtualizar);

            //Assert
            var turmaAtualizada = await repository.PesquisarPorId(_segunda.Id);
            Assert.Equal(anoAtual, turmaAtualizada.Ano);
            Assert.Equal(cursoAtual, turmaAtualizada.Curso);
            Assert.Equal(serieAtual, turmaAtualizada.Serie);
            Assert.Equal(nomeAtual, turmaAtualizada.Nome);
        }


        [Fact]
        public async Task AtualizarTurmaComCursoNuloDeveLancarException()
        {
            //Arrange
            var turmaParaAtualizar = await repository.PesquisarPorId(_primeira.Id);
            turmaParaAtualizar.Curso = null;

            //Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => repository.Atualizar(turmaParaAtualizar));
        }

        [Fact]
        public async Task AtualizarTurmaComSerieNuloDeveLancarException()
        {
            //Arrange
            var turmaParaAtualizar = await repository.PesquisarPorId(_primeira.Id);
            turmaParaAtualizar.Serie = null;

            //Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => repository.Atualizar(turmaParaAtualizar));
        }

        [Fact]
        public async Task AtualizarTurmaComNomeNuloDeveLancarException()
        {
            //Arrange
            var turmaParaAtualizar = await repository.PesquisarPorId(_primeira.Id);
            turmaParaAtualizar.Nome = null;

            //Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => repository.Atualizar(turmaParaAtualizar));
        }

        [Fact]
        public async Task DeveExcluirUmaTurma()
        {
            //Act
            await repository.Excluir(_primeira);
            var turmasBd = await repository.ListarTodosPorEscola(_escola.Id);

            //Assert
            Assert.DoesNotContain(turmasBd, x => x.Id.Equals(_primeira.Id));
        }
    }
}