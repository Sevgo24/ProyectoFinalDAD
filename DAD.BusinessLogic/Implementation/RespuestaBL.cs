using DAD.DataAccess.Entities;
using DAD.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAD.BusinessLogic.Implementation
{
    public class RespuestaBL
    {
        RespuestaDA respuestaDA = new RespuestaDA();

        public List<RespuestaBandejaResultado> ListarRespuestaBandejaCms(string codAlumno, int codDimension)
        {
            return respuestaDA.ListarRespuestaBandejaCms(codAlumno, codDimension);
        }
    }
}
