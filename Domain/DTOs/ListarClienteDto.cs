using System.Runtime.Serialization;

namespace Domain.DTOs
{
    public class ListarClienteDto
    {
        [DataMember(Name = "nome")]
        public string Nome { get; set; }

        [DataMember(Name = "cpf")]
        public string Cpf { get; set; }

        [DataMember(Name = "idade")]
        public string Idade { get; set; }
    }
}
