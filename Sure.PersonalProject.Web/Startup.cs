using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sure.PersonalProject.Web.Startup))]
namespace Sure.PersonalProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
