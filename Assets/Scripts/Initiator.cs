using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using UnityEngine;
using Zenject;

public class Initiator : IInitializable
{
    private readonly InitializeCanvasController _initCanvasController;
    private readonly MenuController _menuController;
    private readonly ClickerController _clickerController;

    public Initiator(InitializeCanvasController initCanvasController, MenuController menuController, ClickerController clickerController)
    {
        _initCanvasController = initCanvasController;
        _menuController = menuController;
        _clickerController = clickerController;
    }

    public async void Initialize()
    {
        _initCanvasController.Init();
        _initCanvasController.Exit();

        await _menuController.Run();
        _clickerController.Init();
    }
}
