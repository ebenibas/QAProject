using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QAWebsiteProject.Startup))]
namespace QAWebsiteProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
