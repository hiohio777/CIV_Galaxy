using System;
using UnityEngine;
using Zenject;

public class DIContainerInstaller : MonoInstaller
{
    [SerializeField] private UnityEngine.Object messageDiscoveredPrefab, messageStartGamePrefab, planetPrefab;
    [SerializeField] private ScriptableObject galaxyScriptableObject;

    public override void InstallBindings()
    {
        Container.Bind<LoaderDataGame>().AsSingle();
        Container.Bind<Civilizations>().AsTransient();
        Container.Bind<GalaxyGame>().AsSingle();
        Container.Bind<GalaxyData>().AsSingle(); 
        Container.Bind<PlanetsFactory>().AsSingle();

        Container.Bind<GalaxyScriptableObject>().FromNewScriptableObject(galaxyScriptableObject).AsSingle();

        Container.Bind<CivilizationData>().AsTransient();
        Container.Bind<ScannerPlanets>().AsTransient();

        Container.Bind<ICivilization>().FromComponentsInHierarchy().AsTransient();
        Container.Bind<ICivilizationAl>().FromComponentsInHierarchy().AsTransient();
        Container.Bind<ICivilizationPlayer>().FromComponentInHierarchy().AsTransient();

        Container.Bind<CivilizationAlPanel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGalaxyUITimer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameUI>().FromComponentsInHierarchy().AsSingle();

        Container.BindFactory<MessageDiscoveredCivilization, MessageDiscoveredCivilization.Factory>().FromComponentInNewPrefab(messageDiscoveredPrefab);
        Container.BindFactory<MessageStartGame, MessageStartGame.Factory>().FromComponentInNewPrefab(messageStartGamePrefab);
        Container.BindFactory<Action<Planet>, Planet, Planet.Factory>().FromComponentInNewPrefab(planetPrefab);
        
    }
}
