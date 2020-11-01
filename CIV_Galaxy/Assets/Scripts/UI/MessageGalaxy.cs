using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageGalaxy : NoRegisterMonoBehaviour
{
    [SerializeField] private Text messadge;
    private Action _endAct;

    public void Show(string textKey) => Show(textKey, new Color(0.27f, 0.63f, 1, 1));
    public void Show(string textKey, Action endAct) => Show(textKey, new Color(0.27f, 0.63f, 1, 1), endAct);
    public void Show(string textKey, Color color, Action endAct = null)
    {
        gameObject.SetActive(true);
        this._endAct = endAct;

        messadge.color = color;
        messadge.text = textKey;

        GetComponent<Animator>().SetTrigger("Show");
    }

    public void HideMessage()
    {
        _endAct.Invoke();
        gameObject.SetActive(false);
    }
}
