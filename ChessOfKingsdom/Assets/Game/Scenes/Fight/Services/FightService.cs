using Fight.Commands.Handler;
using Fight.View.Militaries;
using ObservableCollections;
using UnityEngine;

namespace Fight.Services
{
    public class FightService
    {
        private readonly ObservableList<SquadBinder> PlayerSquadBindersMap = new();
        private readonly ObservableList<SquadBinder> EnemySquadBindersMap = new();
        private readonly Vector3 FightPoint;
        private bool IsStartedFight = false;

        private readonly ICommandProcessor _cmd;

        public FightService(
            ObservableList<SquadBinder> playerSquadBinders,
            ObservableList<SquadBinder> enemySquadBinders,
            Vector3 fightPoint,
            ICommandProcessor cmd)
        {
            _cmd = cmd;
            FightPoint = fightPoint;

            foreach (var squadBinder in enemySquadBinders)
                EnemySquadBindersMap[(int)squadBinder.Type] = squadBinder;

            foreach (var squadBinder in playerSquadBinders)
                PlayerSquadBindersMap[(int)squadBinder.Type] = squadBinder;
        }

        public void Update()
        {
            if (!IsStartedFight)
                return;

            foreach (var squadBinder in PlayerSquadBindersMap)
                Attack(squadBinder.transform, squadBinder.Speed);

            foreach (var squadBinder in EnemySquadBindersMap)
                Attack(squadBinder.transform, squadBinder.Speed);
        }

        public void Start() => IsStartedFight = true;

        public void Stop() => IsStartedFight = false;

        public bool Attack(Transform attackerTransform, float speed)
        {
            var command = new CmdAttack(FightPoint,
                attackerTransform, speed);

            return _cmd.Process(command);
        }
    }
}
