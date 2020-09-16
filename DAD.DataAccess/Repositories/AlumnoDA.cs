using DAD.DataAccess.ConnectionBD;
using DAD.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAD.DataAccess.Repositories
{
    public class AlumnoDA
    {
        AdoHelper adoHelper = new AdoHelper();
        public List<AlumnoBandejaResultado> ListaUsuarioBandejaCms(string codAlumno)
        {
            var lista = new List<AlumnoBandejaResultado>();

            var codAlumnoParam = new SqlParameter("@CODALUMNO", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = (object)codAlumno ?? DBNull.Value };
           
            using (var adoHelper = new AdoHelper())
            {
                using (var reader = adoHelper.ExecDataReaderProc("[DBO].[USP_ALUMNOS_LIST]", codAlumnoParam))
                {
                    while (reader.Read()) lista.Add(ObtenerAlumnoBandeja(reader));
                }
            }
            return lista;
        }

        public void InsertarAlumnoApiExcel(AlumnoBE item)
        {
            var codAlumnoParam = new SqlParameter("@CODALUMNO", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = item.CODALUMNO };
            var correoParam = new SqlParameter("@CORREOALUMNO", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = (object)item.CORREOALUMNO ?? DBNull.Value };
            var sexoParam = new SqlParameter("@SEXOALUMNO", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = (object)item.SEXOALUMNO ?? DBNull.Value };
            var nombreEscuelaParam = new SqlParameter("@NOMBREESCUELA", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = (object)item.NOMBREESCUELA ?? DBNull.Value };
            var cicloAlumnoParam = new SqlParameter("@CICLOALUMNO", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = item.CICLOALUMNO };
            var trabajoAlumnoParam = new SqlParameter("@TRABAJOALUMNO", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = (object)item.TRABAJOALUMNO ?? DBNull.Value };
            var fechaRegistroParam = new SqlParameter("@FECHAREGISTRO", SqlDbType.DateTime) { Direction = ParameterDirection.Input, Value = item.FECHAREGISTRO };
            
            adoHelper.ExecNonQueryProc("[DBO].[USP_ALUMNOS_INS]", codAlumnoParam, correoParam, sexoParam, nombreEscuelaParam, cicloAlumnoParam, trabajoAlumnoParam, fechaRegistroParam);
        }

        public int ValidarAlumnoPorCodigoAlumno(string codAlumno)
        {
            var cantidad = 0;

            var codAlumnoParam = new SqlParameter("@CODALUMNO", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = (object)codAlumno ?? DBNull.Value };

            using (var adoHelper = new AdoHelper())
            {
                using (var reader = adoHelper.ExecDataReaderProc("[DBO].[USP_VALIDAREXISTENCIAALUMNO_READ]", codAlumnoParam))
                {
                    while (reader.Read()) cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                }
            }
            return cantidad;
        }

        #region Mapper
        private AlumnoBandejaResultado ObtenerAlumnoBandeja(SqlDataReader reader)
        {
            var item = new AlumnoBandejaResultado();
            item.CODALUMNO = reader["CODALUMNO"].ToString();
            item.CORREOALUMNO = reader["CORREOALUMNO"].ToString();
            item.SEXO = reader["SEXO"].ToString();
            item.FECHAREGISTRO = ((DateTime)reader["FECHAREGISTRO"]).ToString();
            item.NOMBREESCUELA = reader["NOMBREESCUELA"].ToString();
            item.CICLOALUMNO = reader["CICLOALUMNO"].ToString();
            item.TRABAJO = reader["TRABAJO"].ToString();
            return item;
        }
        #endregion
    }


}
