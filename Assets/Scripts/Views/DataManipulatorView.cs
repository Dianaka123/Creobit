using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class DataManipulatorView : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private Button _load;

        [SerializeField]
        private Button _unload;

        public event Action LoadClick;
        public event Action UnloadClick;

        private void Awake()
        {
            _load.onClick.AddListener(() => LoadClick?.Invoke());
            _unload.onClick.AddListener(() => UnloadClick?.Invoke());
        }

        public void Dispose()
        {
            _load.onClick.RemoveAllListeners();
            _unload.onClick.RemoveAllListeners();
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}