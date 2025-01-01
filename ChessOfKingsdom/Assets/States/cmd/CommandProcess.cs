using States;
using System;
using System.Collections.Generic;

public class CommandProcess : ICommandProcessor
{
    private readonly Dictionary<Type, object> _handlesMap = new();
    private readonly IGameStateProvider gameStateProvider;

    public CommandProcess(IGameStateProvider gameStateProvider)
    {
        this.gameStateProvider = gameStateProvider;
    }

    public bool Process<TCommand>(TCommand command) where TCommand : ICommand
    {
        if (_handlesMap.TryGetValue(typeof(TCommand), out var handler))
        {
            var typedHandler = (ICommandHandler<TCommand>)handler;
            var result = typedHandler.Handle(command);

            if (result)
                gameStateProvider.SaveGameState();

            return result;
        }

        return false;
    }

    public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
    {
        _handlesMap[typeof(TCommand)] = handler;
    }

}
