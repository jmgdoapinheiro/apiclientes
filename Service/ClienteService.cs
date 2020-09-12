using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Infra.CrossCutting;
using Service.Mappers;
using System.Threading.Tasks;

namespace Service
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Response> CadastrarAsync(ClienteDto dto)
        {
            var cliente = ClienteMapper.MapearDtoParaModelo(dto);

            if (!cliente.EValido())
                return CriarResposta(false, cliente.GetMensagemValidacao());

            if(await _clienteRepository.ObterAsync(cliente.Cpf.Numero) == null)
                return CriarResposta(false, "Já existe um cliente com o cpf cadastrado.");

            await _clienteRepository.CadastrarAsync(cliente);

            return CriarResposta(true, "Cliente cadastrado.");
        }
    }
}
