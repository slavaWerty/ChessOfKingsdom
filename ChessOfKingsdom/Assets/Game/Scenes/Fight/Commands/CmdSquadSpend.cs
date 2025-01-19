using States.Military;
using System.Collections.Generic;

namespace Fight.Commands
{
    public class CmdSquadSpend : ICommand
    {
        public int Id;
        public readonly List<Squad> Squads;

        public CmdSquadSpend(int id, List<Squad> squads)
        {
            Squads = squads;
            Id = id;
        }
    }
}
