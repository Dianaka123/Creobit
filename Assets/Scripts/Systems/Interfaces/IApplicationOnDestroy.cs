using System;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IApplicationOnDestroy
    {
        public event Action Destroing;
    }
}
