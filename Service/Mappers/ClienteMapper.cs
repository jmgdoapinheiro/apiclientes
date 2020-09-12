using Domain.DTOs;
using Domain.Models;
using System;

namespace Service.Mappers
{
    public static class ClienteMapper
    {
        public static Cliente MapearDtoParaModelo(ClienteDto dto)
        {
            DateTime dataNascimento;

            DateTime.TryParse(dto.DataNascimento, out dataNascimento);

            return new Cliente(dto.Nome, dto.Cpf, dataNascimento);
        } 
    }
}
