using UnityEngine;

public class MainMenu : PanelUI
{
    [SerializeField] private JustRotateArtGalaxy imageArtGalaxy;

    public override void Enable()
    {
        imageArtGalaxy.StartResize(new Vector3(1, 1, 1), 1, base.Enable);
    }
}
