using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SeleniumDemo.Models;

namespace SeleniumDemo.IntegrationTests.KnownData
{
    /// <summary>
    /// Generates known account data using for logins.
    /// </summary>
    public class AccountDataGenerator
    {
        /// <summary>
        /// Ensure the specified username exists in the database - if not add with specified password.
        /// </summary>
        /// <param name="username">The username to ensure.</param>
        /// <param name="password">The login password to use when creating missing account.</param>
        public void EnsureKnownAccount(string username, string password)
        {
            using (var applicationDbContext = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(applicationDbContext));
                var userExists = userManager.Users.Any(u => u.UserName == username);
                if (userExists)
                {
                    return;
                }

                var user = new ApplicationUser { Email = username, UserName = username };
                userManager.Create(user, password);
            }
        }
    }
}