using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Controllers
{
    public class MenuController : IController
    {
        private readonly MenuChooseGameController _menuChooseGameController;
        private readonly MenuDataManipulatorController _menuDataManipulatorController;

        public MenuController(MenuChooseGameController menuChooseGameController, MenuDataManipulatorController menuDataManipulatorController)
        {
            _menuChooseGameController = menuChooseGameController;
            _menuDataManipulatorController = menuDataManipulatorController;

        }

        public void Init()
        {
            
        }

        public async UniTask Run()
        {
            _menuChooseGameController.Init();
            await _menuChooseGameController.Run();
            _menuChooseGameController.Exit();

            _menuDataManipulatorController.Init();
            await _menuDataManipulatorController.Run();
            _menuDataManipulatorController.Exit();
        }

        public void Exit()
        {

        }
    }
}
