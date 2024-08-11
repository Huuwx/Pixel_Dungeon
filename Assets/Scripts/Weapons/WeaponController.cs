using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    public Sprite[] listWeapons;

    public int indexWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = listWeapons[indexWeapon];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            if (gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
                ChangeWeapon();
                gameObject.SetActive(false);
            }
            else
            {
                ChangeWeapon();
            }

        }
    }

    public void ChangeWeapon()
    {
        if (indexWeapon < listWeapons.Length - 1)
        {
            indexWeapon++;
            spriteRenderer.sprite = listWeapons[indexWeapon];
        }
        else if (indexWeapon == listWeapons.Length - 1)
        {
            indexWeapon = 0;
            spriteRenderer.sprite = listWeapons[indexWeapon];
        }
    }
}
