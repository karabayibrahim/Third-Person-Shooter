using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OyunKontrol : MonoBehaviour
{
    public GameObject StBut;
    void Start()
    {
        
    }
    public void GösterRek()
    {
        ReklamInterstitial.showAd = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        
    }
    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
