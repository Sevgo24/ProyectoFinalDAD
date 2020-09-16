using SimpleInjector;
using SimpleInjector.Integration.Web;
using DAD.DataAccess.ConnectionBD;
using System.Configuration;
using DAD.BusinessLogic.ExternalAgent;

namespace DAD.Web
{
    public class InitializeContainer
    {
        public static Container Container = new Container();
        public static Container ContainerWepApi = new Container();

        public static void Start()
        {
            //Obtener configuración de API EXCEL   
            var _ApplicationName = ConfigurationManager.AppSettings["ApiExcel:ApplicationName"];
            var _RutaCredencial = ConfigurationManager.AppSettings["ApiExcel:RutaCredencial"];
            var _RutaTokenJson = ConfigurationManager.AppSettings["ApiExcel:RutaTokenJson"];
            var _SpreadsheetId = ConfigurationManager.AppSettings["ApiExcel:SpreadsheetId"];

            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            //Container.RegisterInitializer<ConnectionApiExcel>((push) =>
            //{
            //    push.V_ApplicationName = _ApplicationName;
            //    push.V_RutaCredencial = _RutaCredencial;
            //    push.V_RutaTokenJson = _RutaTokenJson;
            //    push.V_SpreadsheetId = _SpreadsheetId;
            //});
            Container.Register<AdoHelper>(() =>
            {
                return new AdoHelper(ConfigurationManager.ConnectionStrings["ProyectoDADBDConnectionString"].ConnectionString);
            }, Lifestyle.Scoped);

            Container.Verify();

            AdoHelper.ConnectionString = ConfigurationManager.ConnectionStrings["ProyectoDADBDConnectionString"].ConnectionString;
        }
    }
}