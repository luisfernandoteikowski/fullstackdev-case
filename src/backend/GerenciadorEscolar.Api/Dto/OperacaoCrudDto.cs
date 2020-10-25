using System;

namespace GerenciadorEscolar.Dto
{
    public class OperacaoCrudDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}