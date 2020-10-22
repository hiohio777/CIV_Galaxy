using UnityEngine;

public class JustRotateGalaxyUITimer : RegisterMonoBehaviour
{
    public float speed = 10;
    private IGalaxyUITimer _galaxyUITimer;

    public void Start()
    {
        _galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
    }

    private void FixedUpdate()
    {
        if (_galaxyUITimer.IsPause == false)
            transform.Rotate(speed * Vector3.forward * Time.deltaTime);
    }
}
