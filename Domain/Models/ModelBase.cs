using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ModelBase
    {
        protected string MensagemValidacao { get; set; }

        public string GetMensagemValidacao()
        {
            return MensagemValidacao;
        }
    }
}
