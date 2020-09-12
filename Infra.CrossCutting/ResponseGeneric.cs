using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.CrossCutting
{
    public class Response<T> : Response
    {
        public Response(bool sucesso, string mensagem) : base(sucesso, mensagem){}

        public IEnumerable<T> ListaDto { get; private set; }

        public void AdicionarListaDto(IEnumerable<T> listaDto)
        {
            ListaDto = listaDto;
        }
    }
}
