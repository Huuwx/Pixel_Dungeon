using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;

public enum PlayerState
{
    idle,
    walk, 
    attack,
    dialogue
}
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public float size;
    private Vector3 interactPos;

    public GameObject HandUseWeapon;
    public GameObject HandHoldWeapon;
    public GameObject DialoguePanel;

    public PlayerState playerState;

    public bool canAttack = true;
    private bool canInterract = true;
    public float damage;

    public Vector3 posOfHandUseWeapon;
    public LayerMask enemyLayer;
    public LayerMask npcLayer;

    private Animator animator;

    SoundController soundController;

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
        soundController = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundController>();
        playerState = PlayerState.idle;
        canAttack = true;
        animator = GetComponent<Animator>();
        //PlayerMovement playerMov = gameObject.GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (Input.GetKeyDown(KeyCode.Z) && canInterract)
        {
            PlayerMovement playerMov = gameObject.GetComponent<PlayerMovement>();
            interactPos = transform.position + playerMov.facingDir * 0.75f;
            Collider2D collider = Physics2D.OverlapCircle(interactPos, size, npcLayer);

            if (collider != null)
            {
                Debug.Log("Phat hien NPC");
                NPCController npc = collider.GetComponent<NPCController>();
                //Debug.Log(npc.dialog.Lines[0]);
                //Debug.Log(npc.dialog.Lines[3]);
                //Dialogue.Instance.StartDialogue(npc.dialog);

                if (npc.dialog != null)
                {
                    canInterract = false;
                    playerState = PlayerState.idle;
                    Dialogue.Instance.StartDialogue(npc.dialog);
                    
                }
                else
                {
                    Debug.LogWarning("NPC dialog is null");
                }

            }
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            playerState = PlayerState.attack;
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
                if(collider != null)
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

    public void setCanInteract()
    {
        canInterract = true;
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

    public void SetPSDialogue()
    {
        animator.SetBool("IsRunning", false);
        PlayerMovement playerMov = gameObject.GetComponent<PlayerMovement>();
        playerMov.setFalseCanRun();
        canAttack = false;
        playerState = PlayerState.dialogue;
    }

    public void SetPSWalk()
    {
        PlayerMovement playerMov = gameObject.GetComponent<PlayerMovement>();
        playerMov.setTrueCanRun();
        canAttack = true;
        playerState = PlayerState.walk;
    }

    public void SetPSIdle()
    {
        PlayerMovement playerMov = gameObject.GetComponent<PlayerMovement>();
        playerMov.setTrueCanRun();
        canAttack = true;
        playerState = PlayerState.idle;
    }

    public void WalkSound()
    {
        soundController.PlayOneShot(soundController.playerWalk);
    }

    public void AttackSound()
    {
        soundController.PlayOneShot(soundController.playerAttack);
    }

    public void GetDamageSound()
    {
        soundController.PlayOneShot(soundController.playerGetDamaged);
    }
}
