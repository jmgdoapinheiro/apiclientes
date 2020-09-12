using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Tests
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void Nome_Excedeu_Limite_30_Caracteres()
        {
            var cliente = new Cliente("Joao Marcelo Gurgel do Amaral Pinheiro", "22089226030", new DateTime());

            Assert.IsFalse(cliente.ValidarNome());
        }

        [TestMethod]
        public void Nome_Nao_Excedeu_Limite_30_Caracteres()
        {
            var cliente = new Cliente("Joao Pinheiro", "22089226030", new DateTime());

            Assert.IsTrue(cliente.ValidarNome());
        }

        [TestMethod]
        public void DataNascimento_Nao_Foi_Preenchida()
        {
            var cliente = new Cliente("Joao Pinheiro", "22089226030", DateTime.MinValue);

            Assert.IsFalse(cliente.ValidarDataNascimento());
        }

        [TestMethod]
        public void DataNascimento_Foi_Preenchida()
        {
            var cliente = new Cliente("Joao Pinheiro", "22089226030", new DateTime(2000,1,1));

            Assert.IsTrue(cliente.ValidarDataNascimento());
        }
    }
}
