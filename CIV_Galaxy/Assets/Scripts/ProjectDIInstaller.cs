using Zenject;

public class ProjectDIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LoaderDataGame>().AsSingle();
        Container.Bind<PlayerSettings>().AsSingle(); 
        Container.Bind<Statistics>().AsSingle();
        Container.Bind<Civilizations>().AsTransient();
    }
}