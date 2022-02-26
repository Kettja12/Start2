﻿using Microsoft.EntityFrameworkCore;
using Start2.Shared.Model.Account;
using Start2.Shared.Model.Dashboard;

namespace Start2.Server.DBContext;

public partial class StartContext
{
    public async Task<User?> GetUserByIdAsync(int id)
    {
        User? user = await Users
            .FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        User? user = await Users
            .FirstOrDefaultAsync(x => x.Username == username);
        return user;
    }
    public async Task<List<User>?> GetUsersByUsernameAsync(string username)
    {
        List<User>? user = await Users.Where(x => x.Username == username)
            .ToListAsync();
        return user;
    }
    public async Task<List<User>?> GetUsersByLastNameAsync(string lastname)
    {
        return await Users.Where(x => x.LastName.Contains(lastname))
            .OrderBy(x => x.LastName).ThenBy(x=>x.FirstName)
            .Take(20)
            .ToListAsync();
    }


    public async Task<User> SaveUserAsync(User user, User? existingUser)
    {

        if (existingUser != null)
        {
            Entry(existingUser).State = EntityState.Detached;
        }
        Entry(user).State = user.Id == 0 ? EntityState.Added : EntityState.Modified;
        await SaveChangesAsync();
        return user;
    }
    public async Task<LoginToken?> GetLoginTokenAsunc(int UserId)
    {
        return await LoginTokens.FirstOrDefaultAsync(x => x.UserId == UserId);
    }

    public async Task<LoginToken?> SaveLoginTokenAsync(int UserId, string token)
    {
        LoginToken? logintoken = await GetLoginTokenAsunc(UserId);
        if (logintoken == null)
        {
            logintoken = new LoginToken() { UserId = UserId, Value = token };
            LoginTokens.Add(logintoken);
        }
        else
        {
            logintoken.Value = token;
        }
        await SaveChangesAsync();
        return logintoken;
    }

    public async Task<List<Claim>> GetClaimsByUserIdAsync(int userId)
    {
        if (Claims != null)
        {
            List<Claim>? result = await Claims.Where(x => x.UserId == userId).ToListAsync();
            if (result != null)
                return result;
        }
        return new List<Claim>();
    }

    public async Task<Claim> SaveClaimAsync(Claim claim)
    {
        if (claim != null)
        {
            if (claim.Id > 0)
            {
                Claim? oldclaim = await Claims.FirstOrDefaultAsync(x => x.Id == claim.Id);
                if (oldclaim! != null)
                {
                    oldclaim.ClaimValue = claim.ClaimValue;
                }
            }
            else
            {
                await AddAsync(claim);
            }

            await SaveChangesAsync();
            return claim;
        }
        return new Claim();
    }

}
