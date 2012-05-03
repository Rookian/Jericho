namespace Jericho.Core.Commands
{
    public interface ICommandHandler { }

    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommandMessage
    {
        object Execute(TCommand commandMessage);
    }
}