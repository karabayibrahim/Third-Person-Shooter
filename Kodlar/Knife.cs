
using UnityEngine;

public class Knife : MonoBehaviour
{
    public GameObject KanPar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Düsman") || other.gameObject.CompareTag("DüsKafa"))
        {
            FindObjectOfType<JoyPlayerMovement>().FireSound.PlayOneShot(FindObjectOfType<JoyPlayerMovement>().KnifeHit);
            other.gameObject.GetComponentInParent<DüsmanKod>().Can -= 100;
            GameObject YeniKanPar = Instantiate(KanPar, other.transform.position, other.transform.rotation);
            Destroy(YeniKanPar, 1f);
        }
    }
}
