using UnityEngine;

namespace Fight.Commands.Handler
{
    public class CmdAttack : ICommand
    {
        public Vector3 AttackPoint;
        public Transform Position;
        public float Speed;

        public CmdAttack(Vector3 attackPoint,
            Transform position,
            float speed)
        {
            AttackPoint = attackPoint;
            Position = position;
            Speed = speed;
        }
    }
}
