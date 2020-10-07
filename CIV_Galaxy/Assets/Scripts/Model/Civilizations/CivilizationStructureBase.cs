using Zenject;

public abstract class CivilizationStructureBase
{
    protected DifficultSettingsScriptable _difficultSettings; // Настройки сложности

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer, PlayerSettings playerSettings)
    {
        galaxyUITimer.ExecuteOfTime += ExecuteOnTimeEvent;
        _difficultSettings = playerSettings.GetDifficultSettings;
    }

    protected abstract void ExecuteOnTimeEvent(float deltaTime);
}