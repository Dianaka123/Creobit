using System;
using UnityEngine;

namespace Assets.Scripts.Views
{
    [Serializable]
    public class CameraSettings
    {
        public float Distance = 5;
        public float Angle = 20;
    }

    public class RunnerView : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private FinishView _finishView;

        [SerializeField]
        private CameraSettings _cameraSettings;

        public Transform SpawnPointTarget => _spawnPoint;

        public FinishView FinishView => _finishView;

        public CameraSettings CameraSettings => _cameraSettings;

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}