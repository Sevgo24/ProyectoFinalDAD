using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAD.DataAccess.Entities
{
    public class AlumnoBE
    {
        public int CODALUMNO { get; set; }
        public string CORREOALUMNO { get; set; }
        public string SEXOALUMNO { get; set; }
        public string NOMBREESCUELA { get; set; }
        public string CICLOALUMNO { get; set; }
        public int? TRABAJOALUMNO { get; set; }
        public DateTime? FECHAREGISTRO { get; set; }
    }
}
