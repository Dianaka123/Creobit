using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    public class MenuDataManipulatorController : BaseMenuController, ITickable
    {
        private readonly IAssetProvider _assetProvider;

        public MenuDataManipulatorController(CanvasManager canvasManager, GameManager gameManager, IAssetProvider assetProvider) : base(canvasManager, gameManager)
        {
            _assetProvider = assetProvider;
        }

        public void Init()
        {
            _canvasManager.Menu.ManipulatorView.SetActive(true);

            _canvasManager.Menu.ManipulatorView.LoadClick += Load;
            _canvasManager.Menu.ManipulatorView.UnloadClick += Unload;
        }

        public void Exit()
        {
            _canvasManager.Menu.ManipulatorView.SetActive(false);

            _canvasManager.Menu.ManipulatorView.LoadClick -= Load;
            _canvasManager.Menu.ManipulatorView.UnloadClick -= Unload;
        }

        private void Unload()
        {
            isClicked = true;
        }

        private async void Load()
        {
            await _assetProvider.PreloadAsync(_gameManager.CurrentGame);
            isClicked = true;
        }

        public UniTask Run()
        {
            return UniTask.WaitUntil(() => isClicked);
        }

        public void Tick()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isClicked = true;
            }
        }
    }
}
