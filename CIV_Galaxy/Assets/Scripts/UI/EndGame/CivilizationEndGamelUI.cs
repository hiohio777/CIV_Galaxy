using UnityEngine;
using UnityEngine.UI;

public class CivilizationEndGamelUI : MonoBehaviour
{
    [SerializeField] private Image art, fon;
    [SerializeField] private Sprite fonSpritePlayer;
    [SerializeField, Space(10)] private LocalisationText nameCiv;
    [SerializeField] private Text countPlanet;
    [SerializeField] private Text countDominationPoints;
    [SerializeField] private Image dominatorIcon;

    public void Assign(ICivilization civilization)
    {
        if (civilization is ICivilizationPlayer)
            fon.sprite = fonSpritePlayer;

        art.sprite = civilization.DataBase.Icon;
        nameCiv.SetKey(civilization.DataBase.Name);

        countDominationPoints.text = ((int)civilization.CivData.DominationPoints).ToString("#,#");
        countPlanet.text = civilization.CivData.Planets.ToString();

        switch (civilization.Lider)
        {
            case LeaderEnum.Lagging:
                dominatorIcon.enabled = false;
                countDominationPoints.color = new Color(0.6f, 0.6f, 0, 1);
                break;
            case LeaderEnum.Advanced:
                dominatorIcon.enabled = true;
                countDominationPoints.color = new Color(1f, 1f, 0, 1);
                dominatorIcon.color = new Color(1, 1, 0, 0.15f);
                break;
            case LeaderEnum.Leader:
                dominatorIcon.enabled = true;
                countDominationPoints.color = new Color(1, 1, 0, 1);
                dominatorIcon.color = new Color(1, 1, 0, 0.75f);
                break;
        }
    }
}