using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject PanelPause;
    [SerializeField] GameObject SettingBanner;

    [SerializeField] AudioClip ClickSound;

    public void OpenSetting()
    {
        SoundController.Instance.PlayOneShot(ClickSound);
        PanelPause.SetActive(true);
        SettingBanner.SetActive(true);
        PlayerMovement.Instance.setFalseCanRun();
        PlayerController.Instance.canAttack = false;
    }

    public void CloseSetting()
    {
        SoundController.Instance.PlayOneShot(ClickSound);
        PanelPause.SetActive(false);
        SettingBanner.SetActive(false);
        PlayerMovement.Instance.setTrueCanRun();
        PlayerController.Instance.canAttack = true;
    }

    public void BackHome()
    {
        SoundController.Instance.PlayOneShot(ClickSound);
        SceneController.Instance.LoadScene("HomeScene");
    }

    public void StartGame()
    {
        SoundController.Instance.PlayOneShot(ClickSound);
        SceneController.Instance.LoadScene("SampleScene");
    }

    public void OpenSettingInHome()
    {
        SoundController.Instance.PlayOneShot(ClickSound);
    }

    public void CloseGame()
    {
        SoundController.Instance.PlayOneShot(ClickSound);
        Application.Quit();
    }
}
