using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class DebugLog : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputField;

        public void SetLog(string log)
        {
            inputField.text += "\\n" + log;
        }
    }
}