using System.ComponentModel.DataAnnotations;
namespace crudapp.Models
{
    public class Penumpang
    {
        public int Id { get; set; }
        public int IdUnit { get; set; }
        public string Nama { get; set; }
        public string NIK { get; set; }
        public string Tujuan { get; set; }
        public DateTime JamBerangkat { get; set; }
        public string TempatDuduk { get; set; }
    }
}