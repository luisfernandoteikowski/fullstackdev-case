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
    public class TurmaControllerTests
    {
        public Guid EscolaId;

        public TurmaControllerTests()
        {
            EscolaId = Guid.NewGuid();
        }

        [Fact]
        public async Task GetDeveRetornarTodasTurmasDaEscola()
        {
            //Arrange
            var service = new Mock<ITurmaService>();
            var resultado = new List<TurmaDto>();

            service
                .Setup(x => x.ListarTodosPorEscola(EscolaId))
                .Returns(Task.FromResult(resultado));

            var controller = new TurmaController(service.Object);

            //Act
            var resultadoRequest = await controller.Get(EscolaId);

            //Assert
            Assert.Same(resultado, ((OkObjectResult)resultadoRequest.Result).Value);
        }

        [Fact]
        public async Task GetDeveRetornarUmaTurma()
        {
            //Arrange
            var service = new Mock<ITurmaService>();
            var resultado = new TurmaDto()
            {
                Id = Guid.NewGuid()
            };

            service
                .Setup(x => x.PesquisarPorId(resultado.Id))
                .Returns(Task.FromResult(resultado));

            var controller = new TurmaController(service.Object);

            //Act
            var resultadoRequest = await controller.Get(EscolaId, resultado.Id);

            //Assert
            Assert.Same(resultado, ((OkObjectResult)resultadoRequest.Result).Value);
        }

        [Fact]
        public async Task PostDeveInserirTurma()
        {
            //Arrange
            var service = new Mock<ITurmaService>();
            var dados = new TurmaDto()
            {
                Id = Guid.NewGuid()
            };

            service
                .Setup(x => x.Inserir(dados))
                .Returns(Task.FromResult(new Turma()
                {
                    Id = dados.Id
                }));

            var controller = new TurmaController(service.Object);

            //Act
            var resultadoRequest = await controller.Post(dados);
            var data = Assert.IsType<OkObjectResult>(resultadoRequest.Result);
            var operacaoCrud = Assert.IsType<OperacaoCrudDto>(data.Value);

            //Assert
            Assert.Equal(dados.Id, operacaoCrud.Id);
            Assert.True(operacaoCrud.Success);
        }

        [Fact]
        public async Task PutDeveAtualizarTurma()
        {
            //Arrange
            var service = new Mock<ITurmaService>();
            var dados = new TurmaDto()
            {
                Id = Guid.NewGuid()
            };

            service
                .Setup(x => x.Atualizar(dados))
                .Returns(Task.FromResult(new Turma()
                {
                    Id = dados.Id
                }));

            var controller = new TurmaController(service.Object);

            //Act
            var resultadoRequest = await controller.Put(EscolaId, dados.Id, dados);
            var data = Assert.IsType<OkObjectResult>(resultadoRequest.Result);
            var operacaoCrud = Assert.IsType<OperacaoCrudDto>(data.Value);

            //Assert
            Assert.Equal(dados.Id, operacaoCrud.Id);
            Assert.True(operacaoCrud.Success);
        }

        [Fact]
        public async Task PutDeveExcluirTurma()
        {
            //Arrange
            var service = new Mock<ITurmaService>();
            Guid turmaId = Guid.NewGuid();

            service
                .Setup(x => x.Excluir(turmaId))
                .Returns(Task.CompletedTask);

            var controller = new TurmaController(service.Object);

            //Act
            var resultadoRequest = await controller.Delete(turmaId);
            var data = Assert.IsType<OkObjectResult>(resultadoRequest.Result);
            var operacaoCrud = Assert.IsType<OperacaoCrudDto>(data.Value);

            //Assert
            Assert.Equal(turmaId, operacaoCrud.Id);
            Assert.True(operacaoCrud.Success);
            service.Verify(x => x.Excluir(turmaId), Times.Once);
        }
    }
}