using Assets.Scripts.Systems.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class ApplicationLifecycleTracker : MonoBehaviour, IApplicationOnDestroy
    {
        public event Action Destroing;

        private void OnDestroy()
        {
            Destroing?.Invoke();
        }
    }
}