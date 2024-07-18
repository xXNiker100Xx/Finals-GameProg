using System;
using System.Xml.Serialization;
using UnityEngine;

[Flags]
public enum _StatusEffectsEnum
{
    None = 0,
    NEUTRAL = None,
    BURN = 1 << 0, 
    FREEZE = 1 << 1,
    POISON = 1 << 2
}

public class StatusEffectManager : MonoBehaviour
{
    [Header("[StatusEffect]")]
    public _StatusEffectsEnum _StatusEffects;
    
    //Componentes
    private PlayerAttributes _PlayerAttributes;
    private Movement _movement;
    private CharacterSpriteRotation _CharacterSpriteRotation;
    private ChacterControlManagement _CharacterControlManagement;

    void Start()
    {
        _PlayerAttributes = GetComponent<PlayerAttributes>();
        _movement = GetComponent<Movement>();
        _CharacterSpriteRotation = GetComponentInChildren<CharacterSpriteRotation>();
        _CharacterControlManagement = GetComponent<ChacterControlManagement>();
    }

    void Update()
    {
        HandleStatusEffect();
    }

    void HandleStatusEffect()
    {
        if ((_StatusEffects & _StatusEffectsEnum.FREEZE) != 0)
        {
            ApplyFreezeEffect(false);
        }

        if (_StatusEffects == _StatusEffectsEnum.NEUTRAL)
        {
            ApplyFreezeEffect(true);
            ApplyBurnEffect(false);
        }

        if ((_StatusEffects & _StatusEffectsEnum.BURN) != 0)
        {
            
        }

        if ((_StatusEffects & _StatusEffectsEnum.POISON) != 0)
        {
            Debug.Log("House");
        }
    }

    public void ApplyFreezeEffect(bool isActive)
    {
        _movement.disableMovement(isActive);
        _CharacterSpriteRotation.onRotation(isActive);
        _CharacterControlManagement.activateHitBox(isActive);
    }

    private void ApplyBurnEffect(bool isActive)
    {
        
    }
    private void burn()
    {
        _PlayerAttributes.playerDedutctHealth(5);
    }


    public void ApplyStatusEffect(_StatusEffectsEnum status)
    {
        _StatusEffects |= status;
        HandleStatusEffect();
    }

    public void RemoveStatusEffect(_StatusEffectsEnum status)
    {
        _StatusEffects &= ~status;
        HandleStatusEffect();
    }
}