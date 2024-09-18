using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI currentCount_1;
    [SerializeField] TextMeshProUGUI Target_1;
    [SerializeField] TextMeshProUGUI currentCount_2;
    [SerializeField] TextMeshProUGUI Target_2;

    private int progress_1;
    private int progress_2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Progress_1") && PlayerPrefs.HasKey("Progress_2"))
        {
            LoadProgress_1();
            LoadProgress_2();
        }
        else
        {
            PlayerPrefs.SetInt("Progress_1", 0);
            PlayerPrefs.SetInt("Progress_2", 0);
            PlayerPrefs.SetInt("Target_1", 5);
            PlayerPrefs.SetInt("Target_2", 5);
            currentCount_1.text = "0";
            currentCount_2.text = "0";
        }
    }

    public void SetProgress_1()
    {
        progress_1 = PlayerPrefs.GetInt("Progress_1");
        progress_1 += 1;
        currentCount_1.text = progress_1.ToString();
        PlayerPrefs.SetInt("Progress_1", progress_1);
        if(progress_1 == PlayerPrefs.GetInt("Target_1"))
        {
            Debug.Log("Tieu diet du 5 skeleton");
        }
    }

    public void SetProgress_2()
    {
        progress_2 = PlayerPrefs.GetInt("Progress_2");
        progress_2 += 1;
        currentCount_2.text = progress_2.ToString();
        PlayerPrefs.SetInt("Progress_2", progress_2);
        if(progress_2 == PlayerPrefs.GetInt("Target_2"))
        {
            Debug.Log("Tieu diet du 5 Orc");
        }
    }

    public void LoadProgress_1()
    {
        progress_1 = PlayerPrefs.GetInt("Progress_1");
        currentCount_1.text = progress_1.ToString();
        Target_1.text = PlayerPrefs.GetInt("Target_1").ToString();
    }

    public void LoadProgress_2()
    {
        progress_2 = PlayerPrefs.GetInt("Progress_2");
        currentCount_2.text = progress_2.ToString();
        Target_2.text = PlayerPrefs.GetInt("Target_2").ToString();
    }
}
