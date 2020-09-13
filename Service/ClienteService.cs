using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
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

        public async Task<ResponseGeneric<ListarClienteDto>> ListarAsync(ClienteDto clienteDto)
        {
            try
            {
                return CriarResposta(OK, true, null, await _clienteRepository.ListarAsync(clienteDto));
            }
            catch (System.Exception)
            {
                return CriarResposta<ListarClienteDto>(INTERNAL_SERVER_ERROR, false, "Ocorreu um erro ao tentar listar clientes. Favor aguardar uns minutos e tentar novamente.");
            }
        }

        public async Task<Response> CadastrarAsync(ClienteDto dto)
        {
            try
            {
                var cliente = ClienteMapper.MapearDtoParaModelo(dto);

                if (!cliente.EValido())
                    return CriarResposta(UNPROCESSABLE_ENTITY, false, cliente.GetMensagemValidacao());

                if (await _clienteRepository.ObterAsync(cliente.Cpf.Numero) != null)
                    return CriarResposta(UNPROCESSABLE_ENTITY, false, "Já existe um cliente com o cpf cadastrado.");

                await _clienteRepository.CadastrarAsync(cliente);

                return CriarResposta(OK, true, "Cliente cadastrado.");
            }
            catch (System.Exception)
            {
                return CriarResposta(INTERNAL_SERVER_ERROR, false, "Ocorreu um erro ao tentar cadastrar o cliente. Favor aguardar uns minutos e tentar novamente.");
            }
        }

        public async Task<Response> AtualizarAsync(long id, ClienteDto dto)
        {
            try
            {
                var cliente = ClienteMapper.MapearDtoParaModelo(dto);

                if (!cliente.EValido())
                    return CriarResposta(UNPROCESSABLE_ENTITY, false, cliente.GetMensagemValidacao());

                if (await _clienteRepository.ObterAsync(id) == null)
                    return CriarResposta(UNPROCESSABLE_ENTITY, false, "Cliente inexistente.");

                await _clienteRepository.Atualizar(id, cliente);

                return CriarResposta(OK, true, "Cliente atualizado.");
            }
            catch (System.Exception)
            {
                return CriarResposta(INTERNAL_SERVER_ERROR, false, "Ocorreu um erro ao tentar atualizar o cliente. Favor aguardar uns minutos e tentar novamente.");
            }
        }

        public async Task<Response> ExcluirAsync(long id)
        {
            try
            {
                var cliente = await _clienteRepository.ObterAsync(id);

                if (cliente == null) return CriarResposta(UNPROCESSABLE_ENTITY, false, "Cliente inexistente.");

                await _clienteRepository.ExcluirAsync(id);

                return CriarResposta(OK, true, "Cliente excluído.");
            }
            catch (System.Exception)
            {
                return CriarResposta(INTERNAL_SERVER_ERROR, false, "Ocorreu um erro ao tentar excluir o cliente. Favor aguardar uns minutos e tentar novamente.");
            }
        }
    }
}
