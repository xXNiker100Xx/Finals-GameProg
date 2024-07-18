using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/New Power up Item")]
public class PowerUpItemScript : PowerUpEffect
{
    [Header("Modifier")]
    public float maxhealthAmount;
    public float healthAmount; 
    public float damageAmount;
    public float speedAmount;
    public float attackSpeed;
    [Range(0f, 1f)]
    public float critChanceAmount;
    public float critDamageAmount;

    
    public override void Apply(GameObject target)
    {
        PlayerAttributes player = target.GetComponent<PlayerAttributes>();
        player.MaxExp += maxhealthAmount;
        player.Health += healthAmount;
        player.atkDmg += damageAmount;
        player.atkSpeed += attackSpeed;
        player.speed += speedAmount;
        player.critChance += critChanceAmount;
        player.critMultiplier += critDamageAmount;
    }
}
