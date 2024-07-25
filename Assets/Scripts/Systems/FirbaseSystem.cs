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
    public class FirbaseSystem : IInitializable, IDisposable, IUriResolver, IServerFileProvider
    {
        private FirebaseApp app;

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

        public UniTask<Uri> ResolveAssetsUriAsync(string url)
        {
            StorageReference assetsRef = GetRef(url);
            return assetsRef.GetDownloadUrlAsync().AsUniTask();
        }

        public UniTask<byte[]> LoadFile(string url)
        {
            StorageReference fileRef = GetRef(url);
            return fileRef.GetBytesAsync(long.MaxValue).AsUniTask();
        }

        private StorageReference GetRef(string url)
        {
            var storage = FirebaseStorage.DefaultInstance;
            return storage.GetReferenceFromUrl(url);
        }

        public async UniTask UpdateFile(byte[] bytes, string url)
        {
            StorageReference fileRef = GetRef(url);
            await fileRef.PutBytesAsync(bytes);
        }
    }
}
