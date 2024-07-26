using System;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class FinishView : MonoBehaviour
    {
        public event Action FinishEnter;

        private void OnTriggerEnter(Collider other)
        {
            FinishEnter?.Invoke();
        }
    }
}