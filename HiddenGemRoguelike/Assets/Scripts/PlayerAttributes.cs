
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    [Header("[Player Health]")]
    public float Health = 0f;
    public float MaxHealth = 100f;
    private float previousHealth;
    [Header("[Player Stats]")]
    public float atkDmg = 0f;
    float defaultAtkDmg;
    [Range(0f, 0.5f)]
    public float atkSpeed;
    public float speed = 100f;
    public float dashCount = 1;
    [Header("[Crit Hit]")]
    [Range(0.1f, 1f)]
    public float critChance = 0.2f;
    public float critMultiplier = 2.0f;
    [Header("[Player Experince]")]
    public float Exp = 0f;
    public float MaxExp = 100f;

    [Header("[Player Damage Color]")]
    public Color _color;
    [Header("[Components]")]
    public ChacterControlManagement _controls;
    public GameObject _hurtScreenFX;
    public Slider HealthSlider;
    public Movement _movement;
    public onHitScript _onHitScript;
    public SpriteRenderer _spriteRenderer;

    void Start()
    {
        
        Health = MaxHealth;
        Exp = MaxExp;
        previousHealth = Health;
        defaultAtkDmg = atkDmg;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;
        _movement = GetComponent<Movement>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _controls = GetComponent<ChacterControlManagement>();
        
        
}

    void Update()
    {
        _controls.atkCoolDown = atkSpeed;
        HealthSlider.value = Health;
        _movement.speed = speed;
        if (Health < previousHealth)
        {
            DamagedFX();
        }
        previousHealth = Health;
        
    }

    public void attack()
    {
        float _onCrit = CalculateDamage();
        _onHitScript.damage = _onCrit;
        
        if(_onCrit == atkDmg)
        {
            _onHitScript.isCriticalHit(false);
        }
        else if (_onCrit > atkDmg)
        {
            _onHitScript.isCriticalHit(true);
        }
    }

    public void playerAddHealth(float health)
    {
        Health += health; 
    }

    public void playerDedutctHealth(float health)
    {
        Health -= health;
    }

    public void playerAddMaxHealth(float maxHealth)
    {
        MaxHealth += maxHealth;
    }

    public void playerAddExperince(float Experince)
    {
        MaxHealth += Experince;
    }

    private void DamagedFX()
    {
        StartCoroutine(onDamage());
    }
    
    IEnumerator onDamage()
    {
        _spriteRenderer.color = _color;
        _hurtScreenFX.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
        _hurtScreenFX.SetActive(false);
    }

    private float CalculateDamage()
    {
        float finalDamage = atkDmg;

        if (IsCriticalHit())
        {
            finalDamage *= critMultiplier;
        }

        return finalDamage;
    }

    public bool IsCriticalHit()
    {
        return UnityEngine.Random.Range(0f, 1f) < critChance;
    }

    public void ApplyDamageMultiplier(float multiplier)
    {
        atkDmg *= multiplier;
        UpdateDamage();
    }

    public void ResetDamageMultiplier()
    {
        atkDmg = defaultAtkDmg;
        UpdateDamage();
    }

    private void UpdateDamage()
    {
        _onHitScript.damage = CalculateDamage();
    }
}
