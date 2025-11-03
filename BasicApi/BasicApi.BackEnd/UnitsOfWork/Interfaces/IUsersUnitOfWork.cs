using BasicApi.Shared.DTOs;
using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace BasicApi.BackEnd.UnitsOfWork.Interfaces;

public interface IUsersUnitOfWork
{
    Task<SignInResult> LoginAsync(LoginDTO model);

    Task LogoutAsync();

    Task<User> GetUserAsync(string email);

    Task<IdentityResult> AddUserAsync(User user, string password);

    Task CheckRoleAsync(string roleName);

    Task AddUserToRoleAsync(User user, string roleName);

    Task<bool> IsUserInRoleAsync(User user, string roleName);
}