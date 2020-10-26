using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using GerenciadorEscolar.Entity;
using GerenciadorEscolar.Dto;
using GerenciadorEscolar.Api.Service;
using System.Collections.Generic;
using GerenciadorEscolar.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorEscolar.Test.Controllers
{
    public class EscolaControllerTests
    {
        [Fact]
        public async Task GetDeveRetornarTodasEscolas()
        {
            //Arrange
            var service = new Mock<IEscolaService>();
            var resultado = new List<EscolaDto>();

            service
                .Setup(x => x.ListarTodos())
                .Returns(Task.FromResult(resultado));

            var controller = new EscolaController(service.Object);

            //Act
            var resultadoRequest = await controller.Get();

            //Assert
            Assert.Same(resultado, ((OkObjectResult)resultadoRequest.Result).Value);
        }

        [Fact]
        public async Task GetDeveRetornarUmaEscola()
        {
            //Arrange
            var service = new Mock<IEscolaService>();
            var resultado = new EscolaDto()
            {
                Id = Guid.NewGuid()
            };

            service
                .Setup(x => x.PesquisarPorId(resultado.Id))
                .Returns(Task.FromResult(resultado));

            var controller = new EscolaController(service.Object);

            //Act
            var resultadoRequest = await controller.Get(resultado.Id);

            //Assert
            Assert.Same(resultado, ((OkObjectResult)resultadoRequest.Result).Value);
        }

        [Fact]
        public async Task PostDeveInserirEscola()
        {
            //Arrange
            var service = new Mock<IEscolaService>();
            var dados = new EscolaDto()
            {
                Id = Guid.NewGuid()
            };

            service
                .Setup(x => x.Inserir(dados))
                .Returns(Task.FromResult(new Escola()
                {
                    Id = dados.Id
                }));

            var controller = new EscolaController(service.Object);

            //Act
            var resultadoRequest = await controller.Post(dados);
            var data = Assert.IsType<OkObjectResult>(resultadoRequest.Result);
            var operacaoCrud = Assert.IsType<OperacaoCrudDto>(data.Value);

            //Assert
            Assert.Equal(dados.Id, operacaoCrud.Id);
            Assert.True(operacaoCrud.Success);
        }

        [Fact]
        public async Task PutDeveAtualizarEscola()
        {
            //Arrange
            var service = new Mock<IEscolaService>();
            var dados = new EscolaDto()
            {
                Id = Guid.NewGuid()
            };

            service
                .Setup(x => x.Atualizar(dados))
                .Returns(Task.FromResult(new Escola()
                {
                    Id = dados.Id
                }));

            var controller = new EscolaController(service.Object);

            //Act
            var resultadoRequest = await controller.Put(dados.Id, dados);
            var data = Assert.IsType<OkObjectResult>(resultadoRequest.Result);
            var operacaoCrud = Assert.IsType<OperacaoCrudDto>(data.Value);

            //Assert
            Assert.Equal(dados.Id, operacaoCrud.Id);
            Assert.True(operacaoCrud.Success);
        }

        [Fact]
        public async Task PutDeveExcluirEscola()
        {
            //Arrange
            var service = new Mock<IEscolaService>();
            Guid escolaId = Guid.NewGuid();

            service
                .Setup(x => x.Excluir(escolaId))
                .Returns(Task.CompletedTask);

            var controller = new EscolaController(service.Object);

            //Act
            var resultadoRequest = await controller.Delete(escolaId);
            var data = Assert.IsType<OkObjectResult>(resultadoRequest.Result);
            var operacaoCrud = Assert.IsType<OperacaoCrudDto>(data.Value);

            //Assert
            Assert.Equal(escolaId, operacaoCrud.Id);
            Assert.True(operacaoCrud.Success);
            service.Verify(x => x.Excluir(escolaId), Times.Once);
        }
    }
}