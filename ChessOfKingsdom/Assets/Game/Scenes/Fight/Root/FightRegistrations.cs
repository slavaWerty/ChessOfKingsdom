using BaCon;
using Fight.Commands.Handler;
using Fight.Services;
using mBuilding.Scripts.Game.Common;
using R3;
using States;

public class FightRegistrations 
{
    public static void Regiter(DIContainer container, FightEnterParams enterParams)
    {
        var gameStateProvider = container.Resolve<IGameStateProvider>();
        var gameState = gameStateProvider.GameState;

        var cmd = new CommandProcess(gameStateProvider);
        cmd.RegisterHandler(new CmdSquadAddHandler(gameState));
        cmd.RegisterHandler(new CmdSquadSpendHandler(gameState));

        container.RegisterInstance<ICommandProcessor>(cmd);

        container.RegisterInstance(AppConstants.EXIT_IN_CASTLE_TO_FIGHT_SCENE_REQUEST_TAG, new Subject<Unit>());

        container.RegisterFactory(_ => new MilitaryService(
            gameState.Militaries,
            cmd));
    }
}
