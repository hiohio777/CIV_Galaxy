using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LocalisationGame
{
    private static LocalisationGame instance;
    public static LocalisationGame Instance {
        get {
            if (instance == null)
                return instance = new LocalisationGame();
            return instance;
        }
    }

    public Action LanguageChanged;
    public string CurrentLanguage { get; private set; }
    public List<string> Localisations { get; private set; } = new List<string>();
    private Dictionary<string, string> data = new Dictionary<string, string>();

    private LocalisationGame()
    {
        ChangeLanguageAutomatically();
    }

    public string GetLocalisationString(string key)
    {
        if (data.TryGetValue(key, out string value))
            return value;
        return $"Noname key ({key})";
    }

    public void ChangeLanguage(string newLanguage)
    {
        if (CurrentLanguage == newLanguage) return; //Данная локализация уже выбрана

        LoadLocalisation(CurrentLanguage = newLanguage);
        LanguageChanged?.Invoke();
    }

    private void LoadLocalisation(string newLanguage)
    {
        data = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.Load<TextAsset>($"Localisation/{newLanguage}").text);
    }

    public void ChangeLanguageAutomatically()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                ChangeLanguage("english");
                break;
            case SystemLanguage.Russian:
                ChangeLanguage("russian");
                break;
            default: ChangeLanguage("english"); break;
        }
    }
}