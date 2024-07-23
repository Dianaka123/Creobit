using Assets.Scripts.Managers;

namespace Assets.Scripts.Controllers
{
    public class MenuChooseGameController : BaseMenuController
    {
        public MenuChooseGameController(CanvasManager canvasManager, GameManager gameManager) : base(canvasManager, gameManager)
        {
        }

        public void Exit()
        {
            _canvasManager.Menu.ChooseGameView.ClickerClicked -= OnClickerChoosed;
            _canvasManager.Menu.ChooseGameView.RunnerClicked -= OnRunnerChoosed;

            _canvasManager.Menu.ChooseGameView.SetActive(false);
        }

        public void Init()
        {
            isClicked = false;
            _canvasManager.Menu.ChooseGameView.SetActive(true);

            _canvasManager.Menu.ChooseGameView.ClickerClicked += OnClickerChoosed;
            _canvasManager.Menu.ChooseGameView.RunnerClicked += OnRunnerChoosed;
        }

        private void OnRunnerChoosed()
        {
            isClicked = true;
            _gameManager.CurrentGame = Enums.GamesType.Runner;
        }

        private void OnClickerChoosed()
        {
            isClicked = true;
            _gameManager.CurrentGame = Enums.GamesType.Clicker;
        }
    }
}
