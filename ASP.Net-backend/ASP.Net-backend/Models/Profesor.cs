using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.Net_backend.Models
{
    public class Profesor
    {
        [Key]
        public int IdP { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string NombreP { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string Apellidos { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(20)")]
        public string TipoContrato { get; set; } = string.Empty;

        [Column]
        public int Salario { get; set; } = 0;

        [Column]
        public bool EsDecano { get; set; } = false;

        [Column]
        public bool FueBorrado { get; set; } = false;

    }
}
