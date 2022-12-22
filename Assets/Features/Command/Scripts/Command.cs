using Cysharp.Threading.Tasks;
using System;

namespace Assets.Features.Command.Scripts
{
    public enum CommandStatus
    {
        Success,       
        Failed
    }
    public class CommandResult
    {
        public object Body;
        public CommandStatus Status;        
    }
    public class Command : IDisposable
    {
        public virtual void Dispose()
        {
            
        }

        public async virtual UniTask<CommandResult> Do()
        {
            return new CommandResult();
        }

        public virtual void Cancel()
        {

        }
    }
}