using Domain.Models;

namespace Domain.ValueObjects
{
    public class Cpf : ModelBase
    {
        public Cpf(string numero)
        {
            Numero = numero;
        }

        public string Numero { get; private set; }

        public bool EValido()
        {
            return ValidarPreenchimento() && ValidarTamanho() && ValidarNumero();
        }

        public bool ValidarPreenchimento()
        {
            if(string.IsNullOrWhiteSpace(Numero))
                return false;

            return true;
        }

        public bool ValidarTamanho()
        {
            if (Numero.Length != 14)
            {
                MensagemValidacao = "É necessário que o Cpf formatado contenha 14 caracteres.";
                return false;
            }
            
            return true;
        }

        public bool ValidarNumero()
        {
            MensagemValidacao = "É necessário que o Cpf seja um cpf válido.";

            string valor = Numero.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            MensagemValidacao = null;
            return true;
        }
    }
}
