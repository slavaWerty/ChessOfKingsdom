using R3;
using UnityEngine;

namespace Buildings
{
    public class BuildingEntityProxy
    {
        public int Id { get; }
        public string TypeId { get; }

        public BuildingsEntity Origin { get; }

        public ReactiveProperty<Vector2Int> Position { get; }
        public ReactiveProperty<int> Level { get; }

        public BuildingEntityProxy(BuildingsEntity entity)
        {
            Origin = entity;
            Id = entity.Id;
            TypeId = entity.TypeId;

            Position = new ReactiveProperty<Vector2Int>(entity.Position);
            Level = new ReactiveProperty<int>(entity.Level);

            Position.Skip(1).Subscribe(value => entity.Position = value);
            Level.Skip(1).Subscribe(value => entity.Level = value);
        }
    }
}
