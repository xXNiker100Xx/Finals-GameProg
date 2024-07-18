using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChacterControlManagement : MonoBehaviour
{
    public PlayerAttributes PlayerAttributes;
    public PlayerHitBox playerHitBox;


    public float atkCoolDown;


    public bool canAttack = true;
    public bool isActive = true;

    private void Start()
    {
        atkCoolDown = PlayerAttributes.atkSpeed;
    }

    void Update()
    {
        if (isActive && canAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayerAttributes.attack();
                playerHitBox.hitTrigger(true);
                StartCoroutine(startCoolDown());

            }
        }
    }

    IEnumerator startCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(atkCoolDown);  
        canAttack = true;
    }

    public void activateHitBox(bool _Active)
    {
        isActive = _Active;
    }
}
