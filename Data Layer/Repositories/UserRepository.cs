using Microsoft.EntityFrameworkCore;
using TriadInterviewBackend.DataLayer.Contexts;
using TriadInterviewBackend.DataLayer.Entities;
using TriadInterviewBackend.DomainLayer.Aggregates;
using TriadInterviewBackend.DomainLayer.Contracts;

namespace TriadInterviewBackend.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TriadDbContext _context;

        public UserRepository(TriadDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return null;

            return EntityToAggregate(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if (user == null)
                return null;

            return EntityToAggregate(user);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

            if (user == null)
                return null;

            return EntityToAggregate(user);
        }

        public async Task<bool> AddUserAsync(User userToAdd)
        {
            var user = AggregateToEntity(userToAdd);
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return false;
                
            }
            return true;
        }

        public async Task<bool> UpdateUserAsync(User userToUpdate)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userToUpdate.Id);

            user.Name = userToUpdate.Name;
            user.Email = userToUpdate.Email;
            user.Password = userToUpdate.Password;

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return false;
                
            }
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return false;

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return false;
            }
            return true;
        }

        private User EntityToAggregate(UserEntity user)
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            };
        }

        private UserEntity AggregateToEntity(User user)
        {
            return new UserEntity
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            };
        }
    }
}
