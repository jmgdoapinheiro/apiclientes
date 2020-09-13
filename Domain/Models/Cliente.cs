using Domain.ValueObjects;
using System;

namespace Domain.Models
{
    public class Cliente : ModelBase
    {
        public Cliente() { }
        //	Cliente deve conter ao menos um identificador, nome, cpf e idade;
        public Cliente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = new Cpf(cpf);
            DataNascimento = dataNascimento;        
        }

        public long Id { get; private set; }
        public string Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; private set; }
        public Endereco Endereco { get; set; }

        public void AdiocionarId(long id)
        {
            Id = id;
        }

        public void AdicionarIdade()
        {
            Idade = CalcularIdade(DateTime.Now);
        }

        public void AdiocionarEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }

        public bool EValido()
        {
            return ValidarNome() && Cpf.EValido() && ValidarDataNascimento();
        }

        public bool ValidarNome()
        {
            if (Nome.Length > 30)
            {
                MensagemValidacao = "O campo nome deve conter até 30 caracteres.";
                return false;
            }
            
            return true;
        }

        public bool ValidarDataNascimento()
        {
            if (DataNascimento == DateTime.MinValue)
            {
                MensagemValidacao = "É necessário que a data de nascimento do cliente seja preenchida e esteja em um formato válido.";
                return false;
            }

            if (DataNascimento > DateTime.Now)
            {
                MensagemValidacao = "A data de nascimento não poder ser uma data futura.";
                return false;
            }

            return true;
        }

        public int CalcularIdade(DateTime hoje)
        {
            int idade = hoje.Year - DataNascimento.Year;

            if (hoje.DayOfYear < DataNascimento.DayOfYear)
                idade = idade - 1;

            return idade;
        }
    }
}
