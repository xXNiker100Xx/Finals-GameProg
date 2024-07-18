using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessItemDrop : MonoBehaviour
{
    public ItemDrop _itemDrop;
    void Start()
    {
        _itemDrop = GetComponent<ItemDrop>();
    }

    private void Awake()
    {
        _itemDrop.DropItem();
    }
}
