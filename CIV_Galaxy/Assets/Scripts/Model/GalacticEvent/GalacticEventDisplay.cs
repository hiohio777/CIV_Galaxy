using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GalacticEventDisplay : MonoBehaviour, IGalacticEventDisplay
{
    [SerializeField] private MovingObject moving;
    [SerializeField] private SpriteRenderer art;
    [SerializeField] private Sprite dominationBonusSprite, industryBonusSprite, sciencePointBonusSprite, researchBonusSprite;

    private GalacticEvent _galacticEvent;
    private bool isActive = false;
    private Vector3 _positionTarget;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;
    }

    public void Show(GalacticEvent galacticEvent)
    {
        switch (_galacticEvent = galacticEvent)
        {
            case DominationBonus _: Assing(new Vector3(178, -209), dominationBonusSprite, Color.yellow); break;
            case IndustryBonus _: Assing(new Vector3(208, -364), industryBonusSprite, Color.red); break;
            case SciencePointBonus _: Assing(new Vector3(-204, -423), sciencePointBonusSprite, Color.green); break;
            case ResearchBonus _: Assing(new Vector3(-208, -363), researchBonusSprite, Color.cyan); break;
        }

        isActive = false;
        gameObject.SetActive(true);

        transform.position = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), 0);

        var startPosition = new Vector3(UnityEngine.Random.Range(-400, 400), UnityEngine.Random.Range(-50, 350), 0);
        moving.SetScale(3f, 0.3f).SetPosition(startPosition, 0.3f).Run(() => StartCoroutine(WaitAfter()));
    }

    public void EndEvent()
    {
        StopAllCoroutines();
        gameObject.SetActive(isActive = false);
    }

    public IEnumerator WaitAfter()
    {
        isActive = true;

        float timeSecond = 0;
        while (timeSecond < 2f)
        {
            if (_galaxyUITimer.IsPause == false)
                timeSecond += Time.deltaTime * _galaxyUITimer.GetSpeed;

            yield return null;
        }

        isActive = false;
        moving.SetScale(0f, 0.3f).Run(EndEvent);
    }

    private void Assing(Vector3 positionTarget, Sprite sprite, Color color)
    {
        _positionTarget = positionTarget;
        art.sprite = sprite;
        art.color = color;
    }

    private void OnMouseUp()
    {
        if (isActive)
        {
            StopAllCoroutines();
            isActive = false;
            moving.SetScale(1f, 0.2f).SetPosition(_positionTarget, 0.3f).Run(EndExecuteEvent);
        }
    }

    private void EndExecuteEvent()
    {
        _galacticEvent.Execute();
        EndEvent();
    }

}

public interface IGalacticEventDisplay
{
    void Show(GalacticEvent galacticEvent);
}
