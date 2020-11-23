using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiringGame.DataModel
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public AppUserConfigurations()
        {

        }

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var admin = new AppUser
            {
                Id = 1,
                UserName = "Admin@Admin.com",
                NormalizedUserName = "Admin@Admin.com",
                FirstName = "Ahmed",
                LastName = "Sakr",
                Email = "Admin@Admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                PhoneNumber = "01202224055",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D"),
            };

            admin.PasswordHash = PassGenerate(admin);

            builder.HasData(admin);
        }
        public string PassGenerate(AppUser user)
        {
            var passHash = new PasswordHasher<AppUser>();
            return passHash.HashPassword(user, "admin");
        }
    }
}
