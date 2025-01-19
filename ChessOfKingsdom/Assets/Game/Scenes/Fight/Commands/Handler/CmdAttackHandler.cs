using StateRoot;
using UnityEngine;

namespace Fight.Commands.Handler
{
    public class CmdAttackHandler : ICommandHandler<CmdAttack>
    {
        private GameStateProxy _gameState;

        public CmdAttackHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }

        public bool Handle(CmdAttack command)
        {
            var transform = command.Position;
            var attackPoint = command.AttackPoint;
            var speed = command.Speed;

            if(speed <= 0)
            {
                Debug.LogError("Speed == 0");
                return false;
            }

            Vector3.MoveTowards(transform.position, attackPoint, Time.deltaTime * speed);

            return true;
        }
    }
}
