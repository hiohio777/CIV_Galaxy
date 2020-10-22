using UnityEngine;

public class JustRotate : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    private void FixedUpdate()
    {
        transform.Rotate(speed * Vector3.forward * Time.deltaTime);
    }
}
