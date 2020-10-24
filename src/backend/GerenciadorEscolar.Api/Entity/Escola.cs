using System;
using System.Collections.Generic;

namespace GerenciadorEscolar.Entity
{
    public class Escola : IIdEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int NumeroInep { get; set; }
        public ICollection<Turma> Turmas { get; set; }

    }
}