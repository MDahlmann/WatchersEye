using PoeLeagueTracker.Application.RepositoryInterfaces;
using PoeLeagueTracker.Domain.Users;

namespace PoeLeagueTracker.Application.Commands.CreateUserCommand
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepo;
        private readonly UserFactory _userFactory;

        public CreateUserCommandHandler(IUserRepository userRepo, UserFactory userFactory)
        {
            _userRepo = userRepo;
            _userFactory = userFactory;
        }

        public async Task<Guid> HandleAsync(CreateUserCommand command)
        {
            if (await _userRepo.UsernameExistsAsync(command.Username))
            {
                throw new ArgumentException("Username already exists.", nameof(command.Username));
            }

            var user = _userFactory.Create(command.Username, command.Password, command.Role);

            await _userRepo.AddUserAsync(user);
            await _userRepo.SaveChangesAsync();

            return user.Id;
        }
    }
}