using ObservableCollections;
using R3;
using UnityEngine;

namespace States.Military
{
    public class Fight
    {
        public readonly FightData Origin;
        public ObservableList<Squad> SquadeDates { get; } = new();

        public ReactiveProperty<Vector3> FightPoint = new();

        public ReactiveProperty<Military> EnemyMilitary = new();
        public ReactiveProperty<Military> PlayerMilitary = new();

        public Fight(FightData data)
        {
            Origin = data;

            EnemyMilitary.Value = new Military(Origin.EnemyMilitaryData);
            PlayerMilitary.Value = new Military(Origin.PlayerMilitaryData);

            EnemyMilitary.Subscribe(e =>
            {
                var addedMilitary = e;

                Origin.EnemyMilitaryData = e.Origin;
            });

            PlayerMilitary.Subscribe(e =>
            {
                var addedMilitary = e;

                Origin.PlayerMilitaryData = e.Origin;
            });

            FightPoint.Subscribe(e =>
            {
                var addedMilitary = e;

                Origin.FightPoint = e;
            });
        }
    }
}
