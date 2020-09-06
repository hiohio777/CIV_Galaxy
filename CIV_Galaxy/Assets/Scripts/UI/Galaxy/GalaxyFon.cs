using UnityEngine;

public class GalaxyFon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteFon;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
