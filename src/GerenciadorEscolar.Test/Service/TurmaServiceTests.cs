using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using GerenciadorEscolar.Entity;
using GerenciadorEscolar.Api.Repository;
using GerenciadorEscolar.Dto;
using GerenciadorEscolar.Api.Service;
using GerenciadorEscolar.Api.Exceptions;
using System.Collections.Generic;

namespace GerenciadorEscolar.Test.Service
{
    public class TurmaServiceTests
    {
        [Fact]
        public async Task DeveInvocarInserirTurma()
        {
            //Arrange
            var repository = new Mock<ITurmaRepository>();
            var turmaDto = new TurmaDto()
            {
                EscolaId = Guid.NewGuid(),
                Ano = 2020,
                Curso = "Ensino Fundamental de 9 Anos",
                Serie = "8° ANO",
                Nome = "8A"
            };

            //Act
            var service = new TurmaService(repository.Object);
            await service.Inserir(turmaDto);

            //Assert
            repository.Verify(
                x => x.Inserir(It.Is<Turma>(y => y.EscolaId == turmaDto.EscolaId && y.Ano == turmaDto.Ano && y.Curso == turmaDto.Curso && y.Serie == turmaDto.Serie && y.Nome == turmaDto.Nome)), Times.Once());
        }

        [Fact]
        public async Task DeveLancarExceptionQuandoTurmaJaExisteAoInserir()
        {
            //Arrange
            var repository = new Mock<ITurmaRepository>();
            var turmaDto = new TurmaDto()
            {
                EscolaId = Guid.NewGuid(),
                Ano = 2020,
                Curso = "Ensino Fundamental de 9 Anos",
                Serie = "8° ANO",
                Nome = "8A"
            };
            repository
                .Setup(x => x.PesquisarPorEscolaAnoCursoSerieNomeTurma(turmaDto.EscolaId, turmaDto.Ano, turmaDto.Curso, turmaDto.Serie, turmaDto.Nome))
                .Returns(Task.FromResult(new Turma()
                {
                    EscolaId = turmaDto.EscolaId,
                    Ano = turmaDto.Ano,
                    Curso = turmaDto.Curso,
                    Serie = turmaDto.Serie,
                    Nome = turmaDto.Nome,
                }));

            //Assert
            var service = new TurmaService(repository.Object);
            await Assert.ThrowsAnyAsync<TurmaJaExisteException>(async () => await service.Inserir(turmaDto));
        }

        [Fact]
        public async Task DeveInvocarAtualizarTurma()
        {
            //Arrange
            var repository = new Mock<ITurmaRepository>();
            var turmaDto = new TurmaDto()
            {
                Id = Guid.NewGuid(),
                EscolaId = Guid.NewGuid(),
                Ano = 2020,
                Curso = "Ensino Fundamental de 9 Anos",
                Serie = "8° ANO",
                Nome = "8A"
            };
            repository
               .Setup(x => x.PesquisarPorId(turmaDto.Id))
               .Returns(Task.FromResult(new Turma()
               {
                   Id = turmaDto.Id,
                   EscolaId = turmaDto.EscolaId,
                   Ano = turmaDto.Ano,
                   Curso = turmaDto.Curso,
                   Serie = turmaDto.Serie,
                   Nome = turmaDto.Nome,
               }));

            //Act    
            var service = new TurmaService(repository.Object);
            await service.Atualizar(turmaDto);

            //Assert
            repository.Verify(x => x.Atualizar(It.Is<Turma>(y => y.Id == turmaDto.Id && y.EscolaId == turmaDto.EscolaId && y.Ano == turmaDto.Ano && y.Curso == turmaDto.Curso && y.Serie == turmaDto.Serie && y.Nome == turmaDto.Nome)), Times.Once());
        }

        [Fact]
        public async Task DeveLancarExceptionQuandoTurmaJaExisteAoAtualizar()
        {
            //Arrange
            var repository = new Mock<ITurmaRepository>();
            var turmaDto = new TurmaDto()
            {
                Id = Guid.NewGuid(),
                EscolaId = Guid.NewGuid(),
                Ano = 2020,
                Curso = "Ensino Fundamental de 9 Anos",
                Serie = "8° ANO",
                Nome = "8A"
            };
            repository
                .Setup(x => x.PesquisarPorEscolaAnoCursoSerieNomeTurma(turmaDto.EscolaId, turmaDto.Ano, turmaDto.Curso, turmaDto.Serie, turmaDto.Nome))
                .Returns(Task.FromResult(new Turma()
                {
                    Id = Guid.NewGuid(),
                    EscolaId = turmaDto.EscolaId,
                    Ano = turmaDto.Ano,
                    Curso = turmaDto.Curso,
                    Serie = turmaDto.Serie,
                    Nome = turmaDto.Nome,
                }));

            //Assert
            var service = new TurmaService(repository.Object);
            await Assert.ThrowsAnyAsync<TurmaJaExisteException>(async () => await service.Atualizar(turmaDto));
        }

        [Fact]
        public async Task NaoDeveLancarExceptionQuandoTurmaJaExisteComMesmoIdAoAtualizar()
        {
            //Arrange
            var repository = new Mock<ITurmaRepository>();
            var turmaDto = new TurmaDto()
            {
                Id = Guid.NewGuid(),
                EscolaId = Guid.NewGuid(),
                Ano = 2020,
                Curso = "Ensino Fundamental de 9 Anos",
                Serie = "8° ANO",
                Nome = "8A"
            };
            var turmaRetorno = new Turma()
            {
                Id = turmaDto.Id,
                EscolaId = turmaDto.EscolaId,
                Ano = turmaDto.Ano,
                Curso = turmaDto.Curso,
                Serie = turmaDto.Serie,
                Nome = turmaDto.Nome
            };

            repository
               .Setup(x => x.PesquisarPorEscolaAnoCursoSerieNomeTurma(turmaDto.EscolaId, turmaDto.Ano, turmaDto.Curso, turmaDto.Serie, turmaDto.Nome))
               .Returns(Task.FromResult(turmaRetorno));

            repository
               .Setup(x => x.PesquisarPorId(turmaDto.Id))
               .Returns(Task.FromResult(turmaRetorno));

            //Act    
            var service = new TurmaService(repository.Object);
            await service.Atualizar(turmaDto);

            //Assert
            repository.Verify(x => x.Atualizar(It.Is<Turma>(y => y.Id == turmaDto.Id && y.EscolaId == turmaDto.EscolaId && y.Ano == turmaDto.Ano && y.Curso == turmaDto.Curso && y.Serie == turmaDto.Serie && y.Nome == turmaDto.Nome)), Times.Once());
        }

        [Fact]
        public async Task DeveInvocarExcluirTurma()
        {
            //Arrange
            var repository = new Mock<ITurmaRepository>();
            var turmaDto = new TurmaDto()
            {
                Id = Guid.NewGuid(),
                EscolaId = Guid.NewGuid(),
                Ano = 2020,
                Curso = "Ensino Fundamental de 9 Anos",
                Serie = "8° ANO",
                Nome = "8A"
            };
            repository
               .Setup(x => x.PesquisarPorId(turmaDto.Id))
               .Returns(Task.FromResult(new Turma()
               {
                   Id = turmaDto.Id,
                   EscolaId = turmaDto.EscolaId,
                   Ano = turmaDto.Ano,
                   Curso = turmaDto.Curso,
                   Serie = turmaDto.Serie,
                   Nome = turmaDto.Nome,
               }));

            //Act    
            var service = new TurmaService(repository.Object);
            await service.Excluir(turmaDto.Id);

            //Assert
            repository.Verify(x => x.Excluir(It.Is<Turma>(y => y.Id == turmaDto.Id)), Times.Once());
        }


        [Fact]
        public async Task DeveLancarExceptionQuandoTurmaNaoForEncontradaAoExcluir()
        {
            //Arrange
            var repository = new Mock<ITurmaRepository>();
            var turmaDto = new TurmaDto()
            {
                Id = Guid.NewGuid(),
                EscolaId = Guid.NewGuid(),
                Ano = 2020,
                Curso = "Ensino Fundamental de 9 Anos",
                Serie = "8° ANO",
                Nome = "8A"
            };

            repository
               .Setup(x => x.PesquisarPorId(turmaDto.Id))
               .Returns(Task.FromResult<Turma>(null));

            //Act    
            var service = new TurmaService(repository.Object);

            //Assert
            await Assert.ThrowsAnyAsync<TurmaNaoEncontradaException>(async () => await service.Excluir(turmaDto.Id));
        }

        [Fact]
        public async Task DeveInvocarListarTodosPorEscola()
        {
            //Arrange
            Guid escolaId = Guid.NewGuid();
            var repository = new Mock<ITurmaRepository>();
            repository
                .Setup(x => x.ListarTodosPorEscola(escolaId))
                .Returns(Task.FromResult(new List<Turma>()));

            //Act
            var service = new TurmaService(repository.Object);
            await service.ListarTodosPorEscola(escolaId);

            //Assert
            repository.Verify(x => x.ListarTodosPorEscola(escolaId), Times.Once());
        }

        [Fact]
        public async Task DeveInvocarPesquisaPorId()
        {
            //Arrange
            Guid idEsperado = Guid.NewGuid();
            var repository = new Mock<ITurmaRepository>();
            repository
                .Setup(x => x.PesquisarPorId(idEsperado))
                .Returns(Task.FromResult(new Turma() { Escola = new Escola() }));

            //Act
            var service = new TurmaService(repository.Object);
            await service.PesquisarPorId(idEsperado);

            //Assert
            repository.Verify(x => x.PesquisarPorId(idEsperado), Times.Once());
        }
    }
}