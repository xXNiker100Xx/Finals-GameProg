using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeStatus : MonoBehaviour
{
    
    public float Duration = 5;
    StatusEffectManager _StatusEffectManager;
    PlayerAttributes _PlayerAttributes;
    public _StatusEffectsEnum StatusEffect = _StatusEffectsEnum.None;
    void Start()
    {
        _StatusEffectManager = GetComponentInParent<StatusEffectManager>();
        _PlayerAttributes = GetComponentInParent<PlayerAttributes>();

        if(StatusEffect == _StatusEffectsEnum.BURN)
        {
            InvokeRepeating("burn", 1f, 1f);
        }
    }

    
    void Update()
    {
        _StatusEffectManager.ApplyStatusEffect(StatusEffect);
        Destroy(gameObject, Duration);
    }

    private void OnDestroy()
    {
        _StatusEffectManager.RemoveStatusEffect(StatusEffect);
    }

    private void burn()
    {
        _PlayerAttributes.playerDedutctHealth(10);
    }
}
