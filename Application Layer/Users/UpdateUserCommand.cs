using MediatR;
using TriadInterviewBackend.ApplicationLayer.DTOs;
using TriadInterviewBackend.DomainLayer.Contracts;

namespace TriadInterviewBackend.ApplicationLayer.Users
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserDto User { get; set; }

        public UpdateUserCommand(UserDto user)
        {
            User = user;
        }

        public class Handler : IRequestHandler<UpdateUserCommand, bool>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var userDto = request.User;
                var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);
                if (existingUser == null)
                {
                    return false; // User not found
                }

                existingUser.Name = userDto.Name;
                existingUser.Email = userDto.Email;
                existingUser.Password = userDto.Password;

                return await _userRepository.UpdateUserAsync(existingUser);
            }
        }
    }
}