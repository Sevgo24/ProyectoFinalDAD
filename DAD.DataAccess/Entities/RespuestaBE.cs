using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAD.DataAccess.Entities
{
    public class RespuestaBE
    {
        public int CODRESPUESTA { get; set; }
        public int? PUNTAJERESPUESTA { get; set; }
        public DateTime FECHAREGISTRO { get; set; }
        public string CODALUMNO { get; set; }
        public int CODPREGUNTA { get; set; }
        public int CODDIMENSION { get; set; }
    }
}
