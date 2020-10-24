using System;

namespace GerenciadorEscolar.Entity
{
    public class Turma : IIdEntity
    {
        public Guid Id { get; set; }
        public Guid EscolaId { get; set; }
        public Escola Escola { get; set; }
        public int Ano { get; set; }
        public string Curso { get; set; }
        public string Serie { get; set; }
        public string Nome { get; set; }
    }
}