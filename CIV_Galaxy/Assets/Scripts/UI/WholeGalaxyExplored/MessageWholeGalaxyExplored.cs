using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessageWholeGalaxyExplored : MonoBehaviour
{
    [SerializeField] private Text messadge;

    public void Show(string textKey) => Show(textKey, new Color(0.27f, 0.63f, 1, 1));
    public void Show(string textKey, Color color)
    {
        gameObject.SetActive(true);

        messadge.color = color;
        messadge.text = textKey;

        GetComponent<Animator>().SetTrigger("Show");
    }

    public void Hide() => gameObject.SetActive(false);
}
