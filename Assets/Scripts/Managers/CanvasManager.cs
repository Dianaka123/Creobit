using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField]
        private ChooseGameView ChooseGameView;

        [SerializeField]
        private DataManipulatorView ChooseDataManipulatorView;

        private void Awake()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}