using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2E16018.Models
{
    [SQLite.Table("Sitios")]

    public class Sitios

    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Longitud { get; set; }
        [MaxLength(100)]
        public string Latitud { get; set; }

        public string Descripcion { get; set; }

        public string foto { get; set; }
    }
}
