using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    private Dialogue instance;

    public static Dialogue Instance { get; private set; }

    [SerializeField] GameObject DialogueBox;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public DialogueManager dialog;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        textComponent.text = string.Empty;
    }

    private void Start()
    {
        StartDialogue(dialog);
    }

    // Update is called once per frame
    void Update()
    {
        TypeDialogue();
    }

    public void TypeDialogue()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            if (textComponent.text == dialog.Lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialog.Lines[index];
            }
        }
    }

    public void StartDialogue(DialogueManager dialogRef)
    {
        PlayerController.Instance.SetPSDialogue();
        dialog = dialogRef;
        index = 0;
        textComponent.text = string.Empty;
        DialogueBox.SetActive(true);
        StartCoroutine(TypeLine(dialog.Lines[index]));
    }

    IEnumerator TypeLine(string line)
    {
        foreach (char c in line.ToCharArray()) 
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < dialog.Lines.Count - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine(dialog.Lines[index]));
        }
        else
        {
            NPCController npcController = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCController>();
            if (npcController != null)
            {
                if (npcController.appearTask && !PlayerPrefs.HasKey("Target_1") && !PlayerPrefs.HasKey("Target_2"))
                {
                    npcController.AcceptBtn.SetActive(true);
                    npcController.CancelBtn.SetActive(true);
                }
                else
                {
                    DialogueBox.SetActive(false);
                    PlayerController.Instance.setCanInteract();
                    PlayerController.Instance.SetPSIdle();
                }
            } 
        }
    }

    public void SetFalseDialogue()
    {
        NPCController npcController = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCController>();
        npcController.AcceptBtn.SetActive(false);
        npcController.CancelBtn.SetActive(false);
        npcController.appearTask = false;
        DialogueBox.SetActive(false);
        PlayerController.Instance.setCanInteract();
        PlayerController.Instance.SetPSIdle();
    }
}
