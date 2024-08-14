using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 facingDir = new Vector3(1, 0, 0);

    public Vector3 input = new Vector3();

    public float moveSpeed = 1f;

    public bool isRunning = false;
    private bool canRun = true;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canRun)
        {
            input = InputManager.Instance.InputFromKeyBoard(input);
            if (input != Vector3.zero)
            {
                //var targetPos = transform.position;
                //targetPos.x += input.x;
                //targetPos.y += input.y;

                var currentPos = transform.position;

                input.Normalize();
                facingDir = input;
                Movement(currentPos, input.x, input.y);
                if (input.x > 0)
                {
                    transform.localScale = Vector3.one;

                }
                else if (input.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                isRunning = true;
                animator.SetBool("IsRunning", isRunning);
            }
            else
            {
                isRunning = false;
                animator.SetBool("IsRunning", isRunning);
            }
        }
    }

    public void Movement(Vector2 Pos, float horizontal, float vertical)
    {
        Pos.x += moveSpeed * horizontal * Time.deltaTime;
        Pos.y += moveSpeed * vertical * Time.deltaTime;

        transform.position = Pos;
    }

    public void setTrueCanRun()
    {
        canRun = true;
    }

    public void setFalseCanRun()
    {
        canRun = false;
    }
}
