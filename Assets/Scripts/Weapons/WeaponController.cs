using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    SpriteRenderer spriteRenderer;

    public List<Sprite> listWeapons;
    public List<float> damgeOfWeapon;  

    public int indexWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = listWeapons[indexWeapon];
        playerController.damage = damgeOfWeapon[indexWeapon];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            ChangeWeapon();
        }
    }

    public void ChangeWeapon()
    {
        if (indexWeapon < listWeapons.Count - 1)
        {
            indexWeapon++;
            spriteRenderer.sprite = listWeapons[indexWeapon];
            playerController.damage = damgeOfWeapon[indexWeapon];
        }
        else if (indexWeapon == listWeapons.Count - 1)
        {
            indexWeapon = 0;
            spriteRenderer.sprite = listWeapons[indexWeapon];
            playerController.damage = damgeOfWeapon[indexWeapon];
        }
    }
}
