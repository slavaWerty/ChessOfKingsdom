using StateRoot;
using States.Military;
using System.Linq;

namespace Fight.Commands.Handler
{
    public class CmdSquadAddHandler : ICommandHandler<CmdSquadAdd>
    {
        private readonly GameStateProxy _gameState;

        public CmdSquadAddHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }

        public bool Handle(CmdSquadAdd command)
        {
            var requiredId = command.Id;
            var requiredMilitary = _gameState.Militaries
                .FirstOrDefault(m => m.Id == requiredId);

            if (requiredMilitary == null)
                requiredMilitary = CreateNewMilitary(command);

            foreach (var addSquad in command.Squads)
            {
                var typeSquad = requiredMilitary.SquadeDates
                    .FirstOrDefault(m => m.SquadType == addSquad.SquadType);

                if (typeSquad != null)
                    typeSquad.Amount.Value += addSquad.Amount.Value;
                else
                    requiredMilitary.SquadeDates.Add(addSquad);
            }

            return true;
        }

        private Military CreateNewMilitary(CmdSquadAdd command)
        {
            var newMilitaryData = new MilitaryData
            {
                Id = command.Id,
                SquadEntityes = new() 
            };

            foreach (var squad in command.Squads)
                newMilitaryData.SquadEntityes.Add(squad.Origin);

            var newMilitary = new Military(newMilitaryData);
            _gameState.Militaries.Add(newMilitary);

            return newMilitary;
        }
    }
}
