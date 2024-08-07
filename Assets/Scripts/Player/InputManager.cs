using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(InputManager).ToString());
                    instance = singleton.AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }
    //private Vector3 input;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This will keep the instance alive across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Enforces singleton pattern, meaning there can only ever be one instance.
        }
    }

    public Vector3 InputFromKeyBoard(Vector3 input)
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        Debug.Log(input.ToString());
        return input;
    }
}
