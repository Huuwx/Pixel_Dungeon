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

    public Vector3 posOfHandUseWeapon;

    private void Awake()
    {
        canAttack = true;
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
            
            PlayerMovement playerMov = gameObject.GetComponent<PlayerMovement>();
            interactPos = transform.position + playerMov.facingDir;

            Debug.Log(playerMov.facingDir);

            Collider2D[] collliders = Physics2D.OverlapCircleAll(interactPos, size, 0);

            if(playerMov.facingDir.x > 0 && playerMov.facingDir.y > 0)
            {
                HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if(playerMov.facingDir.x < 0 && playerMov.facingDir.y > 0)
            {
                HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if(playerMov.facingDir.x > 0 && playerMov.facingDir.y < 0)
            {
                HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, -135);
            }
            else if (playerMov.facingDir.x < 0 && playerMov.facingDir.y < 0)
            {
                HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 135);
            }
            else if(playerMov.facingDir.x == 1)
            {
                HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (playerMov.facingDir.x == -1)
            {
                HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (playerMov.facingDir.y == -1)
            {
                HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                HandUseWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            HandHoldWeapon.SetActive(false);
            HandUseWeapon.SetActive(true);

            canAttack = false;
            HandUseWeapon.transform.position = transform.position + playerMov.facingDir * 0.7f;

            Invoke(nameof(SetHandPos), 0.5f);

            Debug.Log("chem");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactPos, size);
    }

    private void SetHandPos()
    {
        HandUseWeapon.transform.position = posOfHandUseWeapon;
        HandHoldWeapon.SetActive(true);
        canAttack = true;
    }
}
