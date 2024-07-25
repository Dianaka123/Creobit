using Assets.Scripts.Enums;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IUriResolver
    {
        UniTask<Uri> ResolveAssetsUriAsync(string url);
    }
}
