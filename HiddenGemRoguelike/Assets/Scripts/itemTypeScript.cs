using UnityEngine;
using TMPro;
using System.Collections;

public enum rarityOptions
{
    Common,
    Rare,
    Legendary
}

[System.Serializable]
public class RarityType
{
    public GameObject vfx;
    public rarityOptions rarityOptions;
}
public class itemTypeScript : MonoBehaviour
{

    public PowerUpEffect powerUpEffect;
    public RarityType[] rarityTypes;
    SpriteRenderer itemLogo;
    SpriteDirectionControll logoTransform;
   
    
    [Header("Compnents")]    
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI description;
    public GameObject item;
    [Header("Special VFX")]
    public GameObject VFX;
    private void Start()
    {
        itemLogo = GetComponentInChildren<SpriteRenderer>();
        itemLogo.sprite = powerUpEffect.sprite;
        logoTransform = GetComponentInChildren<SpriteDirectionControll>();
       

        itemTitle.text = powerUpEffect.name;
        itemName.text = powerUpEffect.name + "\n" + "[" + powerUpEffect.itemRarity.ToString() + "]";
        description.text = powerUpEffect.Description;

        Vector3 offset = new Vector3(0, 0, 0.03f) ;
        InstantiateVFX(offset);
    }

    private rarityOptions ConvertToRarityOption(itemRarity rarity)
    {
        switch (rarity)
        {
            case itemRarity.Common:
                return rarityOptions.Common;
            case itemRarity.Rare:
                return rarityOptions.Rare;
            case itemRarity.Legendary:
                return rarityOptions.Legendary;
            default:
                return rarityOptions.Common;
        }
    }

    private void InstantiateVFX(Vector3 offset)
    {
        foreach (var rarityType in rarityTypes)
        {
            rarityOptions itemRarityOption = ConvertToRarityOption(powerUpEffect.itemRarity);
            if (rarityType.rarityOptions == itemRarityOption)
            {
                if (itemTitle != null)
                {
                    itemTitle.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, powerUpEffect.GetRarityColor());
                    itemName.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, powerUpEffect.GetRarityColor());
                }
                Transform logoPos = logoTransform.transform;
                GameObject instantiatedVFX = Instantiate(rarityType.vfx, logoPos.position + offset, Quaternion.identity);
                instantiatedVFX.transform.parent = logoPos;
                break;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(gameObject);
                powerUpEffect.Apply(other.gameObject);
                item.gameObject.SetActive(false);
            }
        }
    }
}
