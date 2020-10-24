using System;
using System.ComponentModel.DataAnnotations;
using GerenciadorEscolar.Entity;

namespace GerenciadorEscolar.Dto
{
    public class TurmaDto
    {
        public Guid Id { get; set; }
        public Guid EscolaId { get; set; }
        public int Ano { get; set; }
        [Required]
        [MaxLength(255)]
        public string Curso { get; set; }
        [Required]
        [MaxLength(255)]
        public string Serie { get; set; }
        [Required]
        [MaxLength(255)]
        public string Nome { get; set; }

        internal static TurmaDto deTurma(Turma turma)
        {
            if (turma == null)
            {
                return null;
            }
            return new TurmaDto()
            {
                Id = turma.Id,
                EscolaId = turma.EscolaId,
                Ano = turma.Ano,
                Curso = turma.Curso,
                Serie = turma.Serie,
                Nome = turma.Nome
            };
        }
    }
}