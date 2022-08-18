using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image image;
    public Text itemName;
    public Text nextLevel;
    public Text description;

    private Transform player;
    
    private string level = "level: ";

    private Item item;
    private GameObject itemObj;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void SetButtonImage(GameObject obj)
    {
        itemObj = obj;
        item = obj.GetComponent<Item>();
        image.sprite = item.spriteImg;
        this.itemName.text = item.itemName;
        if (item.level + 1 == 1)
        {
            this.nextLevel.text = "<color=#ffd400>New</color>";
        }
        else if (item.IsMaxNextLevel()) 
        {
            this.nextLevel.text = "<color=#ffd400>Max</color>";
        }
        else
        {
            this.nextLevel.text = level + (item.level + 1);
        }

        this.description.text = item.GetDescription();
    }

    public void PickUpItem()    // 버튼에 사용된 함수
    {
        if (item.itemType != Item.ItemType.Instant) 
            itemObj.transform.parent = player;
        if(item.level==0)
            item.EnableItem();
        else
            item.LevelUpItem();
        Managers.UI.ItemSelectPanel.SetActive(false);
    }
    
}
