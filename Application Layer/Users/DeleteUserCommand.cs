using MediatR;

public class DeleteUserCommand : IRequest<bool>
{
    public int UserId { get; set; }

    public DeleteUserCommand(int userId)
    {
        UserId = userId;
    }

    public class Handler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
    
        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(request.UserId);
            if (existingUser == null)
            {
                return false; // User not found
            }
    
            return await _userRepository.DeleteUserAsync(request.UserId);
        }
    }
}   