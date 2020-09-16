using DAD.DataAccess.ConnectionBD;
using DAD.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAD.DataAccess.Repositories
{
    public class AlumnoDA
    {
        AdoHelper adoHelper = new AdoHelper();
        public List<AlumnoBandejaResultado> ListaUsuarioBandejaCms(string codAlumno)
        {
            var lista = new List<AlumnoBandejaResultado>();

            var codAlumnoParam = new SqlParameter("@Usua_NombreCompleto", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = (object)codAlumno ?? DBNull.Value };
           
            using (var adoHelper = new AdoHelper())
            {
                using (var reader = adoHelper.ExecDataReaderProc("[DBO].[USP_ALUMNOS_LIST]", codAlumnoParam))
                {
                    while (reader.Read()) lista.Add(ObtenerAlumnoBandeja(reader));
                }
            }
            return lista;
        }



        #region Mapper
        private AlumnoBandejaResultado ObtenerAlumnoBandeja(SqlDataReader reader)
        {
            var item = new AlumnoBandejaResultado();
            item.CICLOALUMNO = reader["CICLOALUMNO"].ToString();
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
