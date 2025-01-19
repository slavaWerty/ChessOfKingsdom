using StateRoot;
using System.Linq;
using UnityEngine;

namespace Fight.Commands.Handler
{
    public class CmdSquadSpendHandler : ICommandHandler<CmdSquadSpend>
    {
        private readonly GameStateProxy _gameState;

        public CmdSquadSpendHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }

        public bool Handle(CmdSquadSpend command)
        {
            var requiredId = command.Id;
            var requiredMilitary = _gameState.Militaries.FirstOrDefault(m => m.Id == requiredId);
            if (requiredMilitary == null)
            {
                Debug.LogError("Trying to spend not existed resource");
                return false;
            }

            var flag = false;

            foreach (var squad in command.Squads)
            {
                var typeSquad = requiredMilitary.SquadeDates
                    .FirstOrDefault(m => m.Amount.Value >= squad.Amount.Value
                    && m.SquadType == squad.SquadType); 

                if(typeSquad != null)
                    flag = true;
                else
                    flag = false;
            }

            if (!flag)
            {
                Debug.LogError(
                    $"Trying to spend more resources than existed ({requiredId}). Exists: {requiredMilitary.SquadeDates}, trying to spend: {command.Squads}");
                return false;
            }

            foreach (var squad in command.Squads)
            {
                var typeSquade = requiredMilitary.SquadeDates
                    .FirstOrDefault(m => m.SquadType == squad.SquadType);

                if((typeSquade.Amount.Value - squad.Amount.Value) <= 0)
                    requiredMilitary.SquadeDates.Remove(typeSquade);
                else
                    typeSquade.Amount.Value -= squad.Amount.Value;
            }

            return true;
        }
    }
}
