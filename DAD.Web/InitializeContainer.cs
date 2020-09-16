using SimpleInjector;
using SimpleInjector.Integration.Web;
using DAD.DataAccess.ConnectionBD;
using System.Configuration;

namespace DAD.Web
{
    public class InitializeContainer
    {
        public static Container Container = new Container();
        public static Container ContainerWepApi = new Container();

        public static void Start()
        {
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            Container.Register<AdoHelper>(() =>
            {
                return new AdoHelper(ConfigurationManager.ConnectionStrings["ProyectoDADBDConnectionString"].ConnectionString);
            }, Lifestyle.Scoped);

            Container.Verify();

            AdoHelper.ConnectionString = ConfigurationManager.ConnectionStrings["ProyectoDADBDConnectionString"].ConnectionString;
        }
    }
}