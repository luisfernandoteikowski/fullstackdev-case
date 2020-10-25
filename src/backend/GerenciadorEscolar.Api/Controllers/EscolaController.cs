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
    public class EscolaController : ControllerBase
    {
        public IEscolaService Service { get; }

        public EscolaController(IEscolaService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscolaDto>>> Get()
        {
            return Ok(await Service.ListarTodos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EscolaDto>> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await Service.PesquisarPorId(id));
        }

        [HttpPost]
        public async Task<ActionResult<OperacaoCrudDto>> Post([FromBody] EscolaDto dados)
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

        [HttpPut("{id}")]
        public async Task<ActionResult<OperacaoCrudDto>> Put(Guid id, [FromBody] EscolaDto dados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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