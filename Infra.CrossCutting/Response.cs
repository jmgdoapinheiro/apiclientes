namespace Infra.CrossCutting
{
    public class Response
    {
        public Response(int statusCode, bool sucesso, string mensagem)
        {
            StatusCode = statusCode;
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public int StatusCode { get; private set; }
        public bool Sucesso { get; private set; }
        public string Mensagem { get; private set; }
    }
}
