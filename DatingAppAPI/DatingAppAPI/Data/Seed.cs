using DatingAppAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingAppAPI.Data
{
    public class Seed
    {
        public static async Task SeedUser(DataContext context)
        {
            // Retrn if User data exist in the DB
            if (await context.Users.AnyAsync()) { return; }
            // Get data test from Data/UserSeedData.json
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            // Ignore case differences between the prop nama in the JSON being deserizlized
            // and prop name in the target C# class
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            // deserializes JSON data into a list of 'AppUser' objects, using specified serializ option
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

            foreach ( var user in users )
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
