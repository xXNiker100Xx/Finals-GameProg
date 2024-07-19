using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemRarity
{
    Common,
    Rare,
    Legendary
}

public abstract class PowerUpEffect : ScriptableObject
{
    [Header("Item Info")]
    public Sprite sprite;
    public new string name;
    [TextArea(15, 25)]
    public string Description;
    public itemRarity itemRarity;
    public Color commonColor = new Color(0f, 0.929f, 1f);    
    public Color rareColor = new Color(0.698f, 0.373f, 1f);  
    public Color legendaryColor = new Color(1f, 0.843f, 0.427f);

    public Color GetRarityColor()
    {
        switch (itemRarity)
        {
            case itemRarity.Common:
                return commonColor;
            case itemRarity.Rare:
                return rareColor;
            case itemRarity.Legendary:
                return legendaryColor;
            default:
                Debug.LogWarning($"Unknown item rarity: {itemRarity}. Returning default color.");
                return Color.white;
        }
    }
    public abstract void Apply(GameObject target);
}
