using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infra.CrossCutting;
using Service.Mappers;
using System.Threading.Tasks;

namespace Service
{
    public class EnderecoService : BaseService, IEnderecoService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IClienteRepository clienteRepository, IEnderecoRepository enderecoRepository)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<ResponseGeneric<EnderecoDto>> ListarAsync(EnderecoDto enderecoDto)
        {
            try
            {
                return CriarResposta(OK, true, null, await _enderecoRepository.ListarAsync(enderecoDto));
            }
            catch (System.Exception e)
            {
                return CriarResposta<EnderecoDto>(INTERNAL_SERVER_ERROR, false, "Ocorreu um erro ao tentar listar enderecos. Favor aguardar uns minutos e tentar novamente.");
            }
        }

        public async Task<Response> CadastrarAsync(EnderecoDto dto)
        {
            try
            {
                var cliente = await _clienteRepository.ObterAsync(dto.Cpf);

                if (cliente == null) return CriarResposta(UNPROCESSABLE_ENTITY, false, "Cliente inexistente.");

                if (await _enderecoRepository.ObterAsync(cliente.Id) != null) return CriarResposta(UNPROCESSABLE_ENTITY, false, "Já existe um endereço para esse cliente.");

                var endereco = EnderecoMapper.MapearDtoParaModelo(cliente.Id, dto);

                if (!endereco.EValido())
                    return CriarResposta(UNPROCESSABLE_ENTITY, false, cliente.GetMensagemValidacao());

                await _enderecoRepository.CadastrarAsync(endereco);

                return CriarResposta(OK, true, "Endereço cadastrado.");

            }
            catch (System.Exception)
            {
                return CriarResposta(INTERNAL_SERVER_ERROR, false, "Ocorreu um erro ao tentar cadastrar o endereço. Favor aguardar uns minutos e tentar novamente.");
            }
        }

        public async Task<Response> AtualizarAsync(long idCliente, EnderecoDto dto)
        {
            try
            {
                if (await _enderecoRepository.ObterAsync(idCliente) == null) return CriarResposta(UNPROCESSABLE_ENTITY, false, "Não existe um endereço para esse cliente.");

                var endereco = EnderecoMapper.MapearDtoParaModelo(idCliente, dto);

                if (!endereco.EValido())
                    return CriarResposta(UNPROCESSABLE_ENTITY, false, endereco.GetMensagemValidacao());

                await _enderecoRepository.AtualizarAsync(endereco);

                return CriarResposta(OK, true, "Endereço atualizado.");
            }
            catch (System.Exception)
            {
                return CriarResposta(INTERNAL_SERVER_ERROR, false, "Ocorreu um erro ao tentar atualizar o endereço. Favor aguardar uns minutos e tentar novamente.");
            }
        }

        public async Task<Response> ExcluirAsync(long idCliente)
        {
            try
            {
                var endereco = await _enderecoRepository.ObterAsync(idCliente);

                if (endereco == null) return CriarResposta(UNPROCESSABLE_ENTITY, false, "Endereco inexistente.");

                await _enderecoRepository.ExcluirAsync(idCliente);

                return CriarResposta(OK, true, "Endereço excluído.");

            }
            catch (System.Exception)
            {
                return CriarResposta(INTERNAL_SERVER_ERROR, false, "Ocorreu um erro ao tentar excluir o endereço. Favor aguardar uns minutos e tentar novamente.");
            }
        }
    }
}
