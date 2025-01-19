using States.Military;
using System.Collections.Generic;

namespace Fight.Commands
{
    public class CmdSquadAdd : ICommand
    {
        public int Id;
        public readonly List<Squad> Squads;

        public CmdSquadAdd(int id, List<Squad> squads)
        {
            Squads = squads;
            Id = id;
        }
    }
}
