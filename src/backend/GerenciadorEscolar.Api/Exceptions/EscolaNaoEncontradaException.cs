namespace GerenciadorEscolar.Api.Exceptions
{
    public class EscolaNaoEncontradaException : AppException
    {
        public EscolaNaoEncontradaException() : base("Escola não encontrada")
        {
            StatusCode = 404;
        }
    }
}