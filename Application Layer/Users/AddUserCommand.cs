using MediatR;

public class AddUserCommand : IRequest<bool>
{
    public UserDto User {get; set; }

    public AddUserCommand(UserDto user)
    {
        User = user;
    }

    public class Handler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = request.User;
            if (await _userRepository.GetUserByEmailAsync(userDto.Email) != null || await _userRepository.GetUserByNameAsync(userDto.Name) != null)
            {
                // User with the same email or name already exists
                return false;
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password
            };

            return await _userRepository.AddUserAsync(user);
        }
    }
}