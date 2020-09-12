using Infra.CrossCutting;

namespace Service
{
    public class BaseService
    {
        protected Response CriarResposta(bool sucesso, string mensagem)
        {
            return new Response(sucesso, mensagem);
        }

        protected Response<T> CriarResposta<T>(bool sucesso, string mensagem)
        {
            return new Response<T>(sucesso, mensagem);
        }
    }
}
