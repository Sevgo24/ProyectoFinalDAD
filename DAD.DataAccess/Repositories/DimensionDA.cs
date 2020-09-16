using DAD.DataAccess.ConnectionBD;
using DAD.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAD.DataAccess.Repositories
{
    public class DimensionDA
    {

        public List<DimensionBE> ListarDimensionCms()
        {
            var lista = new List<DimensionBE>();

            using (var adoHelper = new AdoHelper())
            {
                using (var reader = adoHelper.ExecDataReaderProc("[DBO].[USP_DIMENSION_LIST]"))
                {
                    while (reader.Read()) lista.Add(new DimensionBE() 
                    { 
                        CODDIMENSION = Convert.ToInt32(reader["CODDIMENSION"].ToString()),
                        NOMBREDIMENSION = reader["NOMBREDIMENSION"].ToString()
                    });
                }
            }
            return lista;
        }
    }
}
