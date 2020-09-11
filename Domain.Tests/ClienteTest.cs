using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Tests
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void Cliente_Com_Nome_Invalido()
        {
            var cliente = new Cliente("Joao Marcelo Gurgel do Amaral Pinheiro", "22089226030", new DateTime());

            Assert.IsFalse(cliente.ValidarNome(), "É necessário que o nome do cliente contenha até 30 caracteres.");
        }

        [TestMethod]
        public void Cliente_Com_Cpf_Invalido()
        {
            var cliente = new Cliente("Joao Pinheiro", "1", new DateTime());

            Assert.IsFalse(cliente.Cpf.Validar(), "É necessário que o cpf do cliente seja válido.");
        }
    }
}
