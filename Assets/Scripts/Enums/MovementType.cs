using System;

namespace Assets.Scripts.Enums
{
    [Flags]
    public enum MovementType 
    {
        Straight = 1,
        Back = 2,
        Left = 4,
        Right = 8,
        Run = 16,
        None = 0
    }
}
