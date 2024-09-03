using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject PanelPause;
    [SerializeField] GameObject SettingBanner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSetting()
    {
        PanelPause.SetActive(true);
        SettingBanner.SetActive(true);
        PlayerMovement.Instance.setFalseCanRun();
        PlayerController.Instance.canAttack = false;
    }

    public void CloseSetting()
    {
        PanelPause.SetActive(false);
        SettingBanner.SetActive(false);
        PlayerMovement.Instance.setTrueCanRun();
        PlayerController.Instance.canAttack = true;
    }
}
