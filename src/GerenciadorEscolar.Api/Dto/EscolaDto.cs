using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GerenciadorEscolar.Entity;
using System.Linq;

namespace GerenciadorEscolar.Dto
{
    public class EscolaDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Nome { get; set; }
        public int NumeroInep { get; set; }
        public IList<TurmaDto> Turmas { get; set; }

        internal static EscolaDto deEscola(Escola escola)
        {
            if (escola == null)
            {
                return null;
            }
            return new EscolaDto()
            {
                Id = escola.Id,
                Nome = escola.Nome,
                NumeroInep = escola.NumeroInep,
                Turmas = escola.Turmas?.Select(x => TurmaDto.deTurma(x)).ToList()
            };
        }
    }
}
