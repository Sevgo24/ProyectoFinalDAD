using DAD.DataAccess.Entities;
using DAD.DataAccess.Repositories;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAD.BusinessLogic.ExternalAgent
{
    public class ConnectionApiExcel
    {
        AlumnoDA alumnoDA = new AlumnoDA();

        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "ProyectoFinalDAD";

        public void Excel()
        {
            var lista = new List<AlumnoBE>(); 

            UserCredential credential;
            using (var stream = new FileStream(@"D:\GIT PROYECTOS PERSONALES\GIT DAD\ProyectoFinalDAD\DAD.Web\bin\credentials.json", FileMode.Open, FileAccess.Read))
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
            String spreadsheetId = "141PdF8dYokUz2XXGV6MKq5R69KDgz3YFdfrUoTKCEhw";
            String range = "Form Responses 1!A2:G";
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

                foreach (var item in lista)
                {
                    InsertarAlumno(item);
                }
            }
            else
            {

            }



        }

        private void InsertarRespuesta()
        {

        }

        private void InsertarAlumno(AlumnoBE alumno)
        {
            alumnoDA.InsertarAlumnoApiExcel(alumno);
        }
    }
}
