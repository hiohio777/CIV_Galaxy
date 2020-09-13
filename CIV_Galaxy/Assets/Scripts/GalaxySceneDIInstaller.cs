using System;
using UnityEngine;
using Zenject;

public class GalaxySceneDIInstaller : MonoInstaller
{
    [SerializeField] private UnityEngine.Object planetPrefab, discoveryCellUIPrefab;

    public override void InstallBindings()
    {
        Container.Bind<GalaxyGame>().AsSingle();
        Container.Bind<GalaxyData>().AsSingle();
        Container.Bind<DiscoveredCivilization>().AsSingle();

        Container.Bind<IGalaxyUITimer>().FromComponentInHierarchy().AsSingle(); 

        // Цивилизации
        Container.Bind<CivilizationData>().AsTransient();
        Container.Bind<ScannerPlanets>().AsTransient(); 
        Container.Bind<Science>().AsTransient();

        Container.Bind<ICivilization>().FromComponentsInHierarchy().AsTransient();
        Container.Bind<ICivilizationAl>().FromComponentsInHierarchy().AsTransient();
        Container.Bind<ICivilizationPlayer>().FromComponentInHierarchy().AsTransient();

        Container.Bind<SciencePlayerUI>().FromComponentsInHierarchy().AsSingle(); 
        Container.Bind<SciencePanelUI>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ImagePanelInfoScience>().FromComponentsInHierarchy().AsSingle(); 

        Container.Bind<MessageDiscoveredCivilization>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<MessageStartGame>().FromComponentsInHierarchy().AsSingle(); 
        Container.Bind<MessageBackMainMenu>().FromComponentsInHierarchy().AsSingle();

        // Фабрики
        Container.Bind<PlanetsFactory>().AsSingle();

        Container.BindFactory<Action<Planet>, Planet, Planet.Factory>().FromComponentInNewPrefab(planetPrefab);
        Container.BindFactory<DiscoveryCell, DiscoveryCellUI, DiscoveryCellUI.Factory>().FromComponentInNewPrefab(discoveryCellUIPrefab);
    }
}
