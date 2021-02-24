using CommandResponse.Core;
using CommandResponse.Samples.Users;

namespace CommandResponse.Samples
{
    public class GetUserCommand
    {
        private readonly UserRepository _userRepository;

        public GetUserCommand(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CommandResponse<User, string> GetUser(string username)
        {
            var user = _userRepository.GetUser(username);

            if (user is null)
                CommandResponse<User, string>.FromFailure("USER.NOT.FOUND", user);

            return CommandResponse<User, string>.FromSuccess(user);
        }

        public CommandResponse<User> GetUserWriteNoResult(string username)
        {
            var user = _userRepository.GetUser(username);

            if (user is null)
                CommandResponse<User>.FromFailure("USER.NOT.FOUND", user);

            return CommandResponse<User>.FromSuccess(user);
        }
    }
}