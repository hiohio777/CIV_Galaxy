using UnityEngine;

public class Civilization : CivilizationBase, ICivilization, ICivilizationAl
{
    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);
    }

    public void Open()
    {
        civilizationUI.Assign(CivData);

        Debug.Log($"Discover Civilization: {CivData.name}");
    }

    protected override void ExecuteOnTimeProcess() { }

    protected override void ExicuteScanning()
    {
        // Debug.Log($"{CivData.Name}: Сканирование!");
    }

    private void Start()
    {
        civilizationUI.Close();
    }
}
