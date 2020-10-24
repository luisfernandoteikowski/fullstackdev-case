namespace GerenciadorEscolar.Api.Exceptions
{
    public class TurmaJaExisteException : AppException
    {
        public TurmaJaExisteException() : base("Turma já existe")
        {
        }
    }
}