using Architecture.Services.AssetProviding.Impl;
using Architecture.Services.Factories.Impl;
using Architecture.Services.UI.Impl;
using UnityEngine;
using Zenject;

namespace Architecture.Bootstrappers {
    public class UIBootstrapper : MonoInstaller {
        [SerializeField] private Canvas _uiRoot;

        public override void InstallBindings() {
            Container.Bind<Canvas>().FromInstance(_uiRoot).AsSingle().NonLazy();
            
            BindService<ResourcesUIProvider>();
            BindService<UIFactory>();
            BindService<UIService>();
        }
        
        private void BindService<TService>() 
            => Container.BindInterfacesAndSelfTo<TService>().AsSingle().NonLazy();
    }
}