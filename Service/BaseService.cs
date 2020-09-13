using Infra.CrossCutting;
using System.Collections.Generic;

namespace Service
{
    public class BaseService
    {
        protected const int OK = 200;
        protected const int UNPROCESSABLE_ENTITY = 422;
        protected const int INTERNAL_SERVER_ERROR = 500;

        protected Response CriarResposta(int statusCode, bool sucesso, string mensagem)
        {
            return new Response(statusCode, sucesso, mensagem);
        }

        protected ResponseGeneric<T> CriarResposta<T>(int statusCode, bool sucesso, string mensagem, IEnumerable<T> listaDto = null)
        {
            var response = new ResponseGeneric<T>(statusCode, sucesso, mensagem);
            
            if(listaDto != null)
                response.AdicionarListaDto(listaDto);
            
            return response;
        }
    }
}
