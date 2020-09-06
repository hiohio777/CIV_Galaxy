using UnityEngine;

public class Civilization : CivilizationBase, ICivilization, ICivilizationAl
{
    public override void Assign(CivilizationScriptable civData)
    {
        _positionCiv = transform.position;
        base.Assign(civData);
    }

    public void Open()
    {
        civilizationUI.Assign(CivData);
        IsOpen = true;

        Debug.Log($"Discover Civilization: {CivData.name}");
    }

    protected override void ExecuteOnTimeProcess() { }

    protected override void ExicuteScanning()
    {
        // Debug.Log($"{CivData.Name}: Сканирование!");

    }

    private void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Default";

        civilizationUI.Close();
    }
}
