using BaCon;
using Game.Settings;
using GamePlay.Commands;
using GamePlay.Services;
using States;
using UnityEngine;

namespace GamePlay
{
    public static class CastleRegistrations
    {
        public static void Regiter(DIContainer container, CastleEnterParams enterParams)
        {
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var gameState = gameStateProvider.GameState;

            Debug.Log(gameState == null);

            var gameSettingsProvider = container.Resolve<ISettingsProvider>();
            var gameSettings = gameSettingsProvider.GameSettings;

            var cmd = new CommandProcess(gameStateProvider);

            cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameState));
            cmd.RegisterHandler(new CmdCreateBuildingsStateHandler(gameState, gameSettings));
            cmd.RegisterHandler(new CmdResourcesAddHandler(gameState));
            cmd.RegisterHandler(new CmdResourcesSpendHandler(gameState));
            container.RegisterInstance<ICommandProcessor>(cmd);

            container.RegisterFactory(_ => new BuildingsService(
                gameState.Buildings,
                cmd,
                gameSettings.BuildingsSettings)).AsSingle();

            container.RegisterFactory(_ => new ResourcesService(gameState.Resources, cmd)).AsSingle();
        }

    }
}
