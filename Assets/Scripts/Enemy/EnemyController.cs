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
    bool isRun;
    bool cooldown = true;

    public float HP = 5;
    public float chaseRadius;
    public float attackRadius;
    public float DamageSize;
    public float moveSpeed;
    public float timeOfCoolDown = 1.4f;

    public Transform target;

    public LayerMask playerMask;

    public Vector3 interactPos;
    public Vector3 homePos;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        homePos = transform.position;
        chaseRadius = 3f;
        attackRadius = 0.7f;
        DamageSize = 0.5f;
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

            if (canRun)
            {
                isRun = true;
                animator.SetBool("isRun", isRun);
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rigid.MovePosition(temp);
            }

            Collider2D attackRange = Physics2D.OverlapCircle(transform.position, attackRadius, playerMask);
            if (attackRange != null)
            {
                //isRun = false;
                //animator.SetBool("isRun", isRun);

                canRun = false;
                isRun = false;
                animator.SetBool("isRun", isRun);

                if (cooldown == true)
                {
                    animator.SetTrigger("Attack");
                    cooldown = false;
                    Invoke(nameof(SetTrueCoolDown), timeOfCoolDown);
                }
                return;
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

    public void SetTrueCoolDown()
    {
        cooldown = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            canRun = false;
        }
    }
}
