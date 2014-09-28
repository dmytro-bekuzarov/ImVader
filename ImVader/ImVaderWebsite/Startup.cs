using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImVaderWebsite.Startup))]
namespace ImVaderWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
