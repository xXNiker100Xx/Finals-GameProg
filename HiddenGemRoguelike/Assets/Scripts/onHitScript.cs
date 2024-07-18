using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class onHitScript : MonoBehaviour
{
    public PlayerAttributes _playerAttributes;
    public float damage;
    public GameObject hitObject;
    bool criticalHit = false;
    public AudioSource hitAudioSource;

    public void isCriticalHit(bool isCrit)
    {
        criticalHit = isCrit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject Enemy = other.gameObject;
            EnemyAttributes enemyAttributes = Enemy.GetComponent<EnemyAttributes>();
            enemyAttributes.EnemyDedutctHealth(damage);

            Vector3 randomPosition = other.transform.position + new Vector3(Random.Range(-1f, 1f), 1f, 0);
            Quaternion randomRotation = Quaternion.Euler(0, 0, (Random.Range(20f, -20f)));
            GameObject hitText = Instantiate(hitObject, randomPosition, randomRotation);
            
            TextMeshProUGUI onHitText = hitText.GetComponentInChildren<TextMeshProUGUI>();

            if (criticalHit)
            {
                onHitText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.2f);
                onHitText.fontSharedMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.red);
                onHitText.text = damage.ToString() + "!!!";
            }
            else
            {
                onHitText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0f);
                onHitText.text = damage.ToString();
            }
            hitText.transform.parent = Enemy.transform;
            Destroy(hitText, 1f);
            hitAudioSource.Play();
        }
    }
}
