using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public PortalMainMenu _portal;
    public GameObject hideUi;

    Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        
    }


    public void play()
    {
        _portal.portalGate(true);
        hideUi.SetActive(false);
        _anim.Play("MainMenu");
    }

    public void loadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
