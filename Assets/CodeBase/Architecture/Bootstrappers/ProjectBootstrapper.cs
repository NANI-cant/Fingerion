using Ads.Impl;
using Architecture.Services.AssetProviding.Impl;
using Architecture.Services.Factories.Impl;
using Architecture.Services.Gameplay.Impl;
using Architecture.Services.General;
using Architecture.Services.General.Impl;
using Architecture.Services.PersistentProgress.Impl;
using Audio;
using UnityEngine;
using Zenject;

namespace Architecture.Bootstrappers {
    public class ProjectBootstrapper : MonoInstaller, ICoroutineRunner {
	    [SerializeField] private TextAsset _localizationXML;
	    [SerializeField] private Source _persistentMusic;
	    [SerializeField] private Source _sounds;
	    
	    public override void InstallBindings() {
		    Container.BindInstance(0).WhenInjectedInto<RandomService>();
		    Container.BindInstance(_persistentMusic).WhenInjectedInto<AudioService>();
		    Container.BindInstance(_sounds).WhenInjectedInto<AudioService>();
		    Container.BindInstance(_sounds).WhenInjectedInto<UIFactory>();
		    Container.BindInstance(_localizationXML).WhenInjectedInto<LocalizationService>();
		    Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle().NonLazy();

		    BindService<UnityDestroyProvider>();
		    BindService<UnityInstantiateProvider>();
		    BindService<UnitySceneLoadService>();
		    BindService<UnityTimeProvider>();
		    
		    BindService<ResourcesMetricProvider>();
		    BindService<ResourcesPrefabProvider>();
		    
		    BindService<RandomService>();
		    BindService<PersistentProgressService>();
		    BindService<LocalizationService>();
		    BindService<AudioService>();
		    BindService<SchedulerFactory>();

		    BindService<BiomService>();
		    BindService<BankService>();
		    BindService<AchievementService>();
		    BindService<InputService>();

#if UNITY_EDITOR
		    BindService<PrefsSaveLoadService>();
		    BindService<DebugAdService>();
#elif UNITY_WEBGL
			BindService<YandexSaveLoadService>();
		    BindService<YandexAdService>();
#elif UNITY_ANDROID
			BindService<PrefsSaveLoadService>();
		    BindService<DebugAdService>();
#else
			BindService<PrefsSaveLoadService>();
		    BindService<DebugAdService>();
#endif
	    }

	    private void BindService<TService>() 
			=> Container.BindInterfacesAndSelfTo<TService>().AsSingle().NonLazy();
    }
}