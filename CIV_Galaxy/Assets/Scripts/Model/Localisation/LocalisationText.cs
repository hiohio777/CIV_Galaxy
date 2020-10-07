using UnityEngine;
using UnityEngine.UI;

public class LocalisationText : MonoBehaviour
{
    [SerializeField] private string Key;
    [HideInInspector] public Text text;

    public Color Color { set => text.color = value; }

    public void SetKey(string newKey)
    {
        Key = newKey;
        if (text == null) text = GetComponent<Text>();
        SetLocalisationString();
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void Awake()
    {
        text = GetComponent<Text>();
        LocalisationGame.Instance.LanguageChanged += SetLocalisationString;
        SetLocalisationString();
    }

    private void OnDestroy() => LocalisationGame.Instance.LanguageChanged -= SetLocalisationString;
    private void SetLocalisationString() => text.text = LocalisationGame.Instance.GetLocalisationString(Key);
}