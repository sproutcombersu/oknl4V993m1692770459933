using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ListColoringManager : MonoBehaviour
{
    public static ListColoringManager instance;

    public List<ItemColoring> listColoring;


    private string keyInitListColoring = "initListColoring";
    private string keyIsUnlockColoring = "isUnlockColoring";

    private void Awake() {
        if(instance) {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLockUnlickListColoring() {
        Debug.Log("keyInitListColoring: "+PlayerPrefs.HasKey(keyInitListColoring));
        if(!PlayerPrefs.HasKey(keyInitListColoring)) {
            for(int i=0; i<listColoring.Count; i++) {
                if(listColoring[i].isLock) {
                    LockColoring(i);
                }
            }
            PlayerPrefs.SetString(keyInitListColoring, "true");
        } else {
            var tempColoring = new List<ItemColoring>();
            for(var i=0; i<listColoring.Count; i++) {
                listColoring[i].isLock = IsUnlockColoring(i);
                tempColoring.Add(listColoring[i]);
            }
            listColoring = tempColoring;
            
        }
       
    }

    public bool IsUnlockColoring(int index) {
        return PlayerPrefs.GetInt(keyIsUnlockColoring+index, 0) != 0;
    }

    private void LockColoring(int index) {
        PlayerPrefs.SetInt(keyIsUnlockColoring+index, 1);
    }
    public void UnlockColoring(int index) {
        PlayerPrefs.SetInt(keyIsUnlockColoring+index, 0);
    }
}
