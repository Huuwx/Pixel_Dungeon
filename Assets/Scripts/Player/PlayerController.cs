using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float size;
    private Vector3 interactPos;

    public GameObject HandUseWeapon;
    public GameObject HandHoldWeapon;
    public bool canAttack = true;
    public float damage;

    public Vector3 posOfHandUseWeapon;
    public LayerMask enemyLayer;

    private Animator animator;

    private void Awake()
    {
        canAttack = true;
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            canAttack = false;
            PlayerMovement playerMov = gameObject.GetComponent<PlayerMovement>();

            if (playerMov.facingDir.y < 0)
            {
                animator.SetTrigger("SlashDown");
            }
            else if (playerMov.facingDir.y > 0)
            {
                animator.SetTrigger("SlashUp");
            }
            else
            {
                animator.SetTrigger("Slash");
            }

            interactPos = transform.position + playerMov.facingDir * 0.75f;

            playerMov.setFalseCanRun();

            Collider2D[] collliders = Physics2D.OverlapCircleAll(interactPos, size, enemyLayer);
            
            foreach(Collider2D collider in collliders)
            {
                if(collider.tag == "Enemy")
                {
                    Debug.Log("Minus enemy's HP");
                    EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
                    enemy.TakeDamage(damage);
                }
            }

            //if(playerMov.facingDir.x > 0 && playerMov.facingDir.y > 0)
            //{
            //    HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, -45);
            //}
            //else if(playerMov.facingDir.x < 0 && playerMov.facingDir.y > 0)
            //{
            //    HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 45);
            //}
            //else if(playerMov.facingDir.x > 0 && playerMov.facingDir.y < 0)
            //{
            //    HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, -135);
            //}
            //else if (playerMov.facingDir.x < 0 && playerMov.facingDir.y < 0)
            //{
            //    HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 135);
            //}
            //else if(playerMov.facingDir.x == 1)
            //{
            //    HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, -90);
            //}
            //else if (playerMov.facingDir.x == -1)
            //{
            //    HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 90);
            //}
            //else if (playerMov.facingDir.y == -1)
            //{
            //    HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 180);
            //}
            //else
            //{
            //    HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            //}

            //HandHoldWeapon.SetActive(false);
            //HandUseWeapon.SetActive(true);

            //canAttack = false;
            //HandUseWeapon.transform.position = transform.position + playerMov.facingDir * 0.7f;

            //Invoke(nameof(SetHandPos), 0.5f);

            Debug.Log("chem");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactPos, size);
    }

    private void SetCanAttack()
    {
        canAttack = true;
    }

    public void Dead()
    {
        PlayerMovement playerMov = gameObject.GetComponent<PlayerMovement>();
        playerMov.setFalseCanRun();
        canAttack = false;
        animator.SetTrigger("Death");
    }

    public void TakeDamageAnimation()
    {
        animator.SetTrigger("Hit");
    }
}
