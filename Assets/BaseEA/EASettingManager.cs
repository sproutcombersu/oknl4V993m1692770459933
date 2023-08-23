using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EASettingManager : MonoBehaviour
{

    public static EASettingManager instance;
    public Sprite spriteBgMenu;
    public Sprite spriteTitleMenu;
    public Sprite spritePaintBookMenu;
    public Sprite spriteColoringBookMenu;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
    }

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

}
