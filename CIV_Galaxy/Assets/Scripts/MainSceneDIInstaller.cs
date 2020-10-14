using Zenject;

public class MainSceneDIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ChoiceCivilization>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<MainSceneUI>().FromComponentsInHierarchy().AsSingle();

        Container.Bind<JustRotate>().FromComponentsInHierarchy().AsSingle(); 
    }
}
