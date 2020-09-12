using Domain.Models;

namespace Domain.ValueObjects
{
    public class Endereco : ModelBase
    {
        public Endereco(long idCliente, string logradouro, string bairro, string cidade, string estado)
        {
            IdCliente = idCliente; 
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public long IdCliente { get; private set; }
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public bool EValido()
        {
            return ValidarPreenchimento() && ValidarTamanho();
        }

        public bool ValidarPreenchimento()
        {
            if(string.IsNullOrWhiteSpace(Logradouro))
                return false;

            if (string.IsNullOrWhiteSpace(Bairro))
                return false;

            if (string.IsNullOrWhiteSpace(Cidade))
                return false;

            if (string.IsNullOrWhiteSpace(Estado))
                return false;

            return true;
        }

        public bool ValidarTamanho()
        {
            if (Logradouro.Length > 50)
            {
                MensagemValidacao = "É necessário que o logradouro contenha até 50 caracteres.";
                return false;
            }

            if (Bairro.Length > 40)
            {
                MensagemValidacao = "É necessário que o bairro contenha até 40 caracteres.";
                return false;
            }

            if (Cidade.Length > 40)
            {
                MensagemValidacao = "É necessário que o cidade contenha até 40 caracteres.";
                return false;
            }

            if (Estado.Length > 40)
            {
                MensagemValidacao = "É necessário que o estado contenha até 40 caracteres.";
                return false;
            }

            return true;
        }
    }
}
