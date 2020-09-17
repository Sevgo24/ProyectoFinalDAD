using DAD.DataAccess.ConnectionBD;
using DAD.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAD.DataAccess.Repositories
{
    public class RespuestaDA
    {
        AdoHelper adoHelper = new AdoHelper();

        public List<RespuestaBandejaResultado> ListarRespuestaBandejaCms(string codAlumno, int codDimension)
        {
            var lista = new List<RespuestaBandejaResultado>();
            var codAlumnoParam = new SqlParameter("@CODALUMNO", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = (object)codAlumno ?? DBNull.Value };
            var codDimensionParam = new SqlParameter("@CODDIMENSION", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = (object)codDimension ?? DBNull.Value };
            using (var adoHelper = new AdoHelper())
            {
                using (var reader = adoHelper.ExecDataReaderProc("[DBO].[USP_RESPUESTA_LIST]", codAlumnoParam, codDimensionParam))
                {
                    while (reader.Read()) lista.Add(new RespuestaBandejaResultado()
                    {
                        CODALUMNO = reader["CODALUMNO"].ToString(),
                        CORREOALUMNO = reader["CORREOALUMNO"].ToString(),
                        FECHAREGISTRO = reader["FECHAREGISTRO"].ToString(),
                        NOMBREESCUELA = reader["NOMBREESCUELA"].ToString(),
                        NOMBREDIMENSION = reader["NOMBREDIMENSION"].ToString(),
                        CICLOALUMNO = reader["CICLOALUMNO"].ToString()
                    });
                }
            }
            return lista;
        }

        public void InsertarRespuestaApiExcel(RespuestaBE item)
        {
            var puntajeRespuestaParam = new SqlParameter("@PUNTAJERESPUESTA", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = (object)item.PUNTAJERESPUESTA ?? DBNull.Value };
            var fechaRegistroParam = new SqlParameter("@FECHAREGISTRO", SqlDbType.DateTime2) { Direction = ParameterDirection.Input, Value = (object)item.FECHAREGISTRO ?? DBNull.Value };
            var codAlumnoParam = new SqlParameter("@CODALUMNO", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = (object)item.CODALUMNO ?? DBNull.Value };
            var codPreguntaParam = new SqlParameter("@CODPREGUNTA", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = (object)item.CODPREGUNTA ?? DBNull.Value };
            var codDimensionParam = new SqlParameter("@CODDIMENSION", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = (object)item.CODDIMENSION ?? DBNull.Value };
           
            adoHelper.ExecNonQueryProc("[DBO].[USP_RESPUESTA_INS]", puntajeRespuestaParam, fechaRegistroParam, codAlumnoParam, codPreguntaParam, codDimensionParam);
        }

        public void LiberarEspacioRespuesta()
        {
            adoHelper.ExecNonQueryProc("[DBO].[USP_LIBERARESPACIORESPUESTA_DEL]");
        }
    }
}
