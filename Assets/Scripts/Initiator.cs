using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using System;
using UnityEngine;
using Zenject;

public class Initiator : IInitializable
{
    private readonly InitializeCanvasController _initCanvasController;
    private readonly MenuController _initializeMenuController;
    private readonly ClickerController _clickerController;
    private readonly RunnerController _runnerController;
    private readonly GameManager _gameManager;
    private readonly IErrorHandler _errorHandler;

    public Initiator(InitializeCanvasController initCanvasController,
        MenuController initializeMenuController, ClickerController clickerController,
        RunnerController runnerController, GameManager gameManager,
        IErrorHandler errorHandler)
    {
        _initCanvasController = initCanvasController;
        _initializeMenuController = initializeMenuController;
        _clickerController = clickerController;
        _runnerController = runnerController;
        _gameManager = gameManager;
        _errorHandler = errorHandler;
    }

    public async void Initialize()
    {
        await _initCanvasController.Init();
        await _initCanvasController.Exit();

        while (true)
        {
            _gameManager.CurrentGame = Assets.Scripts.Enums.GamesType.None;

            try
            {
                await _initializeMenuController.Init();
                await _initializeMenuController.Exit();

                if (_gameManager.CurrentGame == Assets.Scripts.Enums.GamesType.Clicker)
                {
                    await _clickerController.Init();
                    await _clickerController.Exit();
                }
                else
                {
                    await _runnerController.Init();
                    await _runnerController.Exit();
                }
            }
            catch(Exception ex)
            {
                await _errorHandler.HandleErrorAsync(ex);
            }
        }
    }


}
