using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GalacticEventDisplay : MonoBehaviour, IGalacticEventDisplay
{
    [SerializeField] private MovingObject moving;
    [SerializeField] private SpriteRenderer art;
    [SerializeField]
    private Sprite dominationBonusSprite, industryBonusSprite, researchBonusSprite, progressAbiliryBonusSprite, ProgressScanerBonusSprite;
    [SerializeField, Space(10)] private Color activeColor;
    [SerializeField] private float timeWait = 2f;

    private bool isActive = false;
    private SpriteRenderer _fon;
    private Action _execute;
    private Vector3 _positionTarget;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;
        _fon = GetComponent<SpriteRenderer>();
    }

    public void Show(Action execute, GalaxyTypeEventEnum typeEvent)
    {
        _fon.color = new Color(0.4f, 0.5f, 0.7f, 1);
        art.color = new Color(0.4f, 0.5f, 0.7f, 1);
        transform.localScale = new Vector3(0, 0, 0);

        this._execute = execute;
        switch (typeEvent)
        {
            case GalaxyTypeEventEnum.IndustryBonus: _positionTarget = new Vector3(208, -364); art.sprite = industryBonusSprite; break;
            case GalaxyTypeEventEnum.ResearchBonus: _positionTarget = new Vector3(-208, -363); art.sprite = researchBonusSprite; break;
            case GalaxyTypeEventEnum.ProgressAbiliryBonus: _positionTarget = new Vector3(570, -400); art.sprite = progressAbiliryBonusSprite; break;
            case GalaxyTypeEventEnum.ProgressScanerBonus: _positionTarget = new Vector3(0, -230); art.sprite = ProgressScanerBonusSprite; break;
            case GalaxyTypeEventEnum.DominationBonus: _positionTarget = new Vector3(178, -209); art.sprite = dominationBonusSprite; break;
            default: _positionTarget = new Vector3(178, -209); art.sprite = dominationBonusSprite; break;
        }

        isActive = false;
        gameObject.SetActive(true);

        transform.position = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), 0);

        var startPosition = new Vector3(UnityEngine.Random.Range(-400, 400), UnityEngine.Random.Range(-50, 350), 0);
        moving.SetScale(2.5f, 0.4f).SetPosition(startPosition, 0.4f).Run(() => StartCoroutine(WaitAfter()));
    }

    public void EndEvent()
    {
        StopAllCoroutines();
        gameObject.SetActive(isActive = false);
    }

    public IEnumerator WaitAfter()
    {
        isActive = true;
        art.color = activeColor;
        _fon.color = Color.white;

        float timeSecond = 0;
        while (timeSecond < timeWait)
        {
            if (_galaxyUITimer.IsPause == false)
                timeSecond += Time.deltaTime * _galaxyUITimer.GetSpeed;

            yield return null;
        }

        isActive = false;
        art.color = new Color(0.4f, 0.5f, 0.7f, 1);
        _fon.color = new Color(0.4f, 0.5f, 0.7f, 1);

        moving.SetScale(0f, 0.3f).SetPosition(new Vector3(0, 0), 0.3f).Run(EndEvent);
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
        _execute?.Invoke();
        EndEvent();
    }

}

public interface IGalacticEventDisplay
{
    void Show(Action execute, GalaxyTypeEventEnum typeEvent);
}
