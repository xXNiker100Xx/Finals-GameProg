using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public enum EnemyStatusType
{
    NEUTRAL,
    BURN,
    FREEZE,
    POISON
}

public class EnemyAttributes : MonoBehaviour
{
    [Header("[Enemy Health]")]
    public float Health = 0f;
    private float previousHealth;
    public float MaxHealth = 100f;

    [Header("[Enemy Attributes]")]
    public float Damage = 0f;
    public float coolDown1 = 0f;
    public float speed = 2f;
    [Header("[Enemy Status Type]")]
    public EnemyStatusType EnemyStatusType;
    
    [Header("[Components]")]
    public GameObject HealthBar;
    public Slider HealthSlider;
    public SpriteRenderer _spriteRenderer;
    public NavMeshAgent _agent;
    ItemDrop _ItemDrop;

    [Header("[Damage Color]")]
    public Color _color;
    [Header("[Original Color]")]
    public Color _Origcolor;

    void Start()
    {
        Health = MaxHealth;
        previousHealth = Health;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;
        _agent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _ItemDrop = GetComponent<ItemDrop>();
        
    }

    void Update()
    {
        HealthSlider.value = Health;
        _agent.speed = speed;


        if(Health <= 0f)
        {
            _ItemDrop.DropItem();
            Destroy(gameObject);
            
        }

        if (Health < previousHealth)
        {
            HealthBar.SetActive(true);
            DamagedFX();
        }
        previousHealth = Health;
    }

    public void EnemyAddHealth(float health)
    {
        Health += health;
    }

    public void EnemyDedutctHealth(float health)
    {
        Health -= health;
    }

    private void DamagedFX()
    {
        StartCoroutine(onDamage());
    }

    IEnumerator onDamage()
    {

        _spriteRenderer.color = _color;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = _Origcolor;
    }
}
