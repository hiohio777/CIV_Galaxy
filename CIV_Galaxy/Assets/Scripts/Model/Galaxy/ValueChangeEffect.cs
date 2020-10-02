using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ValueChangeEffect : MonoBehaviour
{
    [SerializeField] private Color buffUp, debuff;
    [SerializeField] private Vector3 startPosition;
    private Text _textCount;
    private Animator _animator;
    private Action<object> _buffered;
    private Transform _transform;
    private IGalaxyUITimer _galaxyUITimer;
    private Action _finishAct;

    public class Factory : PlaceholderFactory<Action<object>, ValueChangeEffect> { }

    [Inject]
    public void Inject(Action<object> buffered, IGalaxyUITimer galaxyUITimer)
    {
        this._buffered = buffered;
        this._galaxyUITimer = galaxyUITimer;
        this._galaxyUITimer.PauseAct += GalaxyUITimer_PauseAct;

        _transform = transform;
        _textCount = GetComponent<Text>();
        _animator = GetComponent<Animator>();
    }

    private void GalaxyUITimer_PauseAct(bool isPause)
    {
        if (isPause) _animator.speed = 0;
        else _animator.speed = 1;
    }

    public void Display(Transform parent, string count, bool ifBuff, Action finishAct)
    {
        gameObject.SetActive(true);
        _finishAct = finishAct;

        _transform.SetParent(parent, false);
        _transform.position = startPosition + parent.position;

        if (ifBuff) DisplayBuffUp(count);
        else DisplayBuffDown(count);

        _animator.SetTrigger("ShowValueChangeEffect");
    }

    public void Execute()
    {
        _finishAct.Invoke();
    }

    public void Destroy()
    {
        _transform.SetParent(null, false);
        gameObject.SetActive(false);
        _buffered?.Invoke(this); // Поместить в буфер для переиспользования
    }

    private void DisplayBuffUp(string count)
    {
        _textCount.text = $"+{count}";
        _textCount.color = buffUp;
    }
    private void DisplayBuffDown(string count)
    {

        _textCount.text = count;
        _textCount.color = debuff;
    }
}
