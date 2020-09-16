using DAD.DataAccess.Entities;
using DAD.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAD.BusinessLogic.Implementation
{
    public class AlumnoBL
    {
        AlumnoDA alumnoDA = new AlumnoDA();

        public List<AlumnoBandejaResultado> ListaUsuarioBandejaCms(string codAlumno)
        {
            return alumnoDA.ListaUsuarioBandejaCms(codAlumno);
        }


    }
}
