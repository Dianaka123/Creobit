using Assets.Scripts.Managers;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Systems
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly IPopupManager _popupManager;

        public ErrorHandler(IPopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        public UniTask HandleErrorAsync(Exception error) 
            => _popupManager.Show(error.Message, true);
    }
}
