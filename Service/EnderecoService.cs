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

        //public async Task<IEnumerable<EnderecoDto>> ListarAsync(ClienteDto clienteDto, EnderecoDto enderecoDto)
        public async Task<Response<EnderecoDto>> ListarAsync(EnderecoDto enderecoDto)
        {
            var cliente = await _clienteRepository.ObterAsync(enderecoDto.Cpf);

            if (cliente == null)
                return CriarResposta<EnderecoDto>(false, "Para realizar essa consulta, é necessários que o cpf do cliente seja cadastrado.");

            var endereco = EnderecoMapper.MapearDtoParaModelo(cliente.Id, enderecoDto);

            var response = CriarResposta<EnderecoDto>(true, null);

            response.AdicionarListaDto(await _enderecoRepository.ListarAsync(cliente, endereco));

            return response;
        }

        public async Task<Response> CadastrarAsync(EnderecoDto dto)
        {
            var cliente = await _clienteRepository.ObterAsync(dto.Cpf);

            if (cliente == null) return CriarResposta(false, "Cliente inexistente.");

            if(await _enderecoRepository.ObterAsync(cliente.Id) != null) return CriarResposta(false, "Já existe um endereço para esse cliente.");

            var endereco = EnderecoMapper.MapearDtoParaModelo(cliente.Id, dto);

            if (!endereco.EValido())
                return CriarResposta(false, cliente.GetMensagemValidacao());

            await _enderecoRepository.CadastrarAsync(endereco);

            return CriarResposta(true, "Endereço cadastrado.");
        }

        public async Task<Response> AtualizarAsync(long idCliente, EnderecoDto dto)
        {
            var cliente = await _clienteRepository.ObterAsync(dto.Cpf);

            if (cliente == null) return CriarResposta(false, "Cliente inexistente.");

            if (await _enderecoRepository.ObterAsync(cliente.Id) == null) return CriarResposta(false, "Não existe um endereço para esse cliente.");

            var endereco = EnderecoMapper.MapearDtoParaModelo(idCliente, dto);

            if (!endereco.EValido())
                return CriarResposta(false, endereco.GetMensagemValidacao());

            await _enderecoRepository.AtualizarAsync(endereco);

            return CriarResposta(true, "Endereço atualizado.");
        }

        public async Task<Response> ExcluirAsync(long idCliente)
        {
            var endereco = await _enderecoRepository.ObterAsync(idCliente);

            if (endereco == null) return CriarResposta(false, "Endereco inexistente.");

            await _enderecoRepository.ExcluirAsync(idCliente);

            return CriarResposta(true, "Endereço excluído.");
        }
    }
}
