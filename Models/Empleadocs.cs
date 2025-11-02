using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System;

namespace Examen1_PM_II.Models
{
    [Table("Empleado")]
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250), NotNull]
        public string Nombre { get; set; }

        public DateTime Fecha_ingreso { get; set; }

        [MaxLength(100)]
        public string Puesto { get; set; }

        [MaxLength(100)]
        public string Correo { get; set; }

        // La foto se almacena como una cadena Base64
        public string Foto { get; set; }
    }
}

