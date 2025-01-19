using Buildings;
using GamePlay.Settings;
using States.GameResources;
using System;
using System.Collections.Generic;

namespace StateRoot
{
    [Serializable]
    public class GameState
    {
        public int GlobalEntityId;
        public List<BuildingsEntity> Buildings;
        public BuildingsSettings BuildingsSettings;
        public List<ResourceData> Resource;
        public List<MilitaryData> Militaries;

        public int CreateEntityId()
        {
            return GlobalEntityId++;
        }
    }
}
