using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JustRotate : MonoBehaviour
{
    public float speed = 10;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;
    }

    private void FixedUpdate()
    {
        if (_galaxyUITimer.IsPause == false)
            transform.Rotate(speed * Vector3.forward * Time.deltaTime);
    }
}
