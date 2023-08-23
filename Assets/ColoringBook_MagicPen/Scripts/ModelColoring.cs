using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JSONData {
    public MainMenu mainMenu { get; set; }
    public List<ItemColoring> listColoring { get; set; }
}

[System.Serializable]
public class ItemColoring {
    public string image;
    public bool isLock;
    public Sprite spriteImage;
}

public class MainMenu
{
    public string urlBgMenu;
    public string urlTitleMenu;
    public string urlPaintBookMenu;
    public string urlColoringBookMenu;
}
