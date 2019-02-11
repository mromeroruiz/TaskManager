using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TaskManager.Data;

[assembly: OwinStartupAttribute(typeof(TaskManager.WebMVC.Startup))]
namespace TaskManager.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Create First Admin Role and Default Admin User

            if (!roleManager.RoleExists("Admin"))
            {
                //Create Role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Create Admin Super User
                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "admin@admin.com";

                string userPWD = "ABC123";
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            //Creating user/manager Role
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }
            if(!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }

        }
    }
}
