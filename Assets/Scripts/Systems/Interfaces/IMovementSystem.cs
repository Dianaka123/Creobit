using Assets.Scripts.Enums;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IMovementSystem
    {
        MovementType GetCurrentMovement();
    }
}
