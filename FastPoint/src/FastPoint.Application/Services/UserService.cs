using FastPoint.Application.Interfaces;
using FastPoint.Domain.Entities;

namespace FastPoint.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();

        public async Task<User?> GetByIdAsync(Guid id) => await _userRepository.GetByIdAsync(id);

        public async Task<User?> GetByEmailAsync(string email) => await _userRepository.GetByEmailAsync(email);

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("Usuário não encontrado.");
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
