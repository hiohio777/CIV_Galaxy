using System;
using UnityEngine;
using Zenject;

public class GalaxySceneDIInstaller : MonoInstaller
{
    [SerializeField] private UnityEngine.Object planetPrefab, discoveryCellUIPrefab, unitAbilityPrefab, valueChangeEffectPrefab;

    public override void InstallBindings()
    {
        Container.Bind<DiscoveredCivilization>().AsSingle(); 
        Container.Bind<LeaderQualifier>().AsSingle();

        Container.Bind<IGalaxyUITimer>().FromComponentInHierarchy().AsSingle();

        // Цивилизации
        Container.Bind<CivilizationData>().AsTransient();
        Container.Bind<Scanner>().AsTransient(); 
        Container.Bind<Science>().AsTransient();
        Container.Bind<Industry>().AsTransient();
        Container.Bind<Diplomacy>().AsTransient(); 
        Container.Bind<Ability>().AsTransient();

        Container.Bind<GalacticEventGenerator>().AsTransient();
        Container.Bind<GalacticEventGeneratorPlayer>().AsTransient();

        Container.Bind<ICivilization>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ICivilizationAl>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ICivilizationPlayer>().FromComponentInHierarchy().AsSingle();


        Container.Bind<GalaxySceneUI>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<AbilityUI>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<MessageGalaxy>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<CanvasFonGalaxy>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<PlayerCivInfo>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ScanerPanelUI>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<SciencePlayerUI>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<SciencePanelUI>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ImagePanelInfoScience>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IGalacticEventDisplay>().FromComponentsInHierarchy().AsSingle(); 
        Container.Bind<CounterEndGame>().FromComponentsInHierarchy().AsSingle(); 
        Container.Bind<EndGameUI>().FromComponentsInHierarchy().AsSingle(); 
        Container.Bind<GalaxyData>().FromComponentsInHierarchy().AsSingle();

        Container.Bind<MessageDiscoveredCivilization>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<MessageStartGame>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<MessageBackMainMenu>().FromComponentsInHierarchy().AsSingle();

        // Фабрики
        Container.Bind<PlanetsFactory>().AsSingle();
        Container.Bind<UnitAbilityFactory>().AsSingle(); 
        Container.Bind<AbilityFactory>().AsSingle(); 
        Container.Bind<ValueChangeEffectFactory>().AsSingle();

        Container.BindFactory<Action<object>, ValueChangeEffect, ValueChangeEffect.Factory>().FromComponentInNewPrefab(valueChangeEffectPrefab).AsSingle();
        Container.BindFactory<Action<object>, Planet, Planet.Factory>().FromComponentInNewPrefab(planetPrefab).AsSingle();
        Container.BindFactory<Action<object>, UnitAbility, UnitAbility.Factory>().FromComponentInNewPrefab(unitAbilityPrefab).AsSingle();
        Container.BindFactory<DiscoveryCell, DiscoveryCellUI, DiscoveryCellUI.Factory>().FromComponentInNewPrefab(discoveryCellUIPrefab).AsSingle();
    }
}
