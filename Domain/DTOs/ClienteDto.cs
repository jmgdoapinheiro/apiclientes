using System.Runtime.Serialization;

namespace Domain.DTOs
{
    public class ClienteDto
    {
        [DataMember(Name = "nome")]
        public string Nome { get; set; }

        [DataMember(Name = "cpf")]
        public string Cpf { get; set; }

        [DataMember(Name = "dataNascimento")]
        public string DataNascimento { get; set; }
    }
}
