using Assets.Scripts.Systems.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class ServerConfigProvider : IServerConfigProvider
    {
        private static readonly string _path = Application.dataPath + "/config.json";
        private Configuration _configuration;

        public Configuration Config
        {
            get
            {
                if(_configuration == null)
                {
                    try
                    {
                        _configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(_path));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        return null;
                    }
                }

                return _configuration;
            }
        }
    }
}
