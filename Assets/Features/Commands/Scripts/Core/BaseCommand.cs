using Cysharp.Threading.Tasks;
using System;

namespace Assets.Features.Command
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
    public class BaseCommand : IDisposable
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