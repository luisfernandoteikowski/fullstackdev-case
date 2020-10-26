using System;
using Microsoft.AspNetCore.Mvc;
using GerenciadorEscolar.Api.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using GerenciadorEscolar.Dto;

namespace GerenciadorEscolar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        public ITurmaService Service;

        public TurmaController(ITurmaService service)
        {
            Service = service;
        }

        [HttpGet("{escolaId}")]
        public async Task<ActionResult<IEnumerable<TurmaDto>>> Get(Guid escolaId)
        {
            return Ok(await Service.ListarTodosPorEscola(escolaId));
        }

        [HttpGet("{escolaId}/{id}")]
        public async Task<ActionResult<TurmaDto>> Get(Guid escolaId, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await Service.PesquisarPorId(id));
        }

        [HttpPost]
        public async Task<ActionResult<OperacaoCrudDto>> Post([FromBody] TurmaDto dados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var escola = await Service.Inserir(dados);

            return Ok(new OperacaoCrudDto()
            {
                Success = true,
                Id = escola.Id
            });
        }

        [HttpPut("{escolaId}/{id}")]
        public async Task<ActionResult<OperacaoCrudDto>> Put(Guid escolaId, Guid id, [FromBody] TurmaDto dados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dados.EscolaId = escolaId;
            dados.Id = id;
            await Service.Atualizar(dados);

            return Ok(new OperacaoCrudDto()
            {
                Success = true,
                Id = dados.Id
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperacaoCrudDto>> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Service.Excluir(id);

            return Ok(new OperacaoCrudDto()
            {
                Success = true,
                Id = id
            });
        }
    }
}