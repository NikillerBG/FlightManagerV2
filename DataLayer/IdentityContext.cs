using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;

namespace DataLayer
{
    public class IdentityContext
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public IdentityContext(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region Seed Данни

        public async Task SeedDataAsync(string adminPass, string adminEmail)
        {
            if (!await _context.Users.AnyAsync())
            {
                await CreateAdminAccountAsync(adminPass, adminEmail);
            }
        }

        private async Task CreateAdminAccountAsync(string password, string email)
        {
            var adminUser = new User("admin", "Admin", "Admin", "0000000000", "Default Address");
            IdentityResult createResult = await _userManager.CreateAsync(adminUser, password);
            if (!createResult.Succeeded)
            {
                throw new Exception("Неуспешно създаване на администраторски акаунт: " +
                    string.Join(", ", createResult.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(adminUser, Role.Administrator.ToString());
            await _userManager.SetEmailAsync(adminUser, email);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region CRUD Операции

        public async Task<Tuple<IdentityResult, User?>> CreateUserAsync(string username, string password, string firstName, string lastName, string egn, string address, Role role)
        {
            try
            {
                User user = new User(username, firstName, lastName, egn, address);
                IdentityResult result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    return new Tuple<IdentityResult, User?>(result, user);
                }

                if (role == Role.Administrator)
                {
                    await _userManager.AddToRoleAsync(user, Role.Administrator.ToString());
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Role.User.ToString());
                }
                return new Tuple<IdentityResult, User?>(IdentityResult.Success, user);
            }
            catch (Exception ex)
            {
                IdentityResult result = IdentityResult.Failed(new IdentityError { Code = "Registration", Description = ex.Message });
                return new Tuple<IdentityResult, User?>(result, null);
            }
        }

        public async Task<User?> LogInUserAsync(string username, string password)
        {
            try
            {
                User? user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return null;
                }

                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
                return isPasswordValid ? user : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> ReadUserAsync(string id, bool useNavigationalProperties = false)
        {
            try
            {
                return await _userManager.FindByIdAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> ReadAllUsersAsync(bool useNavigationalProperties = false)
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUserAsync(string id, string username, string firstName, string lastName, string egn, string address)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    User user = await _context.Users.FindAsync(id);
                    user.UserName = username;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.EGN = egn;
                    user.Address = address;
                    await _userManager.UpdateAsync(user);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserByNameAsync(string username)
        {
            try
            {
                User user = await FindUserByNameAsync(username);

                if (user == null)
                {
                    throw new InvalidOperationException("User not found for deletion!");
                }

                await _userManager.DeleteAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> FindUserByNameAsync(string username)
        {
            try
            {
                return await _userManager.FindByNameAsync(username);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
