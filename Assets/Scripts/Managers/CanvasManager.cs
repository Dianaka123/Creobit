using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CanvasManager
    {
        private GameObject _canvas;
        public GameObject Canvas => _canvas;

        //public DebugLog Debug;

        public void Setup(GameObject canvas)
        {
            _canvas = canvas;
        }
    }
}