using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ColoringBookMagicPen;

public class ScrollViewColoring : MonoBehaviour
{
    public GameObject parentScrollView;
    public GameObject templateItemColoring;

    public ScrollListManager ScrollListManager;

    // Start is called before the first frame update

    private void Awake()
    {
        SetListColoring();
    }

    private void SetListColoring()
    {
        if(ListColoringManager.instance) {
            ListColoringManager.instance.UpdateLockUnlickListColoring();
            var index = 0;
            foreach (var item in ListColoringManager.instance.listColoring)
            {
                var obj = Instantiate(templateItemColoring, parentScrollView.transform);
                obj.name = index.ToString();
                obj.GetComponent<Button>().onClick.AddListener(() => clickItem(item, int.Parse(obj.name)));
                var itemTemplateColoring = obj.GetComponent<ItemTemplateColoring>();
                itemTemplateColoring.setImageLock(item.isLock);
                loadImage(item, itemTemplateColoring);
                index++;
            }
        }
       
    }

    private void clickItem(ItemColoring item, int index) {
        if(item.isLock) {
            if(AdsManagerWrapper.INSTANCE) {
                AdsManagerWrapper.INSTANCE.ShowRewards((onAdLoded)=>{}, (onAdFailedToLoad) => {} , (iRewards) => {
                    ScrollListManager.LoadGame(index);
                    ListColoringManager.instance.UnlockColoring(index);
                });
            }
        } else {
            if(AdsManagerWrapper.INSTANCE) {
                 AdsManagerWrapper.INSTANCE.ShowInterstitial( (onAdLoded) => {}, (onAdFailedToLoad) => {});
            }
            ScrollListManager.LoadGame(index);
        }
    }

    private void loadImage(ItemColoring item, ItemTemplateColoring itemTemplateColoring)
    {
        if(item.spriteImage) {
            itemTemplateColoring.setImageColoring(item.spriteImage);
            return;
        }
        if (item.image.StartsWith("http"))
        {
            StartCoroutine(Utils.LoadSpriteCoroutine(item.image, (sprite) =>
            {
                itemTemplateColoring.setImageColoring(sprite);
                item.spriteImage = sprite;
            }));
        }
        else
        {
            var sprite = Utils.LoadImageFromResources(item.image);
            itemTemplateColoring.setImageColoring(sprite);
            item.spriteImage = sprite;
        }
    }

    private void RemoveAllChildObjects(GameObject parent)
    {
        while (parent.transform.childCount > 0)
        {
            // Hancurkan setiap child object
            Transform child = parent.transform.GetChild(0);
            child.SetParent(null);  // Agar tidak mengganggu iterator
            Destroy(child.gameObject);
        }
    }
}
