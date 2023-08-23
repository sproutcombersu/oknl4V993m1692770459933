using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button btnPlay;
    // Start is called before the first frame update
    void Start()
    {
        btnPlay.onClick.AddListener(() => {
            SceneManager.LoadScene("SelectionScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
