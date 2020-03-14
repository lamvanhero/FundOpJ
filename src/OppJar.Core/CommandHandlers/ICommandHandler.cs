namespace OppJar.Core.CommandHandlers
{
    using OppJar.Core.Commands;
    using System;
    using System.Threading.Tasks;
    
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command, Func<object> callBack = null);
    }
}
