using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.CrossCutting
{
    public class ResponseGeneric<T> : Response
    {
        public ResponseGeneric(int statusCode, bool sucesso, string mensagem) : base(statusCode, sucesso, mensagem){}

        public IEnumerable<T> ListaDto { get; private set; }

        public void AdicionarListaDto(IEnumerable<T> listaDto)
        {
            ListaDto = listaDto;
        }
    }
}
