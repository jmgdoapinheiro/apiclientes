using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Tests
{
    [TestClass]
    public class EnderecoTeste
    {
        [TestMethod]
        public void Logradouro_Null()
        {
            var endereco = new Endereco(1, null, "Bairro Teste", "Cidade Teste", "Estado Teste");

            Assert.IsFalse(endereco.ValidarPreenchimento());
        }

        [TestMethod]
        public void Logradouro_Com_Espaco()
        {
            var endereco = new Endereco(1, " ", "Bairro Teste", "Cidade Teste", "Estado Teste");

            Assert.IsFalse(endereco.ValidarPreenchimento());
        }

        [TestMethod]
        public void Bairro_Null()
        {
            var endereco = new Endereco(1, "Logradouro Teste", null, "Cidade Teste", "Estado Teste");

            Assert.IsFalse(endereco.ValidarPreenchimento());
        }

        [TestMethod]
        public void Bairro_Com_Espaco()
        {
            var endereco = new Endereco(1, "Logradouro Teste", " ", "Cidade Teste", "Estado Teste");

            Assert.IsFalse(endereco.ValidarPreenchimento());
        }

        [TestMethod]
        public void Cidade_Null()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", null, "Estado Teste");

            Assert.IsFalse(endereco.ValidarPreenchimento());
        }

        [TestMethod]
        public void Cidade_Com_Espaco()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", " ", "Estado Teste");

            Assert.IsFalse(endereco.ValidarPreenchimento());
        }

        [TestMethod]
        public void Estado_Null()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", "Cidade Teste", null);

            Assert.IsFalse(endereco.ValidarPreenchimento());
        }

        [TestMethod]
        public void Estado_Com_Espaco()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", "Cidade Teste", " ");

            Assert.IsFalse(endereco.ValidarPreenchimento());
        }

        [TestMethod]
        public void Logradouro_Tamanho_Valido()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", "Cidade Teste", "Estado Teste");

            Assert.IsTrue(endereco.ValidarTamanho());
        }

        [TestMethod]
        public void Logradouro_Tamanho_Invalido()
        {
            var endereco = new Endereco(1, "Logradouro Testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "Bairro Teste", "Cidade Teste", "Estado Teste");

            Assert.IsFalse(endereco.ValidarTamanho());
        }

        [TestMethod]
        public void Bairro_Tamanho_Valido()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", "Cidade Teste", "Estado Teste");

            Assert.IsTrue(endereco.ValidarTamanho());
        }

        [TestMethod]
        public void Bairro_Tamanho_Invalido()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "Cidade Teste", "Estado Teste");

            Assert.IsFalse(endereco.ValidarTamanho());
        }

        [TestMethod]
        public void Cidade_Tamanho_Valido()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", "Cidade Teste", "Estado Teste");

            Assert.IsTrue(endereco.ValidarTamanho());
        }

        [TestMethod]
        public void Cidade_Tamanho_Invalido()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", "Cidade Testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "Estado Teste");

            Assert.IsFalse(endereco.ValidarTamanho());
        }

        [TestMethod]
        public void Estado_Tamanho_Valido()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", "Cidade Teste", "Estado Teste");

            Assert.IsTrue(endereco.ValidarTamanho());
        }

        [TestMethod]
        public void Estado_Tamanho_Invalido()
        {
            var endereco = new Endereco(1, "Logradouro Teste", "Bairro Teste", "Cidade Teste", "Estado Testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");

            Assert.IsFalse(endereco.ValidarTamanho());
        }

    }
}
