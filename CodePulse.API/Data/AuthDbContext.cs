﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        //create constructor
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "53eb5141-7eb3-4719-8023-2ff00d7449f2";
            var writterRoleId ="37718b17-9483-497a-995b-f9388c0288cd";

            // create reader & writter role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                 new IdentityRole()
                {
                    Id = writterRoleId,
                    Name = "Writter",
                    NormalizedName = "Writter".ToUpper(),
                    ConcurrencyStamp = writterRoleId
                },
            };
            
            // seed roles
            builder.Entity<IdentityRole>().HasData(roles);

            // create admin user
            var adminUserId = "821c4bb9-a23d-4df3-a719-c7cebb7579fa";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepluse.com",
                Email = "admin@codepluse.com",
                NormalizedEmail = "admin@codepluse.com".ToUpper(),
                NormalizedUserName = "admin@codepluse.com".ToUpper()
            };


            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            // give toles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writterRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
