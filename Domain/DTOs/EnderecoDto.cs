using System.Runtime.Serialization;

namespace Domain.DTOs
{
    public class EnderecoDto
    {
        [DataMember(Name = "cliente")]
        public string Cliente { get; set; }

        [DataMember(Name = "cpf")]
        public string Cpf { get; set; }

        [DataMember(Name = "logradouro")]
        public string Logradouro { get; set; }

        [DataMember(Name = "bairro")]
        public string Bairro { get; set; }

        [DataMember(Name = "cidade")]
        public string Cidade { get; set; }

        [DataMember(Name = "estado")]
        public string Estado { get; set; }
    }
}
