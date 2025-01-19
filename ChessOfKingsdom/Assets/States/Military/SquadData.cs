using System;

namespace States.Military
{
    [Serializable]
    public class SquadEntity : Entity
    {
        public int Amount;
        public float Speed;
        public SquadType Type;
    }
}
