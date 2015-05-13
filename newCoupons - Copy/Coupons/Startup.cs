using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Coupons.Startup))]
namespace Coupons
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
