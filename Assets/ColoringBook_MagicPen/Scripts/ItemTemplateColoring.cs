using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTemplateColoring : MonoBehaviour
{
   public Image imgColoring;
   public GameObject loading;

   public Text txtLoading;
   public GameObject lockItem;

   public void setImageColoring(Sprite sprite) {
        if(sprite) {
            imgColoring.sprite = sprite;
            loading.SetActive(false);
        } else {
            txtLoading.text = "Failed Load";
        }
      
   }
   public void setImageLock(bool isLock) {
        lockItem.SetActive(isLock);
   }
}
