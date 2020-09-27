using UnityEngine;

[CreateAssetMenu(fileName = "DifficultSettingsScriptable", menuName = "Data/DifficultSettings", order = 54)]
public class DifficultSettingsScriptable : ScriptableObject
{
    [SerializeField, Range(0, 50)] private int dangerPlayer = 0; // То как сильно Al воспринимает игрпока как угрозу

    public int DangerPlayer => dangerPlayer;
}