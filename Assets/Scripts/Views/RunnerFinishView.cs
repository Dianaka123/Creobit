using Assets.Scripts.Systems.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class RunnerFinishView : MonoBehaviour, IFinishTriggerHandler
    {
        public event Action<GameObject> FinishTriggered;

        private void OnCollisionEnter(Collision collision)
        {
            FinishTriggered?.Invoke(collision.gameObject);
        }
    }
}