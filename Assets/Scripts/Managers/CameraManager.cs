using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        public Camera MainCamera => _camera;
    }
}