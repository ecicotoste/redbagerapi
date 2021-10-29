using System.ComponentModel.DataAnnotations;

namespace RedBagerApi.Models
{
    public class PlacaRB
    {
        [Key]
        public int Id {get; set;}
        public long DataPlaca {get; set;}
        public int Status {get; set;}
        public long cpfCnpj {get; set;}
        public string IdChamador {get; set;}
    }
}