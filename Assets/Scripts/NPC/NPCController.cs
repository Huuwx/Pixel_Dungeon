using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    public DialogueManager dialog;
    public DialogueManager dialog2;
    public DialogueManager dialog3;

    public GameObject Task;
    public GameObject AcceptBtn;
    public GameObject CancelBtn;

    public bool appearTask = false;
    public bool acceptTask = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("AcceptTask"))
        {
            if(PlayerPrefs.GetInt("AcceptTask") == 1)
                Task.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
