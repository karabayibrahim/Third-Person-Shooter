using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Düsman;
    public Transform[] DogmaNoktaları;
    public GameObject[] Düsmanlar;
    //public int ÖlmeSayac = 0;
    public int Sayac = 0;
   public  bool Deneme = false;

    // Start is called before the first frame update
    void Dogma()
    {
        if (Düsmanlar[0] == null)
        {
            Deneme = false;
            Sayac = 0;
        }
        for (int i = 0; i < 5; i++)
        {
            Düsmanlar[i] = GameObject.FindGameObjectWithTag("Enemy");
        }
        

        if (Sayac<5&&Deneme==false)
        {
            
            int Sec = Random.Range(1, 4);
            GameObject YeniDüsman = Instantiate(Düsman, DogmaNoktaları[Sec].position, Quaternion.identity);
            Sayac++;

            if (Sayac>5)
            {
                Deneme = true;
                
            }






        }
        
        




    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dogma();
    }
}
