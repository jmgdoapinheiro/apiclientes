namespace Infra.CrossCutting
{
    public class Erro
    {
        public Erro(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; private set; }
    }
}
