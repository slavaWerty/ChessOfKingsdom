using System;
using UnityEngine;

namespace Buildings
{
    [Serializable]
    public class BuildingsEntity : Entity
    {
        public int Id;
        public string TypeId;
        public Vector2Int Position;
        public int Level;
    }
}
