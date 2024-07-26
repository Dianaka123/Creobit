using Assets.Scripts.Enums;
using Assets.Scripts.Systems.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class MovementSystem : IMovementSystem
    {
        public MovementType GetCurrentMovement()
        {
            MovementType type = MovementType.None;

            if (Input.GetKey(KeyCode.S))
            {
                type |= MovementType.Back;
            }

            if (Input.GetKey(KeyCode.W))
            {
                type &= ~MovementType.Back;
                type |= MovementType.Straight;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                type |= MovementType.Left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                type |= MovementType.Right;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                type |= MovementType.Run;
            }

            return type;
        }
    }
}
