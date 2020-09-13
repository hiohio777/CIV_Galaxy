using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryCell : MonoBehaviour
{
    [SerializeField, Space(10)] private SpriteRenderer sprite;

    [SerializeField, Space(10)] private int researchCost = 10;
    [SerializeField, Space(10)] private List<DiscoveryCell> dependentSciences;
    [SerializeField] private bool isAvailable = false;
    [SerializeField] private Color colorResearch, colorAvailable;

    public event Action ResearchUI;
    public event Action<bool> AvailableUI;

    public Sprite SpriteIcon => sprite.sprite;
    public int ResearchCost => researchCost;
    public bool IsResearch { get; private set; }
    public bool IsAvailable => isAvailable;
    public string Description => $"{name}_Description";
    public int CountSciencesRequired { get; set; }

    /// <summary>
    /// Изучить науку(открыть её)
    /// </summary>
    public void Study(ICivilizationBase civilization)
    {
        IsResearch = true;
        sprite.color = colorResearch;
        ResearchUI?.Invoke();

        foreach (var item in GetComponents<IDiscoveryEffects>())
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
            sprite.color = colorAvailable;

            AvailableUI?.Invoke(isAvailable);
        }
    }

    private void Awake()
    {
        foreach (var item in dependentSciences)
            item.CountSciencesRequired++;

        if (IsResearch)
        {
            ResearchUI?.Invoke();
            sprite.color = colorResearch;
        }
        else if (isAvailable)
        {
            AvailableUI?.Invoke(isAvailable);
            sprite.color = colorAvailable;
        }
    }

    private void OnDrawGizmos() //Встроенный метод,необходим для работы с Gizmos объектами и их отрисовки
    {
        Gizmos.color = Color.green; //Назначаем цвет нашему объекту
        foreach (var item in dependentSciences)
        {
            Gizmos.DrawLine(transform.position, item.transform.position);
        }
    }
}
