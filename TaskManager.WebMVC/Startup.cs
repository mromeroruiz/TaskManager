using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskManager.WebMVC.Startup))]
namespace TaskManager.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
