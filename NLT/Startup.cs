using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NLT.Startup))]
namespace NLT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
