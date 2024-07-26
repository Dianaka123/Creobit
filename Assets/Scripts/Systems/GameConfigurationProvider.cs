using Assets.Scripts.ConfigData;
using Assets.Scripts.Systems.Interfaces;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Text;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Systems
{
    public class GameConfigurationProvider : IGameConfigProvider, IUniTaskAsyncDisposable
    {
        private readonly IServerConfigProvider _serverConfigProvider;
        private readonly IServerFileProvider _serverFileLoader;

        private string _url;

        public GameConfiguration Configuration { get; private set; }

        public GameConfigurationProvider(IServerConfigProvider configProvider, IServerFileProvider serverFileLoader)
        {
            _serverConfigProvider = configProvider;
            _serverFileLoader = serverFileLoader;
        }

        public async UniTask Download()
        {
            if(Configuration == null)
            {
                try
                {
                    _url = _serverConfigProvider.Config.GameConfigURL;
                    byte[] configBytes = await _serverFileLoader.LoadFile(_url);
                    string jsonStr = Encoding.UTF8.GetString(configBytes);
                    Configuration = JsonConvert.DeserializeObject<GameConfiguration>(jsonStr);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }

        public void Unload()
        {
            Configuration = null;
        }

        public async UniTask Upload()
        {
            string jsonStr = JsonConvert.SerializeObject(Configuration);
            var bytes = Encoding.UTF8.GetBytes(jsonStr);
            await _serverFileLoader.UpdateFile(bytes, _url);
        }

        public async UniTask DisposeAsync()
        {
            await Upload();
        }
    }
}
