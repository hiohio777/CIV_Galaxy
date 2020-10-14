using System;
using System.Collections;
using UnityEngine;

public class JustRotateArtGalaxy : MonoBehaviour
{
    [SerializeField] private float speed = 5, timeScale = 1;
    private Transform _transform;
    private Action _actionFinal;

    private void Awake()
    {
        _transform = transform;
    }

    public void StartResize(Vector3 target, float timeScale = 1, Action actionFinal = null)
    {
        StopAllCoroutines();
        this.timeScale = timeScale;
        this._actionFinal = actionFinal;
        if (_transform.localScale.x == target.x)
        {
            _actionFinal?.Invoke();
        }

        StartCoroutine(Resize(target));
    }

    private IEnumerator Resize(Vector3 target)
    {
        float speedMove = Vector2.Distance(_transform.localScale, target) / timeScale;

        while (_transform.localScale != target)
        {
            float stepMove = speedMove * Time.deltaTime;
            _transform.localScale = Vector3.MoveTowards(_transform.localScale, target, stepMove);

            yield return new WaitForFixedUpdate();
        }

        _actionFinal?.Invoke();
    }

    private void FixedUpdate()
    {
        transform.Rotate(speed * Vector3.forward * Time.deltaTime);
    }
}
