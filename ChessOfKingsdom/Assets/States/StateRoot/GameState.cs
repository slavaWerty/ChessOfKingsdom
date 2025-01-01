using Buildings;
using System;
using System.Collections.Generic;

namespace StateRoot
{
    [Serializable]
    public class GameState
    {
        public int GlobalEntityId;
        public List<BuildingsEntity> Buildings;

        public int CreateEntityId()
        {
            return GlobalEntityId++;
        }
    }
}
