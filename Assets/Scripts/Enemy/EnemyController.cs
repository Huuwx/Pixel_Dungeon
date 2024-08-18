using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;
    private bool isDead = false;
    public bool canRun = true;

    public float HP = 5;
    public Transform target;
    public LayerMask playerMask;
    public float chaseRadius;
    public float attackRadius;
    public float DamageSize;
    public Vector3 interactPos;
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
        if (!isDead)
            CheckPlayer();
    }

    public void CheckPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, chaseRadius, playerMask);
        if (collider != null)
        {
            if (target.position.x > transform.position.x)
            {
                transform.localScale = Vector3.one;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Mathf.Abs(target.position.y - transform.position.y) < 1)
            {
                if (target.position.x > transform.position.x)
                {
                    interactPos = transform.position + new Vector3(1, 0, 0) * 0.7f;
                }
                else if (target.position.x < transform.position.x)
                {
                    interactPos = transform.position + new Vector3(-1, 0, 0) * 0.7f;
                }
            }
            else
            {
                if (target.position.y - transform.position.y > 1)
                {

                    interactPos = transform.position + new Vector3(0, 1, 0) * 0.7f;

                }
                else
                {

                    interactPos = transform.position + new Vector3(0, -1, 0) * 0.7f;

                }
            }

            Collider2D attackRange = Physics2D.OverlapCircle(transform.position, attackRadius, playerMask);
            if (attackRange != null)
            {
                //isRun = false;
                //animator.SetBool("isRun", isRun);

                canRun = false;
                animator.SetTrigger("Attack");
                return;
            }
            if (canRun)
            {
                isRun = true;
                animator.SetBool("isRun", isRun);
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rigid.MovePosition(temp);
            }
        }
        else
        {
            if (transform.position != homePos)
            {
                if (homePos.x > transform.position.x)
                {
                    transform.localScale = Vector3.one;
                }
                else if (homePos.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (canRun)
                {
                    isRun = true;
                    animator.SetBool("isRun", isRun);
                    Vector3 temp = Vector3.MoveTowards(transform.position, homePos, moveSpeed * Time.deltaTime);
                    rigid.MovePosition(temp);
                }
            }
            else
            {
                isRun = false;

                animator.SetBool("isRun", isRun);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.DrawWireSphere(interactPos, DamageSize);
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP > 0)
        {
            animator.SetTrigger("Hit");
        }
        else if (HP <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
    }

    public void setTrueCanRun()
    {
        canRun = true;
    }

    public void setFalseCanRun()
    {
        canRun = false;
    }

    public void AttackPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(interactPos, DamageSize, playerMask);
        if (player != null)
        {
            Debug.Log("chet me may chua");
            Health playerHealth = player.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(1);
        }

    }
}
