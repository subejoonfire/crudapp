using System.ComponentModel.DataAnnotations;
namespace crudapp.Models
{
    public class Unit
    {
        public int Id { get; set; }

        public string NamaUnit { get; set; }

        public string JenisUnit { get; set; }

        public int Kapasitas { get; set; }

        public string NoPol { get; set; }

        public string Sarana { get; set; }
        public bool IsAvailable { get; set; }
    }
 
}

