using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject PanelPause;
    [SerializeField] GameObject SettingBanner;
    [SerializeField] GameObject reviveBtn;

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

    public void ReviveBtn()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        soundController.PlayOneShot(soundController.Click);
        PanelPause.SetActive(false);
        reviveBtn.SetActive(false);
        SceneController.Instance.LoadScene(SceneName);
    }

    public void DeadPanel()
    {
        PanelPause.SetActive(true);
        reviveBtn.SetActive(true);
    }

    public void AcceptTask()
    {
        NPCController npcController = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCController>();
        Debug.Log("task appear");
        PlayerPrefs.SetInt("AcceptTask", 1);
        PlayerPrefs.SetInt("Progress_1", 0);
        PlayerPrefs.SetInt("Progress_2", 0);
        PlayerPrefs.SetInt("Target_1", 5);
        PlayerPrefs.SetInt("Target_2", 5);
        npcController.Task.SetActive(true);
        TaskManager.Instance.LoadProgress_1();
        TaskManager.Instance.LoadProgress_2();
        Dialogue.Instance.SetFalseDialogue();
    }

    public void CancelTask()
    {
        Dialogue.Instance.SetFalseDialogue();
    }
}
