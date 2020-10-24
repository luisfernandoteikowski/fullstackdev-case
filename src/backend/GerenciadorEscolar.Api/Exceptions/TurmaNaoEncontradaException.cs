namespace GerenciadorEscolar.Api.Exceptions
{
    public class TurmaNaoEncontradaException : AppException
    {
        public TurmaNaoEncontradaException() : base("Turma não encontrada")
        {
            StatusCode = 404;
        }
    }
}