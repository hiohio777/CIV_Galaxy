public abstract class CivilizationStructureBase
{

    protected DifficultSettingsScriptable _difficultSettings; // Настройки сложности

    protected CivilizationStructureBase(IGalaxyUITimer galaxyUITimer)
    {
        galaxyUITimer.ExecuteOfTime += ExecuteOnTimeEvent;
        _difficultSettings = PlayerSettings.Instance.GetDifficultSettings;
    }

    protected abstract void ExecuteOnTimeEvent(float deltaTime);
}