using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public PowerUpEffect[] commonItems;
    public PowerUpEffect[] rareItems;
    public PowerUpEffect[] legendaryItems;
    public GameObject newItemPrefab;
    public void DropItem()
    {
        float dropChance = Random.value;

        if (dropChance <= 0.6f)
        {
            DropRandomItem(commonItems);
        }
        else if (dropChance <= 0.9f) 
        {
            DropRandomItem(rareItems);
        }
        else
        {
            DropRandomItem(legendaryItems);
        }
    }

    void DropRandomItem(PowerUpEffect[] items)
    {
        if (items.Length > 0)
        {
            int index = Random.Range(0, items.Length);
            PowerUpEffect itemToDrop = items[index];
            GameObject newItem = Instantiate(newItemPrefab, transform.position, Quaternion.identity);
            newItem.GetComponent<itemTypeScript>().powerUpEffect = itemToDrop;
        }
        else
        {
            Debug.LogWarning("No items in the array.");
        }
    }
}
