using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ClickerController : IController
    {
        private readonly IAssetProvider _assetProvider;
        private readonly CanvasManager _canvasManager;
        private readonly IGameConfigProvider _gameConfigProvider;

        private ClickerView _clickerView;
        private bool _isGoBackClicked;

        private Texture2D _gameTexture;

        public ClickerController(IAssetProvider assetProvider, CanvasManager canvasManager,
            IGameConfigProvider gameConfigProvider)
        {
            _assetProvider = assetProvider;
            _canvasManager = canvasManager;
            _gameConfigProvider = gameConfigProvider;
        }

        public async UniTask Init()
        {
            await _gameConfigProvider.Download(); 
            await _assetProvider.PreloadAsync(GamesType.Clicker);

            var assetBundle = _assetProvider.GetAssetBundle(GamesType.Clicker);

            var clickerViewAsset = assetBundle.LoadAsset<GameObject>("Clicker");
            _gameTexture = (Texture2D)await assetBundle.LoadAssetAsync("Click");

            var clickerViewGO = GameObject.Instantiate(clickerViewAsset);
            clickerViewGO.transform.SetParent(_canvasManager.Canvas.transform);
            clickerViewGO.transform.localPosition = Vector3.zero;

            _clickerView = clickerViewGO.GetComponent<ClickerView>();
        }

        //TODO: Need to fix - Image loaded not in time
        public async UniTask Run()
        {
            _clickerView.SetImage(_gameTexture);
           _clickerView.SetText(_gameConfigProvider.Configuration.ClickCount.ToString());

            _clickerView.Clicked += OnClicked;
            _clickerView.GoBack += OnGoBack;

            await UniTask.WaitUntil(() => _isGoBackClicked);
        }

        public async UniTask Exit()
        {
            await _gameConfigProvider.Upload();
            _clickerView.Clicked -= OnClicked;
            _clickerView.Destroy();
        }

        private void OnGoBack()
        {
            _isGoBackClicked = true;
        }

        private void OnClicked()
        {
            _gameConfigProvider.Configuration.ClickCount++;
            _clickerView.SetText(_gameConfigProvider.Configuration.ClickCount.ToString());
        }
    }
}