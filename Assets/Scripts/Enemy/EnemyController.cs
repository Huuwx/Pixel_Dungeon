using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;


    public Transform target;
    public LayerMask playerMask;
    public float chaseRadius;
    public float attackRadius;
    public Vector3 homePos;
    public float moveSpeed;
    bool isRun;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        homePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    public void CheckPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, chaseRadius, playerMask);
        if (collider != null)
        {
            isRun = true;
            animator.SetBool("isRun",isRun);
            if(target.position.x > transform.position.x)
            {
                transform.localScale = Vector3.one;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            Collider2D attackRange = Physics2D.OverlapCircle(transform.position, attackRadius, playerMask);
            if(attackRange != null)
            {
                isRun = false;
                animator.SetBool("isRun", isRun);
                return;
            }
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            rigid.MovePosition(temp);
        }
        else
        {
            if(transform.position != homePos)
            {
                isRun = true;
                animator.SetBool("isRun", isRun);
                if (homePos.x > transform.position.x)
                {
                    transform.localScale = Vector3.one;
                }
                else if (homePos.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                Vector3 temp = Vector3.MoveTowards(transform.position, homePos, moveSpeed * Time.deltaTime);
                rigid.MovePosition(temp);
            }
            else
            {
                isRun = false;
                animator.SetBool("isRun", isRun) ;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
