using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryCell : MonoBehaviour
{
    [SerializeField, Space(10)] private int researchCost = 10;
    [SerializeField, Space(10)] private List<DiscoveryCell> dependentSciences;
    [SerializeField] private bool isAvailable = false;
    [SerializeField] private Color colorResearch, colorAvailable;

    private SpriteRenderer _sprite;
    public event Action ResearchUI;
    public event Action<bool> AvailableUI;

    public Sprite SpriteIcon => _sprite.sprite;
    public int ResearchCost => researchCost;
    public bool IsResearch { get; private set; }
    public bool IsAvailable => isAvailable;
    public string Description => $"{name}_Description";
    public int CountSciencesRequired { get; set; }
    public IDiscoveryEffects[] Boneses { get; private set; }

    /// <summary>
    /// Изучить науку(открыть её)
    /// </summary>
    public void Study(ICivilization civilization)
    {
        IsResearch = true;
        _sprite.color = colorResearch;
        ResearchUI?.Invoke();

        foreach (var item in Boneses)
            item.ExecuteStudy(civilization, name);

        foreach (var item in dependentSciences)
            item.Progress(); // Попытаться открыть идущие далее по древу науки
    }

    private void Progress()
    {
        // Если изучены все необходимые науки, открыть эту науку для исследования
        if (--CountSciencesRequired <= 0)
        {
            isAvailable = true;
            _sprite.color = colorAvailable;

            AvailableUI?.Invoke(isAvailable);
        }
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        foreach (var item in dependentSciences)
            item.CountSciencesRequired++;

        Boneses = GetComponents<IDiscoveryEffects>();

        if (IsResearch)
        {
            ResearchUI?.Invoke();
            _sprite.color = colorResearch;
        }
        else if (isAvailable)
        {
            AvailableUI?.Invoke(isAvailable);
            _sprite.color = colorAvailable;
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmos() //Встроенный метод,необходим для работы с Gizmos объектами и их отрисовки
    {
        Gizmos.color = Color.green; //Назначаем цвет нашему объекту
        foreach (var item in dependentSciences)
            Gizmos.DrawLine(transform.position, item.transform.position);
    }
}
