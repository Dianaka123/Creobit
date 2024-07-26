using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using UnityEngine;
using Zenject;

public class Initiator : IInitializable
{
    private readonly InitializeCanvasController _initCanvasController;
    private readonly MenuController _initializeMenuController;
    private readonly ClickerController _clickerController;
    private readonly RunnerController _runnerController;
    private readonly GameManager _gameManager;

    public Initiator(InitializeCanvasController initCanvasController, MenuController initializeMenuController, ClickerController clickerController, RunnerController runnerController, GameManager gameManager)
    {
        _initCanvasController = initCanvasController;
        _initializeMenuController = initializeMenuController;
        _clickerController = clickerController;
        _runnerController = runnerController;
        _gameManager = gameManager;
    }

    public async void Initialize()
    {
        await _initCanvasController.Init();
        await _initCanvasController.Exit();

        while (true)
        {
            await _initializeMenuController.Init();
            await _initializeMenuController.Run();
            await _initializeMenuController.Exit();

            if(_gameManager.CurrentGame == Assets.Scripts.Enums.GamesType.Runner)
            {
                await _clickerController.Init();
                await _clickerController.Run();
                await _clickerController.Exit();
            }
            else
            {
                await _runnerController.Init();
                await _runnerController.Run();
                await _runnerController.Exit();
            }
        }
        
    }
}
