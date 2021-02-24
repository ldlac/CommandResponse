using CommandResponse.Samples.Users;
using System;
using System.Threading.Tasks;

namespace CommandResponse.Samples
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // DI

            var userRepository = new UserRepository();

            var command = new GetUserCommand(userRepository);

            WriteResponseToCase(command);

            ReturnResultCase(command);
        }

        private static void ReturnResultCase(GetUserCommand command)
        {
            var commandResponse = command.GetUser("User1");

            var response = commandResponse
                .OnSuccess(user =>
                {
                    return Task.FromResult(user.Username);
                })
                .OnFailure((error, user) =>
                {
                    return Task.FromResult(error);
                });

            Console.WriteLine(response);
        }

        private static void WriteResponseToCase(GetUserCommand command)
        {
            var commandResponseNoResult = command.GetUserWriteNoResult("User1");

            commandResponseNoResult
                .OnSuccess(user =>
                {
                    // Write to response request body

                    return Task.CompletedTask;
                })
                .OnFailure((error, user) =>
                {
                    // Write to response request body with specific code

                    return Task.CompletedTask;
                });
        }
    }
}