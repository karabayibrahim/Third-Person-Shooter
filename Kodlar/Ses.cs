using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ses : MonoBehaviour
{
    public AudioClip SenKimsin;
    AudioSource SesCal;
    // Start is called before the first frame update
    void Start()
    {
        SesCal = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DüsKafa"))
        {
            SesCal.PlayOneShot(SenKimsin);
        }
    }
    void Update()
    {
        
    }
}
