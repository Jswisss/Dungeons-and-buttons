using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PuaseMenu : MonoBehaviour
{
    public GameObject PanelEngame;
    public GameObject PanelPausemenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PuaseGame()
    {
        Time.timeScale = 0;
        PanelEngame.gameObject.SetActive(false);
        PanelPausemenu.gameObject.SetActive(true);
    }

    public void resumegame()
    {
        Time.timeScale = 1;
        PanelEngame.gameObject.SetActive(true);
        PanelPausemenu.gameObject.SetActive(false);
    }

    public void InvectoryPanelScreen()
    {

    }

    public void Retreat()
    {
        SceneManager.LoadScene(0);
    }
}
