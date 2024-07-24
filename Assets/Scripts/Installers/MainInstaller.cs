using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using Assets.Scripts.Systems;
using Assets.Scripts.Systems.Interfaces;
using Assets.Scripts.Views;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public MenuView MenuView;
        public ClickerView ClickerView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirbaseSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ServerConfigProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetsBundleLoader>().AsSingle();

            Container.BindIFactory<MenuView>().FromComponentInNewPrefab(MenuView);
            Container.BindIFactory<ClickerView>().FromComponentInNewPrefab(ClickerView);
            
            Container.BindInterfacesAndSelfTo<InitializeCanvasController>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClickerController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Initiator>().AsSingle().NonLazy();
            
            Container.Bind<CanvasManager>().AsSingle();
            Container.Bind<GameManager>().AsSingle();
        }
    }
}
