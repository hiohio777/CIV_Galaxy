using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceCivilization : PanelUI
{
    public void StartGalaxyScene()
    {
        Debug.Log("StartGalaxyScene!");

        SceneManager.LoadScene("GalaxyScene");
    }

    public void AnimationClose()
    {
        animator.SetTrigger("Close");
    }
}
