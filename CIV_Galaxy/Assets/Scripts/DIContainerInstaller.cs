using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DIContainerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LoaderDataGame>().AsSingle();
        Container.Bind<Civilizations>().AsTransient();
        Container.Bind<GalaxyGame>().AsSingle();

        Container.Bind<ICivilization>().FromComponentsInHierarchy().AsTransient();
        Container.Bind<ICivilizationAl>().FromComponentsInHierarchy().AsTransient();
        Container.Bind<ICivilizationPlayer>().FromComponentInHierarchy().AsTransient();
        Container.Bind<GalaxyUITimer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameUI>().FromComponentsInHierarchy().AsSingle();

    }
}
