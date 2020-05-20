using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
        public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>, IUserRoleStore<User>
        {
            private readonly DataContext db;
            public UserStore(DataContext db)
            {
                this.db = db;
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db?.Dispose();
                }
            }

            public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
            {
                return Task.FromResult(user.ID.ToString());
            }

            public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
            {
                return Task.FromResult(user.UserName);
            }

            public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
            {
                throw new NotImplementedException(nameof(SetUserNameAsync));
            }

            public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
            {
                throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
            }

            public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
            {
                return Task<bool>.Run(() =>
                {
                    user.UserName = normalizedName;
                });
            }

            public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
            {
                db.Add(user);

                //await db.SaveChangesAsync(cancellationToken);

                return await Task.FromResult(IdentityResult.Success);
            }

            public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
            {
                try
                {
                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return IdentityResult.Failed();
                }
                return IdentityResult.Success;
            }

            public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
            {
                db.Remove(user);

                int i = await db.SaveChangesAsync(cancellationToken);

                return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
            }

            public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
            {
                if (int.TryParse(userId, out int id))
                {
                    return await db.Users.FindAsync(id);
                }
                else
                {
                    return await Task.FromResult((User)null);
                }
            }

            public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
            {
                return db.Users.Where(p => p.UserName.Equals(normalizedUserName)).FirstOrDefault();
            }

            public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
            {
                user.PasswordHash = passwordHash;

                return Task.FromResult((object)null);
            }

            public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
            {
                return Task.FromResult(user.PasswordHash);
            }

            public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
            {
                return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
            }

            public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
            {
                return Task<bool>.Run(() =>
                {
                    user.Email = email;
                });
            }

            public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
            {
                return Task.FromResult(user.Email);
            }

            public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
            {
                return Task.FromResult(db.Users.Where(p => p.Email.Equals(normalizedEmail, StringComparison.OrdinalIgnoreCase)).FirstOrDefault());
            }

            public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
            {
                return Task<bool>.Run(() =>
                {
                    return user.Email;
                });
            }

            public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
            {
                return Task<bool>.Run(() =>
                {
                    user.Email = normalizedEmail;
                });
            }

            public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
            {
                var role = db.Roles.Where(x => x.Name.Equals(roleName)).FirstOrDefault();
                // var isIn =IsInRoleAsync(user, roleName, cancellationToken);
                if (role != null)// || isIn==Task.FromResult(false))
                {
                    UserRole userRole = new UserRole() { UserId = user.ID, RoleId = role.Id };
                    db.UserRole.Add(userRole);
                    db.SaveChanges();
                    return Task.CompletedTask;
                }
                return Task.FromResult((User)null);

            }

            public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
            {
                return Task.FromResult<IList<string>>(new List<string>(user.UserRole.Select(r => r.Role.Name).ToList()));
            }

            public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
            {
                bool inRole = false;
                var role = db.Roles.Where(x => x.Name.Equals(roleName)).FirstOrDefault();
                if (role != null)
                {
                    var result = db.UserRole.Where(x => x.UserId.Equals(user.ID) && x.RoleId.Equals(role.Id)).FirstOrDefault();
                    inRole = result != null;
                }
                return Task.FromResult<bool>(inRole);
            }



            public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
            {
                IList<User> users = new List<User>();
                var role = db.Roles.Where(x => x.Name.Equals(roleName)).FirstOrDefault();
                if (role != null)
                {
                    var result = db.UserRole.Where(x => x.RoleId.Equals(role.Id));
                    foreach (var ans in result)
                    {
                        users.Add(ans.User);
                    }
                }
                return Task.FromResult<IList<User>>(users);
            }
        }
}

