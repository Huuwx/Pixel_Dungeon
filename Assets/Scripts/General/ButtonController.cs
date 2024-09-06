using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject PanelPause;
    [SerializeField] GameObject SettingBanner;

    public void OpenSetting()
    {
        PanelPause.SetActive(true);
        SettingBanner.SetActive(true);
        PlayerMovement.Instance.setFalseCanRun();
        PlayerController.Instance.canAttack = false;
    }

    public void CloseSetting()
    {
        PanelPause.SetActive(false);
        SettingBanner.SetActive(false);
        PlayerMovement.Instance.setTrueCanRun();
        PlayerController.Instance.canAttack = true;
    }

    public void BackHome()
    {
        SceneController.Instance.LoadScene("HomeScene");
    }

    public void StartGame()
    {
        SceneController.Instance.LoadScene("SampleScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
