namespace NCSafety.DAL.IdentityEntities.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NCSafety.DAL.IdentityEntities.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\IdentityEntities\Migrations";
        }

        protected override void Seed(NCSafety.DAL.IdentityEntities.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Create a Role Manager
            var roleManager = new RoleManager<IdentityRole>(new
                                          RoleStore<IdentityRole>(context));
            //Create Role Admin if it does not exist
            if (!context.Roles.Any(r => r.Name == "SuperAdmin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("SuperAdmin"));
            }
            //Create Role Associative Dean if it does not exist
            if (!context.Roles.Any(r => r.Name == "AssociativeDean"))
            {
                var roleresult = roleManager.Create(new IdentityRole("AssociativeDean"));
            }

            //Create Role Academic Business Manager if it does not exist
            if (!context.Roles.Any(r => r.Name == "AcademicBusinessManager"))
            {
                var roleresult = roleManager.Create(new IdentityRole("AcademicBusinessManager"));
            }

            //Create a User Manager
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //Now the Admin user named admin1 with password password
            var adminuser = new ApplicationUser
            {
                UserName = "superadmin1@outlook.com",
                Email = "superadmin1@outlook.com"
            };

            //Assign admin user to role
            if (!context.Users.Any(u => u.UserName == "superadmin1@outlook.com"))
            {
                manager.Create(adminuser, "Password1");
                manager.AddToRole(adminuser.Id, "SuperAdmin");
            }

            //Now the asc. dean user named ascdean1 with password Password1
            var assdeanuser = new ApplicationUser
            {
                UserName = "ascdean1@outlook.com",
                Email = "ascdean1@outlook.com"
            };

            //Assign assdean  user to role
            if (!context.Users.Any(u => u.UserName == "ascdean1@outlook.com"))
            {
                manager.Create(assdeanuser, "Password1");
                manager.AddToRole(assdeanuser.Id, "AssociativeDean");
            }

            //Now the ac. bus. manager user named manager1 with password Password1
            var busadminuser = new ApplicationUser
            {
                UserName = "manager1@outlook.com",
                Email = "manager1@outlook.com"
            };

            //Assign ac. bus. manager  user to role
            if (!context.Users.Any(u => u.UserName == "manager1@outlook.com"))
            {
                manager.Create(busadminuser, "Password1");
                manager.AddToRole(busadminuser.Id, "AcademicBusinessManager");
            }

            //Create a few generic users
            for (int i = 1; i <= 2; i++)
            {
                var tech = new ApplicationUser
                {
                    UserName = string.Format("tech{0}@outlook.com", i.ToString()),
                    Email = string.Format("tech{0}@outlook.com", i.ToString())
                };
                if (!context.Users.Any(u => u.UserName == tech.UserName))
                    manager.Create(tech, "Password1");
            }
        }
    }
}
