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
    public class EscolaServiceTests
    {
        [Fact]
        public async Task DeveInvocarInserirEscola()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            var escolaDto = new EscolaDto()
            {
                Nome = "EMEF Santa Barbara",
                NumeroInep = 4789
            };

            //Act
            var service = new EscolaService(repository.Object);
            await service.Inserir(escolaDto);

            //Assert
            repository.Verify(
                x => x.Inserir(It.Is<Escola>(y => y.Nome == escolaDto.Nome && y.NumeroInep == escolaDto.NumeroInep)), Times.Once());
        }

        [Fact]
        public async Task DeveLancarExceptionQuandoEscolaJaExisteAoInserir()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            var escolaDto = new EscolaDto()
            {
                Nome = "EMEF Santa Barbara",
                NumeroInep = 4789
            };

            //Act
            repository
                .Setup(x => x.PesquisarPorNome(escolaDto.Nome))
                .Returns(Task.FromResult(new Escola()
                {
                    Id = Guid.NewGuid(),
                    Nome = escolaDto.Nome,
                    NumeroInep = escolaDto.NumeroInep
                }));

            //Assert
            var service = new EscolaService(repository.Object);
            await Assert.ThrowsAnyAsync<EscolaJaExisteException>(async () => await service.Inserir(escolaDto));
        }

        [Fact]
        public async Task DeveInvocarAtualizarEscola()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            var escolaDto = new EscolaDto()
            {
                Id = Guid.NewGuid(),
                Nome = "EMEF Santa Barbara",
                NumeroInep = 4789
            };
            var escolaRetorno = Task.FromResult(new Escola()
            {
                Id = escolaDto.Id,
                Nome = escolaDto.Nome,
                NumeroInep = escolaDto.NumeroInep
            });
            repository
                .Setup(x => x.PesquisarPorId(escolaDto.Id))
                .Returns(escolaRetorno);

            //Act    
            var service = new EscolaService(repository.Object);
            await service.Atualizar(escolaDto);

            //Assert
            repository.Verify(x => x.Atualizar(It.Is<Escola>(y => y.Id == escolaDto.Id && y.Nome == escolaDto.Nome && y.NumeroInep == escolaDto.NumeroInep)), Times.Once());
        }


        [Fact]
        public async Task DeveLancarExceptionQuandoEscolaJaExisteAoAtualizar()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            var escolaDto = new EscolaDto()
            {
                Id = Guid.NewGuid(),
                Nome = "EMEF Santa Barbara",
                NumeroInep = 4789
            };

            //Act
            repository
                .Setup(x => x.PesquisarPorNome(escolaDto.Nome))
                .Returns(Task.FromResult(new Escola()
                {
                    Id = Guid.NewGuid(),
                    Nome = escolaDto.Nome,
                    NumeroInep = escolaDto.NumeroInep
                }));

            //Assert
            var service = new EscolaService(repository.Object);
            await Assert.ThrowsAnyAsync<EscolaJaExisteException>(async () => await service.Atualizar(escolaDto));
        }

        [Fact]
        public async Task NaoDeveLancarExceptionQuandoEscolaJaExisteComMesmoIdAoAtualizar()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            var escolaDto = new EscolaDto()
            {
                Id = Guid.NewGuid(),
                Nome = "EMEF Santa Barbara",
                NumeroInep = 4789
            };
            var escolaRetorno = new Escola()
            {
                Id = escolaDto.Id,
                Nome = escolaDto.Nome,
                NumeroInep = escolaDto.NumeroInep
            };

            repository
                .Setup(x => x.PesquisarPorId(escolaDto.Id))
                .Returns(Task.FromResult(escolaRetorno));

            repository
                .Setup(x => x.PesquisarPorNome(escolaDto.Nome))
                .Returns(Task.FromResult(escolaRetorno));

            //Act    
            var service = new EscolaService(repository.Object);
            await service.Atualizar(escolaDto);

            //Assert
            repository.Verify(x => x.Atualizar(It.Is<Escola>(y => y.Id == escolaDto.Id && y.Nome == escolaDto.Nome && y.NumeroInep == escolaDto.NumeroInep)), Times.Once());
        }

        [Fact]
        public async Task DeveInvocarExcluirEscola()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            var escolaDto = new EscolaDto()
            {
                Id = Guid.NewGuid(),
                Nome = "EMEF Santa Barbara",
                NumeroInep = 4789
            };

            //Act
            repository
                .Setup(x => x.PesquisarPorId(escolaDto.Id))
                .Returns(Task.FromResult(new Escola()
                {
                    Id = escolaDto.Id,
                    Nome = escolaDto.Nome,
                    NumeroInep = escolaDto.NumeroInep
                }));

            //Assert
            var service = new EscolaService(repository.Object);
            await service.Excluir(escolaDto.Id);

            //Assert
            repository.Verify(x => x.Excluir(It.Is<Escola>(y => y.Id == escolaDto.Id)), Times.Once());
        }

        [Fact]
        public async Task DeveLancarExceptionQuandoEscolaNaoForEncontradaAoExcluir()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            var escolaDto = new EscolaDto()
            {
                Id = Guid.NewGuid(),
                Nome = "EMEF Santa Barbara",
                NumeroInep = 4789
            };

            //Act
            repository
                .Setup(x => x.PesquisarPorId(escolaDto.Id))
                .Returns(Task.FromResult<Escola>(null));

            //Assert
            var service = new EscolaService(repository.Object);
            await Assert.ThrowsAnyAsync<EscolaNaoEncontradaException>(async () => await service.Excluir(escolaDto.Id));
        }

        [Fact]
        public async Task DeveInvocarListarTodos()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            repository
                .Setup(x => x.ListarTodos())
                .Returns(Task.FromResult(new List<Escola>()));

            //Act
            var service = new EscolaService(repository.Object);
            await service.ListarTodos();

            //Assert
            repository.Verify(x => x.ListarTodos(), Times.Once());
        }

        [Fact]
        public async Task DeveInvocarPesquisarPorId()
        {
            //Arrange
            var repository = new Mock<IEscolaRepository>();
            Guid idEsperado = Guid.NewGuid();

            repository
                .Setup(x => x.PesquisarPorId(idEsperado))
                .Returns(Task.FromResult(new Escola()));

            //Act    
            var service = new EscolaService(repository.Object);
            await service.PesquisarPorId(idEsperado);

            //Assert
            repository.Verify(x => x.PesquisarPorId(idEsperado), Times.Once());
        }
    }
}