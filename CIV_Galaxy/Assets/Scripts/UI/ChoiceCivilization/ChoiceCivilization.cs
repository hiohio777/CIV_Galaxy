using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ChoiceCivilization : PanelUI
{
    private PlayerSettings _playerData;

    [Inject]
    public void Inject(PlayerSettings playerData)
    {
        this._playerData = playerData;
    }

    public void StartGalaxyScene()
    {
        Debug.Log("StartGalaxyScene!");

        switch (_playerData.CurrentOpponents)
        {
            case OpponentsEnum.Two:
                SceneManager.LoadScene("GalaxyScene_2"); break;
            case OpponentsEnum.Four:
                SceneManager.LoadScene("GalaxyScene_4"); break;
            case OpponentsEnum.Six:
                SceneManager.LoadScene("GalaxyScene_6"); break;
        }
    }

    public void AnimationClose()
    {
        animator.SetTrigger("Close");
    }
}
