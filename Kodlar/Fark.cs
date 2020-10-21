using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fark : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<DüsmanKod>().FarketmeDistanse = 50f;
            GetComponent<DüsmanKod>().RetreatDistance = 5;
            GetComponent<DüsmanKod>().StopDistance = 15f;
            GetComponent<DüsmanKod>().FarkEtme = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<DüsmanKod>().FarketmeDistanse = 0f;
            GetComponent<DüsmanKod>().FarkEtme = false;
            GetComponent<DüsmanKod>().RetreatDistance = 0;
            GetComponent<DüsmanKod>().StopDistance = 0;
        }
    }

}
