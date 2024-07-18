using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class playerAbilitiesScript : MonoBehaviour
{
    [SerializeField]PlayerHitBox _playerHitBox;
    [Header("1st Ability")]
    public KeyCode Ability1;
    public float Ability1CD = 5;
    float onCD = 0f;
    public bool _1stIsActive = true;
    public float duration = 0.5f;
    public GameObject[] VFXs;
    public GameObject VFXsdoneCharge;
    private ParticleSystem[] particleSystems;
    public GameObject VFX;
    public Image skillImage;


    private float chargeTime = 0;
    private float maxChargeTime = 2f;
    private float defaultSpeed;


    [SerializeField] private float currentMultiplier;
    [Header("2st Ability")]
    public KeyCode Ability2;
    public float Ability2CD;
    public bool _2stIsActive = true;
    float dmgMultiplier = 2;
    private StatusEffectManager _pauseMotion;
    private PlayerAttributes _playerAttributes;
    public onHitScript _onHitScript;
   

    private void Start()
    {
        
        _playerHitBox = GetComponentInChildren<PlayerHitBox>();
        _pauseMotion = GetComponent<StatusEffectManager>();
        _playerAttributes = GetComponent<PlayerAttributes>();
        _onHitScript = GetComponentInChildren<onHitScript>();


        defaultSpeed = _playerAttributes.speed;

        particleSystems = new ParticleSystem[VFXs.Length];
        for (int i = 0; i < VFXs.Length; i++)
        {
            particleSystems[i] = VFXs[i].GetComponent<ParticleSystem>();
        }

    }

    private void Update()
    {
        if (_1stIsActive == false)
        {
            onCD -= Time.deltaTime;
            skillImage.fillAmount = onCD / Ability1CD;
        }
        else
        {
            onCD = Ability1CD;
            skillImage.fillAmount = 1;
        }

        _1stAbility(_1stIsActive, Ability1);
    }   



    private void _1stAbility(bool _Active, KeyCode _key)
    {
        if(_Active)
        {
            if (Input.GetKey(_key))
            {
                _playerAttributes.speed = 2;
                PlayVFX(true);
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Clamp(chargeTime, 0, maxChargeTime);
                currentMultiplier = Mathf.Lerp(1, dmgMultiplier, chargeTime / maxChargeTime);
                currentMultiplier = Mathf.Round(currentMultiplier * 100f) / 100f;
                UpdateVFXState();
            }

            if (Input.GetKeyUp(_key))
            {
                PlayVFX(false);
                StartCoroutine(_1stAbilityCast());
                _playerAttributes.speed = defaultSpeed;
            }
        }
    }

    private void UpdateVFXState()
    {
        float normalizedChargeTime = chargeTime / maxChargeTime;

        for (int i = 0; i < VFXs.Length; i++)
        {
            if (currentMultiplier != 2)
            {
                if (!particleSystems[i].isPlaying)
                {
                    particleSystems[i].Play();
                }
            }
            else
            {
                if (particleSystems[i].isPlaying)
                {
                    particleSystems[i].Stop();
                    VFXsdoneCharge.SetActive(true);
                }
            }
        }
    }
    IEnumerator _1stAbilityCast()
    {
        VFXsdoneCharge.SetActive(false);
        _pauseMotion.ApplyStatusEffect(_StatusEffectsEnum.FREEZE);
        _playerHitBox.onTrigger = true;
        VFX.SetActive(true);
        
        if (_playerHitBox.onTrigger == true)
        {
            _playerHitBox.onHitBox(false);
            _playerAttributes.ApplyDamageMultiplier(currentMultiplier);
            _playerAttributes.attack();
        }
        Transform _HitBox = _playerHitBox.HitBox;
        Rigidbody rb = _HitBox.GetComponentInParent<Rigidbody>();

        Vector3 startPosition = rb.position;
        Vector3 targetPosition = startPosition + _HitBox.forward * 5f;
        _1stIsActive = false;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            rb.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime * 2;
            yield return null;
        }
        rb.position = targetPosition;
        _playerAttributes.ResetDamageMultiplier();
        _pauseMotion.RemoveStatusEffect(_StatusEffectsEnum.FREEZE);
        _playerHitBox.onTrigger = false;
        _playerHitBox.onHitBox(true);
        VFX.SetActive(false);
        chargeTime = 0;
        yield return new WaitForSeconds(Ability1CD);
        _1stIsActive =  true;

    }

    private void PlayVFX(bool isActive)
    {
        foreach (GameObject vfx in VFXs)
        {
            vfx.SetActive(isActive);
        }
    }

}
