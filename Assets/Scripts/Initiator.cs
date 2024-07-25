using Assets.Scripts.Controllers;
using UnityEngine;
using Zenject;

public class Initiator : IInitializable
{
    private readonly InitializeCanvasController _initCanvasController;
    private readonly MenuController _initializeMenuController;
    private readonly ClickerController _clickerController;

    public Initiator(InitializeCanvasController initCanvasController, MenuController initializeMenuController, ClickerController clickerController)
    {
        _initCanvasController = initCanvasController;
        _initializeMenuController = initializeMenuController;
        _clickerController = clickerController;

    }

    public async void Initialize()
    {
        _initCanvasController.Init();
        _initCanvasController.Exit();

        //_initializeMenuController.Init();
        //await _initializeMenuController.Run();
        //_initializeMenuController.Exit();

        await _clickerController.Init();
        await _clickerController.Run();
        await _clickerController.Exit();
    }
}
