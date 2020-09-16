using DAD.DataAccess.Entities;
using DAD.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAD.BusinessLogic.Implementation
{
    public class DimensionBL
    {
        DimensionDA dimensionDA = new DimensionDA();

        public List<DimensionBE> ListarDimensionCms()
        {
            return dimensionDA.ListarDimensionCms();
        }
    }
}
