using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;

namespace DAD.Web.Models
{
    public class ApiExcel
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "ProyectoFinalDAD";

        public void Excel()
        {
            UserCredential credential;
            using (var stream = new FileStream(@"D:\credentials.json", FileMode.Open, FileAccess.Read))
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
            String range = "Form Responses 1!A:A";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);

            var response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {

                }
            }
            else
            {

            }
        }
    }
}