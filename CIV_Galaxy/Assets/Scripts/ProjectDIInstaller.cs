using UnityEngine;
using Zenject;

public class ProjectDIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LoaderDataGame>().AsSingle();
        Container.Bind<PlayerData>().AsSingle();

        Container.Bind<Civilizations>().AsTransient();

        Container.Bind<GalaxyScriptableObject>().FromNewScriptableObjectResource("Galaxies/GalaxyStandart").AsSingle();

    }
}