using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHit : MonoBehaviour
{
    [Header("[Status Effects VFX]")]
    public GameObject freezeVFX;
    public GameObject burnVFX;
    
    private float damage;

    private EnemyAttributes _enemyAttributes;

    void Start()
    {
        _enemyAttributes = GetComponentInParent<EnemyAttributes>();
        damage = _enemyAttributes.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_enemyAttributes.EnemyStatusType == EnemyStatusType.FREEZE)
            {
                Transform player = other.transform;
                GameObject FreezeFX = Instantiate(freezeVFX, player.position, Quaternion.identity);
                FreezeFX.transform.parent = player;
                Destroy(FreezeFX, 5f);

                PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
                playerAttributes.playerDedutctHealth(damage);

            }

            if (_enemyAttributes.EnemyStatusType == EnemyStatusType.BURN)
            {
                Transform player = other.transform;
                GameObject BurnFX = Instantiate(burnVFX, player.position, Quaternion.identity);
                BurnFX.transform.parent = player;
                Destroy(BurnFX, 5f);

                PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
                playerAttributes.playerDedutctHealth(damage);
            }

            if (_enemyAttributes.EnemyStatusType == EnemyStatusType.NEUTRAL)
            {
                Transform player = other.transform;
                PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
                playerAttributes.playerDedutctHealth(damage);
            }
        }
        
    }


}


