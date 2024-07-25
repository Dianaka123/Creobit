using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IServerFileProvider
    {
        UniTask<byte[]> LoadFile(string url);
        UniTask UpdateFile(byte[] bytes, string url);
    }
}
