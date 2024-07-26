using System;
using UnityEngine;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IFinishTriggerHandler
    {
        event Action<GameObject> FinishTriggered;
    }
}
