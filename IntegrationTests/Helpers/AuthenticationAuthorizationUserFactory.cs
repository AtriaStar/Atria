using Backend.AspPlugins;
using Backend.Services;
using DatabaseMocker;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Helpers
{
    public class AuthenticationAuthorizationUserFactory
    {
        AtriaContext _context;
        string masterPassword = "12345";
        byte[] masterSalt = new byte[64];
        byte[] masterHashedPassword = new byte[128];
        string masterToken = "12345";


        public AuthenticationAuthorizationUserFactory(AtriaContext context)
        {
            _context = context;

            masterSalt = HashingService.GenerateSalt();
            masterHashedPassword = HashingService.Hash(masterPassword, masterSalt);
        }

        public async Task<(User, Session)> GetAuthenticatedUser()
        {
            User user = new User()
            {
                FirstNames = "Max",
                LastName = "Mustermann",
                PasswordHash = masterHashedPassword,
                PasswordSalt = masterSalt,
                Email = "authenticatedUser@email.com",
                SignUpIp = "127.0.0.1",
            };


            Session session = new Session()
            {
                User = user,
                Ip = "127.0.0.1",
                Token = masterToken,
                UserAgent = "",
                CreatedAt = DateTimeOffset.UtcNow,
            };

            await _context.Users.AddAsync(user);
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
            return (user, session);
        }
    }
}
