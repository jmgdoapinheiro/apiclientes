using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Tests
{
    [TestClass]
    public class CpfTeste
    {
        [TestMethod]
        public void Null()
        {
            var cpf = new Cpf(null);

            Assert.IsFalse(cpf.ValidarPreenchimento());
        }

        [TestMethod]
        public void Com_Espaco()
        {
            var cpf = new Cpf(" ");

            Assert.IsFalse(cpf.ValidarPreenchimento());
        }

        [TestMethod]
        public void Tamanho_Valido()
        {
            var cpf = new Cpf("220.892.260-30");

            Assert.IsTrue(cpf.ValidarTamanho());
        }

        [TestMethod]
        public void Tamanho_Invalido()
        {
            var cpf = new Cpf("1");

            Assert.IsFalse(cpf.ValidarTamanho());
        }

        [TestMethod]
        public void Numero_Valido()
        {
            var cpf = new Cpf("220.892.260-30");

            Assert.IsTrue(cpf.ValidarNumero());
        }

        [TestMethod]
        public void Numero_Invalido()
        {
            var cpf = new Cpf("111.111.111-11");

            Assert.IsFalse(cpf.ValidarNumero());
        }
    }
}
