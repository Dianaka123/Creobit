using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class ClickerView : MonoBehaviour
    {
        [SerializeField]
        private RawImage _image;

        public void SetImage(Texture2D texture)
        {
            _image.texture = texture;
        }  
    }
}
