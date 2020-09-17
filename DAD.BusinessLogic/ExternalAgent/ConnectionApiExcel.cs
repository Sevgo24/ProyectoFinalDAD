using DAD.DataAccess.ConnectionBD;
using DAD.DataAccess.ConstantesGenerales;
using DAD.DataAccess.Entities;
using DAD.DataAccess.Repositories;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DAD.BusinessLogic.ExternalAgent
{
    public class ConnectionApiExcel
    {
        AlumnoDA alumnoDA = new AlumnoDA();
        RespuestaDA respuestaDA = new RespuestaDA();
        AdoHelper adoHelper = new AdoHelper();

        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "ProyectoFinalDAD";

        public void Excel()
        {
            var lista = new List<AlumnoBE>(); 

            UserCredential credential;
            using (var stream = new FileStream(@"D:\GIT PROYECTOS PERSONALES\GIT DAD\ProyectoFinalDAD\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string creadPath = @"D:\token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None,
                    new FileDataStore(creadPath, true)).Result;
            }
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
            string spreadsheetId = "141PdF8dYokUz2XXGV6MKq5R69KDgz3YFdfrUoTKCEhw";
            
            // Insertar Alumno del Api Excel
            ObtenerDatosAlumnoPorApiExcel(spreadsheetId, service);

            //Insertar Respuesta Por Dimension 
            ObtenerDatosRespuestaPorApiExcel(spreadsheetId, service);
        }

        #region Privado Insetar Respuesta de API EXCEL
        private void ObtenerDatosRespuestaPorApiExcel(string spreadsheetId, SheetsService service)
        {
            var listaTotalRespuesta = new List<RespuestaBE>();
            // CONFIABILIDAD
            var listaTemporalConfiabilidad = ListaTemporalRespuestaConfiabilidad(spreadsheetId, service, ConstanteDb.Dimensiones.CONFIABILIDAD);
            // SEGURIDAD
            var listaTemporalSeguridad = ListaTemporalRespuestaSeguridad(spreadsheetId, service, ConstanteDb.Dimensiones.SEGURIDAD);
            // TANGIBILIDAD
            var listaTemporalTangibilidad = ListaTemporalRespuestaTangibilidad(spreadsheetId, service, ConstanteDb.Dimensiones.TANGIBILIDAD);
            // CAPACIDAD DE RESPUESTA
            var listaTemporalCapacidadRespuesta = ListaTemporalRespuestaCapacidadRespuesta(spreadsheetId, service, ConstanteDb.Dimensiones.CAPACIDAD_DE_RESPUESTA);
            // EMPATIA
            var listaEmpatia = ListaTemporalRespuestaEmpatia(spreadsheetId, service, ConstanteDb.Dimensiones.EMPATIA);


            //Liberar Espacio No Duplicados e Insertar:
            foreach (var item in listaTemporalConfiabilidad) listaTotalRespuesta.Add(item); 
            foreach (var item in listaTemporalSeguridad) listaTotalRespuesta.Add(item); 
            foreach (var item in listaTemporalTangibilidad) listaTotalRespuesta.Add(item); 
            foreach (var item in listaTemporalCapacidadRespuesta) listaTotalRespuesta.Add(item); 
            foreach (var item in listaEmpatia) listaTotalRespuesta.Add(item);

            LiberarEspacio(listaTotalRespuesta);
        }

        #region Respuesta por CONFIABILIDAD
        private List<RespuestaBE> ListaTemporalRespuestaConfiabilidad(string spreadsheetId, SheetsService service, int dimension)
        {
            var listaRespuestaConfiabilidad = new List<RespuestaBE>();

            var range = "Form Responses 1!C2:K";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();

            IList<IList<Object>> values = response.Values;


            if (values != null && values.Count > 0)
            {

                foreach (var row in values)
                {
                    listaRespuestaConfiabilidad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[6].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[6]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaConfiabilidad.Pregunta_1,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaConfiabilidad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[7].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[7]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaConfiabilidad.Pregunta_2,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaConfiabilidad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[8].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[8]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaConfiabilidad.Pregunta_3,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                }
            }
            return listaRespuestaConfiabilidad;
        }
        #endregion

        #region Respuesta por SEGURIDAD
        private List<RespuestaBE> ListaTemporalRespuestaSeguridad(string spreadsheetId, SheetsService service, int dimension)
        {
            var listaRespuestaSeguridad = new List<RespuestaBE>();

            var range = "Form Responses 1!C2:P";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();
            IList<IList<Object>> values = response.Values;

            if (values != null && values.Count > 0)
            {

                foreach (var row in values)
                {
                    listaRespuestaSeguridad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[9].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[9]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaSeguridad.Pregunta_4,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaSeguridad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[10].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[10]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaSeguridad.Pregunta_5,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaSeguridad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[11].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[11]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaSeguridad.Pregunta_6,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaSeguridad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[12].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[12]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaSeguridad.Pregunta_7,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaSeguridad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[13].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[13]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaSeguridad.Pregunta_8,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                }
            }
            return listaRespuestaSeguridad;
        }
        #endregion

        #region Respuesta por TANGIBILIDAD
        private List<RespuestaBE> ListaTemporalRespuestaTangibilidad(string spreadsheetId, SheetsService service, int dimension)
        {
            var listaRespuestaTangibilidad = new List<RespuestaBE>();

            var range = "Form Responses 1!C2:U";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();
            IList<IList<Object>> values = response.Values;

            if (values != null && values.Count > 0)
            {

                foreach (var row in values)
                {
                    listaRespuestaTangibilidad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[14].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[14]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaTangibilidad.Pregunta_9,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaTangibilidad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[15].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[15]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaTangibilidad.Pregunta_10,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaTangibilidad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[16].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[16]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaTangibilidad.Pregunta_11,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaTangibilidad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[17].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[17]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaTangibilidad.Pregunta_12,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaTangibilidad.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[18].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[18]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaTangibilidad.Pregunta_13,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                }
            }
            return listaRespuestaTangibilidad;
        }
        #endregion

        #region Respuesta por CAPACIDAD DE RESPUESTA
        private List<RespuestaBE> ListaTemporalRespuestaCapacidadRespuesta(string spreadsheetId, SheetsService service, int dimension)
        {
            var listaRespuestaCapacidadRespuesta = new List<RespuestaBE>();

            var range = "Form Responses 1!C2:Y";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();
            IList<IList<Object>> values = response.Values;

            if (values != null && values.Count > 0)
            {

                foreach (var row in values)
                {
                    listaRespuestaCapacidadRespuesta.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[19].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[19]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaCapacidadRespuesta.Pregunta_14,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaCapacidadRespuesta.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[20].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[20]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaCapacidadRespuesta.Pregunta_15,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaCapacidadRespuesta.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[21].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[21]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaCapacidadRespuesta.Pregunta_16,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaCapacidadRespuesta.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = row[22].ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(row[22]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaCapacidadRespuesta.Pregunta_17,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                }
            }
            return listaRespuestaCapacidadRespuesta;
        }
        #endregion

        #region Respuesta por EMPATIA
        private List<RespuestaBE> ListaTemporalRespuestaEmpatia(string spreadsheetId, SheetsService service, int dimension)
        {
            var listaRespuestaEmpatia = new List<RespuestaBE>();

            var range = "Form Responses 1!C2:AC";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();
            IList<IList<Object>> values = response.Values;

            if (values != null && values.Count > 0)
            {

                foreach (var row in values)
                {
                    listaRespuestaEmpatia.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = Convert.ToInt32(row[23]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaEmpatia.Pregunta_18,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaEmpatia.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = Convert.ToInt32(row[24]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaEmpatia.Pregunta_19,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaEmpatia.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = Convert.ToInt32(row[25]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaEmpatia.Pregunta_20,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                    listaRespuestaEmpatia.Add(new RespuestaBE()
                    {
                        PUNTAJERESPUESTA = Convert.ToInt32(row[26]),
                        FECHAREGISTRO = DateTime.Now,
                        CODPREGUNTA = ConstanteDb.PreguntaEmpatia.Pregunta_21,
                        CODALUMNO = row[0].ToString(),
                        CODDIMENSION = dimension
                    });
                }
            }
            return listaRespuestaEmpatia;
        }
        #endregion


        private void LiberarEspacio(List<RespuestaBE> listRespuestas)
        {
            respuestaDA.LiberarEspacioRespuesta();
            foreach (var respuestas in listRespuestas) respuestaDA.InsertarRespuestaApiExcel(respuestas);
        }

        #endregion

        #region Metodos Privado Insertar Alumnos de API EXCEL
        private void ObtenerDatosAlumnoPorApiExcel(string spreadsheetId, SheetsService service)
        {
            var lista = new List<AlumnoBE>();
            var range = "Form Responses 1!A2:G";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    lista.Add(new AlumnoBE()
                    {
                        FECHAREGISTRO = Convert.ToDateTime(row[0].ToString()),
                        CORREOALUMNO = row[1].ToString(),
                        CODALUMNO = row[2].ToString(),
                        NOMBREESCUELA = row[3].ToString(),
                        SEXOALUMNO = row[4].ToString(),
                        CICLOALUMNO = row[5].ToString(),
                        TRABAJOALUMNO = row[6].ToString().Equals("No") ? 0 : 1
                    });
                }
                using (var scope = adoHelper.BeginTransaction())
                {
                    foreach (var item in lista) InsertarAlumno(item);
                    scope.Commit();
                }
            }
        }

        private void InsertarAlumno(AlumnoBE alumno)
        {
            if (alumno.CODALUMNO.Length < 9)
            {
                var cantidad = alumnoDA.ValidarAlumnoPorCodigoAlumno(alumno.CODALUMNO);
                if (cantidad == 0) alumnoDA.InsertarAlumnoApiExcel(alumno);
            }
        }
        #endregion
    }
}
