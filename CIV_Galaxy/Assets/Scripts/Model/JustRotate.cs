using System;
using System.Collections;
using UnityEngine;

public class JustRotate : MonoBehaviour
{
    [SerializeField] private float speed = 5, timeScale = 1;
    private Transform _transform;
    private Action _actionFinal;

    public void StartResize(Action actionFinal, Vector3 target)
    {
        _transform = transform;
        this._actionFinal = actionFinal;

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

        _actionFinal.Invoke();
    }

    private void FixedUpdate()
    {
        transform.Rotate(speed * Vector3.forward * Time.deltaTime);
    }
}
