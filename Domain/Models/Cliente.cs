using Domain.ValueObjects;
using System;

namespace Domain.Models
{
    public class Cliente
    {
        //	Cliente deve conter ao menos um identificador, nome, cpf e idade;
        public Cliente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = new Cpf(cpf);
            DataNascimento = dataNascimento;        
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; private set; }

        public void AdiocionarIdade(int idade)
        {
            Idade = idade;
        }

        public bool EValido()
        {
            var valido = Cpf.Validar();
            valido &= ValidarNome();

            return valido;
        }

        public bool ValidarNome()
        {
            if (Nome.Length > 30)
                return false;

            return true;
        }
    }
}
