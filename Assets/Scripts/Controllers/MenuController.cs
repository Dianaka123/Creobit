using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Controllers
{
    public class MenuController : IController
    {
        private readonly CanvasManager _canvasManger;
        private readonly GameManager _gameManager;
        private readonly IFactory<MenuView> _menuViewFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameConfigProvider _gameConfigProvider;
        private readonly IPopupManager _popupManager;

        private MenuView _menuView;
        private bool _isGameChoosed;

        public MenuController(CanvasManager canvasManager,
            IFactory<MenuView> menuViewFactory, IAssetProvider assetProvider,
            IGameConfigProvider gameConfigProvider, GameManager gameManager,
            IPopupManager popupManager)
        {
            _canvasManger = canvasManager;
            _menuViewFactory = menuViewFactory;
            _assetProvider = assetProvider;
            _gameConfigProvider = gameConfigProvider;
            _gameManager = gameManager;
            _popupManager = popupManager;

        }

        public UniTask Exit()
        {
            _menuView.ClickerDataManipulator.LoadClick -= LoadClick;
            _menuView.ClickerDataManipulator.UnloadClick -= UnloadClick;

            _menuView.RunnerDataManipulator.LoadClick -= LoadClick;
            _menuView.RunnerDataManipulator.UnloadClick -= UnloadClick;

            _menuView.ChooseGameView.ClickerClicked -= OnClickerClicked;
            _menuView.ChooseGameView.RunnerClicked -= OnRunnerClicked;

            _menuView.Destroy();

            return UniTask.CompletedTask;
        }

        public async UniTask Init()
        {
            _isGameChoosed = false;

            MenuView view = _menuViewFactory.Create();
            view.transform.SetParent(_canvasManger.Canvas.transform);
            view.transform.localPosition = UnityEngine.Vector3.zero;

            _menuView = view;

            _menuView.ClickerDataManipulator.LoadClick += LoadClick;
            _menuView.ClickerDataManipulator.UnloadClick += UnloadClick;

            _menuView.RunnerDataManipulator.LoadClick += LoadClick;
            _menuView.RunnerDataManipulator.UnloadClick += UnloadClick;

            _menuView.ChooseGameView.ClickerClicked += OnClickerClicked;
            _menuView.ChooseGameView.RunnerClicked += OnRunnerClicked;

            await UniTask.WaitUntil(() => _isGameChoosed);
        }

        private void OnRunnerClicked()
        {
            _gameManager.CurrentGame = GamesType.Runner;
            _isGameChoosed = true;
        }

        private void OnClickerClicked()
        {
            _gameManager.CurrentGame = GamesType.Clicker;
            _isGameChoosed = true;
        }

        private void UnloadClick(GamesType type)
        {
            _gameConfigProvider.Unload();
            _assetProvider.UnloadAsync(type);
        }

        private async void LoadClick(GamesType type)
        {
            _popupManager.Show("Preloading resources...", false).Forget();
            await _gameConfigProvider.Download();
            await _assetProvider.PreloadAsync(type);
            await _popupManager.Hide();
        }
    }
}
