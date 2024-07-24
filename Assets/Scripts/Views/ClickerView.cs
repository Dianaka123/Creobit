using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class ClickerView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private RawImage _image;

        [SerializeField]
        private TMP_Text _clickCounter;

        [SerializeField]
        private Button _goBack;

        public Action Clicked;
        public Action GoBack;

        private void Awake()
        {
            _goBack.onClick.AddListener(() => GoBack?.Invoke());
        }

        private void OnDestroy()
        {
            _goBack.onClick.RemoveAllListeners();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public void SetImage(Texture2D texture)
        {
            _image.texture = texture;
        }
        
        public void SetText(string count)
        {
            _clickCounter.SetText(count);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
