using System.Collections.Generic;
using UnityEngine;

public class TreeOfScience : MonoBehaviour, ITreeOfScience
{
    public string Name { get => name; set => name = value; }
    public DiscoveryCell[] Discoveries { get; private set; }
    public List<DiscoveryCell> AvailableDiscoveries { get; } = new List<DiscoveryCell>();

    public void CreatAvailableDiscoveriesList()
    {
        AvailableDiscoveries.Clear();
        foreach (var item in Discoveries)
        {
            if (item.IsAvailable && item.IsResearch == false)
                AvailableDiscoveries.Add(item);
        }
    }

    private void Start()
    {
        Discoveries = GetComponentsInChildren<DiscoveryCell>();
        gameObject.SetActive(false);
    }
}

public interface ITreeOfScience
{
    DiscoveryCell[] Discoveries { get; }
    List<DiscoveryCell> AvailableDiscoveries { get; }
    void CreatAvailableDiscoveriesList();

    string Name { get; set; }
}