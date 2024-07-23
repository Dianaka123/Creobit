using Assets.Scripts.Enums;
using Assets.Scripts.Systems.Interfaces;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Systems
{
    public class FirbaseSystem : IInitializable, IDisposable, IAssetsUriResolver
    {
        private readonly IServerConfigProvider _configProvider;

        private FirebaseApp app;

        public FirbaseSystem(IServerConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public async void Initialize()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    app = FirebaseApp.Create();
                }
                else
                {
                    Debug.LogError(string.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            }).AsUniTask();
        }

        public void Dispose()
        {
            app?.Dispose();
            app = null;
        }

        public UniTask<Uri> ResolveUriAsync(GamesType gamesType)
        {
            var storage = FirebaseStorage.DefaultInstance;

            string url = gamesType == GamesType.Clicker
                ? _configProvider.Config.ClickerResourcesURL 
                : _configProvider.Config.RunnerResourcesURL;

            StorageReference imageRef = storage.GetReferenceFromUrl(url);

            return imageRef.GetDownloadUrlAsync().AsUniTask();
        }
    }
}
