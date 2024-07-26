using Assets.Scripts.Managers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class InitializeCanvasController: IController
    {
        private readonly CanvasManager _canvasManager;
        private GameObject _canvas;

        public InitializeCanvasController( CanvasManager canvasManager)
        {
            _canvasManager = canvasManager;
        }

        public UniTask Init()
        {
            _canvas = CreateCanvas();
            _canvasManager.Setup(_canvas);
            return UniTask.CompletedTask;
        }

        public UniTask Exit() => UniTask.CompletedTask;

        //TODO: move to factory
        private GameObject CreateCanvas()
        {
            var canvasGO = new GameObject();
            canvasGO.name = "Canvas";

            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            return canvasGO;
        }

        public UniTask Run()
        {
            return UniTask.CompletedTask;
        }
    }
}
