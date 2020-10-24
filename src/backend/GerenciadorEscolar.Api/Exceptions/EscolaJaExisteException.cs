namespace GerenciadorEscolar.Api.Exceptions
{
    public class EscolaJaExisteException : AppException
    {
        public EscolaJaExisteException() : base("Escola já existe")
        {

        }
    }
}