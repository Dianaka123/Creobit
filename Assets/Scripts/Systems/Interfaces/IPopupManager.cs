using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface IPopupManager
    {
        UniTask Show(string text, bool isOkButtonActive);
        UniTask Hide();
    }
}
