using System;
using UnityEngine;

public class Planet : UnitBase, IPlanet
{
    [SerializeField] private SpriteRenderer art;
    [SerializeField] private Sprite[] spritesStars;

    public Planet Initialize()
    {
        gameObject.SetActive(true);

        switch (UnityEngine.Random.Range(0, 6))
        {
            case 0: art.color = new Color(1, 1, 0.3f, 1); break;
            case 1: art.color = new Color(0.3f, 1, 1, 1); break;
            case 2: art.color = new Color(0.3f, 1, 0.3f, 1); break;
            case 3: art.color = new Color(1, 0.3f, 0.3f, 1); break;
            default: art.color = new Color(1, 1, 1, 1); break;
        }

        art.sprite = spritesStars[UnityEngine.Random.Range(0, spritesStars.Length)];

        transform.position = new Vector3(0, 0, 0);
        art.sortingOrder = GetSortingOrder;

        return this;
    }

    #region IPlanet
    public void OpenPlanet(Vector3 positionTarget, float timeWait, Action actFinish)
    {
        TtransformUnit.localScale = new Vector3(0, 0, 0);
        TtransformUnit.position = new Vector3(UnityEngine.Random.Range(-60f, 60f), UnityEngine.Random.Range(0f, 100f), 0);

        Action endAct = () => SetScale(0f, 0.2f).Run(actFinish);
        SetScale(1.4f, 0.5f).SetWaitForSeconds(timeWait).Run(() => SetScale(1, 0.2f).Run(() => SetPosition(positionTarget, UnityEngine.Random.Range(1f, 2f)).Run(endAct)));
    }

    public void ConquestPlanets(Vector3 startPosition, Vector3 positionTarget, Action actFinish)
    {
        TtransformUnit.localScale = new Vector3(0, 0, 0);
        TtransformUnit.position = new Vector3(startPosition.x + UnityEngine.Random.Range(-30f, 30f), startPosition.y + UnityEngine.Random.Range(-30f, 30f), 0);

        float speed = UnityEngine.Random.Range(0.8f, 1f);

        Action endAct = () => SetScale(0f, 0.2f).Run(actFinish);
        Action toMoveTarget = () => SetPosition(positionTarget, speed).Run(endAct);
        Action toMoveGalaxyAct = () => SetPosition(new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f)), speed).Run(toMoveTarget);
        Action startAct = () => SetScale(0.5f, 0.2f).Run(toMoveTarget);
        SetScale(1f, 0.2f).Run(startAct);
    }
    #endregion
}

public interface IPlanet
{
    /// <summary>
    /// Открыть планету(получить новую планету из галактики)
    /// </summary>
    void OpenPlanet(Vector3 positionTarget, float timeWait, Action actFinish);
    void ConquestPlanets(Vector3 startPosition, Vector3 positionTarget, Action actFinish);
    void Destroy();
}
