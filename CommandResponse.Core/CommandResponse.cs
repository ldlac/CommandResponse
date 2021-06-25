using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandResponse.Core
{
    public class CommandResponse<T>
    {
        private readonly T _entity;
        private readonly string _error;

        private readonly List<Func<T, Task>> _onSuccessHandlers;
        private readonly List<Func<string, T, Task>> _onFailureHandlers;

        public static CommandResponse<T> FromSuccess(T entity)
        {
            return new CommandResponse<T>(entity, null);
        }

        public static CommandResponse<T> FromFailure(string error)
        {
            return new CommandResponse<T>(error);
        }

        public static CommandResponse<T> FromFailure(string error, T entity)
        {
            return new CommandResponse<T>(entity, error);
        }

        private CommandResponse(string error)
        {
            _entity = default;
            _error = error;
            _onSuccessHandlers = new List<Func<T, Task>>();
            _onFailureHandlers = new List<Func<string, T, Task>>();
        }

        private CommandResponse(T entity, string error)
        {
            _entity = entity;
            _error = error;
            _onSuccessHandlers = new List<Func<T, Task>>();
            _onFailureHandlers = new List<Func<string, T, Task>>();
        }

        public CommandResponse<T> OnSuccess(Func<T, Task> onSuccessHandler)
        {
            _onSuccessHandlers.Add(onSuccessHandler);

            return this;
        }

        public CommandResponse<T> OnFailure(Func<string, T, Task> onFailureHandler)
        {
            _onFailureHandlers.Add(onFailureHandler);

            return this;
        }

        public async Task Resolve()
        {
            if (_error is null)
            {
                foreach (var onSuccessHandler in _onSuccessHandlers)
                {
                    await onSuccessHandler(_entity);
                }
            }
            else
            {
                foreach (var onFailureHandler in _onFailureHandlers)
                {
                    await onFailureHandler(_error, _entity);
                }
            }

            await Task.CompletedTask;
        }
    }

    public class CommandResponse<T, TResult>
    {
        private readonly T _entity;
        private readonly string _error;

        private readonly List<Func<T, Task<TResult>>> _onSuccessHandlers;
        private readonly List<Func<string, T, Task<TResult>>> _onFailureHandlers;

        public static CommandResponse<T, TResult> FromSuccess(T entity)
        {
            return new CommandResponse<T, TResult>(entity, null);
        }

        public static CommandResponse<T, TResult> FromFailure(string error)
        {
            return new CommandResponse<T, TResult>(error);
        }

        public static CommandResponse<T, TResult> FromFailure(string error, T entity)
        {
            return new CommandResponse<T, TResult>(entity, error);
        }

        private CommandResponse(string error)
        {
            _entity = default;
            _error = error;
            _onSuccessHandlers = new List<Func<T, Task<TResult>>>();
            _onFailureHandlers = new List<Func<string, T, Task<TResult>>>();
        }

        private CommandResponse(T entity, string error)
        {
            _entity = entity;
            _error = error;
            _onSuccessHandlers = new List<Func<T, Task<TResult>>>();
            _onFailureHandlers = new List<Func<string, T, Task<TResult>>>();
        }

        public CommandResponse<T, TResult> OnSuccess(Func<T, Task<TResult>> onSuccessHandler)
        {
            if (_onSuccessHandlers.Any())
                throw new CannotHaveMoreThanOneHookWhenResultsException();

            _onSuccessHandlers.Add(onSuccessHandler);

            return this;
        }

        public CommandResponse<T, TResult> OnFailure(Func<string, T, Task<TResult>> onFailureHandler)
        {
            if (_onFailureHandlers.Any())
                throw new CannotHaveMoreThanOneHookWhenResultsException();

            _onFailureHandlers.Add(onFailureHandler);

            return this;
        }

        public async Task<TResult> Resolve()
        {
            TResult result = default;
            if (_error is null)
            {
                foreach (var onSuccessHandler in _onSuccessHandlers)
                {
                    result = await onSuccessHandler(_entity);
                }
            }
            else
            {
                foreach (var onFailureHandler in _onFailureHandlers)
                {
                    result = await onFailureHandler(_error, _entity);
                }
            }

            return result;
        }
    }
}