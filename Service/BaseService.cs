using Infra.CrossCutting;

namespace Service
{
    public class BaseService
    {
        protected Response CriarResposta(bool sucesso, string mensagem)
        {
            return new Response(sucesso, mensagem);
        }
    }
}
