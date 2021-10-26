using System.ComponentModel.DataAnnotations;

namespace RedBagerApi.Models
{
    public class Consumer
    {
        [Key]
        public long CpfCnpj {get; set;}
        public int TotCallFree {get; set;}
        public int TotCall {get; set;}
        public string ChavePixCpfCnpj {get; set;}
        public string ChavePixCelular {get; set;}
        public string ChavePixEmail {get; set;}
        public string ContatoResponsavel {get; set;}
    }
}