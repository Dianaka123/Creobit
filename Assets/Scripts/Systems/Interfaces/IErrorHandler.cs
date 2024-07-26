using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IErrorHandler
    {
        UniTask HandleErrorAsync(Exception error);
    }
}
