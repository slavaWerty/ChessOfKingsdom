using System;
using UnityEngine;

namespace States.Military
{
    [Serializable]
    public class FightData
    {
        public Vector3 FightPoint;

        public MilitaryData EnemyMilitaryData;
        public MilitaryData PlayerMilitaryData;
    }
}
