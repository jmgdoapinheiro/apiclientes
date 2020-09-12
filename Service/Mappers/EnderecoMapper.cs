using Domain.DTOs;
using Domain.Models;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Service.Mappers
{
    public static class EnderecoMapper
    {
        public static Endereco MapearDtoParaModelo(long idCliente, EnderecoDto dto)
        {
            return new Endereco(idCliente, dto.Logradouro, dto.Bairro, dto.Cidade, dto.Estado);
        }
    }
}
