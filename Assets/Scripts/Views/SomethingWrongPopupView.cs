using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class SomethingWrongPopupView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private RectTransform _rectTransform;

        public event Action OkClick;

        private void Awake()
        {
            _button.onClick.AddListener(() =>
                {
                    OkClick?.Invoke();
                    Destroy(gameObject);
                }
            );
        }

        private void OnDestroy()
        {
            _button.onClick?.RemoveAllListeners();
        }

        public void Init(string text, bool isButtonActive)
        {
            _text.text = text;
            _button.gameObject.SetActive(isButtonActive);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}