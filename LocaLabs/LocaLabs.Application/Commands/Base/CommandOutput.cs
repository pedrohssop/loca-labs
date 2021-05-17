using System;

namespace LocaLabs.Application.Commands.Base
{
    public class CommandOutput<T>
    {
        public T Data { get; }
        public bool IsValid { get; }
        public bool HasErrors { get; }
        public Exception Error { get; }

        public CommandOutput(Exception err)
        {
            Error = err;
            Data = default;
            IsValid = false;
            HasErrors = true;
        }

        public CommandOutput(T data)
        {
            Data = data;
            IsValid = true;
        }

        internal CommandOutput()
        {
            IsValid = false;
        }

        public static implicit operator CommandOutput<T>(T data) =>
            new CommandOutput<T>(data);

        public static implicit operator CommandOutput<T>(bool value) =>
            new CommandOutput<T>();

        public static implicit operator CommandOutput<T>(Exception value) =>
            new CommandOutput<T>(value);
    }
}