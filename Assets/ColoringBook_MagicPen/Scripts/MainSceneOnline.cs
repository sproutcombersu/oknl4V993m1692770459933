using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneOnline : MonoBehaviour
{

    public Image imageBgMenu;
    public Image imageTitleMenu;
    public Image imagePaintBookMenu;
    public Image imageColoringBookMenu;
    
    void Awake()
    {
        if(EASettingManager.instance) {
            if(EASettingManager.instance.spriteBgMenu) {
                imageBgMenu.sprite = EASettingManager.instance.spriteBgMenu;
            }
            if(EASettingManager.instance.spriteTitleMenu) {
                imageTitleMenu.sprite = EASettingManager.instance.spriteTitleMenu;
            }
            if(EASettingManager.instance.spritePaintBookMenu) {
                imagePaintBookMenu.sprite = EASettingManager.instance.spritePaintBookMenu;
            }
            if(EASettingManager.instance.spriteColoringBookMenu) {
                imageColoringBookMenu.sprite = EASettingManager.instance.spriteColoringBookMenu;
            }
        }
        if(AdsManagerWrapper.INSTANCE) {
            AdsManagerWrapper.INSTANCE.HideBanner();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
