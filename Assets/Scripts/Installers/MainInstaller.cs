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

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirbaseSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ServerConfigProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetsBundleLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameConfigurationProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RunnerTimeManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();

            Container.BindIFactory<MenuView>().FromComponentInNewPrefab(MenuView);
            
            Container.BindInterfacesAndSelfTo<InitializeCanvasController>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClickerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<RunnerController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Initiator>().AsSingle().NonLazy();
            
            Container.Bind<CanvasManager>().AsSingle();
            Container.Bind<GameManager>().AsSingle();
        }
    }
}
