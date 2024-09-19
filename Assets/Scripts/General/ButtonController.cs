using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject PanelPause;
    [SerializeField] GameObject SettingBanner;

    SoundController soundController;

    private void Awake()
    {
        soundController = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundController>();
    }

    public void OpenSetting()
    {
        soundController.PlayOneShot(soundController.Click);
        PanelPause.SetActive(true);
        SettingBanner.SetActive(true);
        PlayerMovement.Instance.setFalseCanRun();
        PlayerController.Instance.canAttack = false;
    }

    public void CloseSetting()
    {
        soundController.PlayOneShot(soundController.Click);
        PanelPause.SetActive(false);
        SettingBanner.SetActive(false);
        PlayerMovement.Instance.setTrueCanRun();
        PlayerController.Instance.canAttack = true;
    }

    public void BackHome()
    {
        soundController.PlayOneShot(soundController.Click);
        SceneController.Instance.LoadScene("HomeScene");
    }

    public void StartGame()
    {
        soundController.PlayOneShot(soundController.Click);
        SceneController.Instance.LoadScene("SampleScene");
    }

    public void CloseGame()
    {
        soundController.PlayOneShot(soundController.Click);
        Application.Quit();
    }

    public void OpenVolumeSetting()
    {
        soundController.PlayOneShot(soundController.Click);
        SceneController.Instance.LoadScene("MusicSettingScene");
    }

    public void AcceptTask()
    {
        NPCController npcController = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCController>();
        Debug.Log("task appear");
        npcController.Task.SetActive(true);
        Dialogue.Instance.SetFalseDialogue();
    }

    public void CancelTask()
    {
        Dialogue.Instance.SetFalseDialogue();
    }
}
