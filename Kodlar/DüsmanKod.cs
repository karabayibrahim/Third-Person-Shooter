using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class DüsmanKod : MonoBehaviour
{
    public int Can =100;
    public float Speed = 3f;
    //public Collider MainCollider;
    public Collider MainCollider;
    [SerializeField]
    public float StopDistance;
    [SerializeField]
    public float FarketmeDistanse;
    [SerializeField]
     public float RetreatDistance;
    public Collider[] AllColliders;
    public Collider KafaCol;
    public Collider GövdeCol;
    public Rigidbody[] AllRig;
    public Animator Anim;
    public bool AtesAc = false;
    AudioSource SesCal;
    public AudioClip FireSound;
    public bool RagKontrol = false;
    public Transform DüsNamlu;
    public Transform DüsNamluCalisan;
    Transform Player;
    int MermiSayisi = 30;
    float ReloadBekleme=4.2f;
    float Firerate = 0.1f;
    float KalanZaman = 0f;
    public GameObject ShotParticle;
    Rigidbody Rb;
    public GameObject KanPar;
    NavMeshAgent agent;
    public AudioClip WalkSound;
    public bool FarkEtme = false;
    
    
    
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        SesCal = GetComponent<AudioSource>();
        MainCollider = GetComponent<Collider>();
        Rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        AllRig = GetComponentsInChildren<Rigidbody>(true);
        RagdollAktif(false);
        Anim = GetComponent<Animator>();
        //Rb.isKinematic = false;
    }
    public void RagdollAktif(bool Kontrol)
    {
        
        foreach (var col in AllColliders)
        {
            //Rb.isKinematic = false;
            col.enabled = Kontrol;
            RagKontrol = Kontrol;
            MainCollider.enabled = !Kontrol;
            KafaCol.enabled = !Kontrol;
            GövdeCol.enabled = !Kontrol;
            AtesAc = false;
            
            GetComponent<Rigidbody>().useGravity = !Kontrol;
            GetComponent<Animator>().enabled = !Kontrol;
        }
        foreach (var rig in AllRig)
        {
            Rb.isKinematic = Kontrol;
            rig.isKinematic = !Kontrol;
            


        }
    }
    void Walk()
    {
        SesCal.PlayOneShot(WalkSound);
    }
    

    // Update is called once per frame
    void Update()
    {
        //Rb.isKinematic = false;
        if (Can<=0)
        {
            
            RagdollAktif(true);
            //Anim.SetTrigger("Dead");
            Destroy(gameObject, 5f);
            
            

        }
        
        Reload();
        
        
        Shoot();
    }
    
    private void FixedUpdate()
    {
        DüsmanTakip();
    }
    public void DüsmanTakip()
    {
        if (Vector3.Distance(transform.position,Player.transform.position)>StopDistance&&Vector3.Distance(transform.position,Player.transform.position)<FarketmeDistanse&&!RagKontrol&&FarkEtme)
        {
            AtesAc = true;
            Anim.SetBool("Kosma", true);
            Anim.SetBool("Geri", false);
            //transform.position = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
            agent.SetDestination(Player.transform.position);
            //Rb.AddForce(transform.forward * 2f*);
            transform.LookAt(Player.position);
            //Vector3 direction = (Player.position - transform.position).normalized;
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.5f);//Yumusak dönerek bakış.


        }
        else if (Vector3.Distance(transform.position,Player.position)<StopDistance&&Vector3.Distance(transform.position,Player.position)>RetreatDistance && !RagKontrol && FarkEtme)
        {
            AtesAc = true;
            Anim.SetBool("Kosma", false);
            Anim.SetBool("Geri", false);
            //transform.position = this.transform.position;
            agent.SetDestination(transform.position);
            //transform.LookAt(Player.position);
            Vector3 direction = (Player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);//Yumusak dönerek bakış.

        }
        else if(Vector3.Distance(transform.position,Player.position)<RetreatDistance && !RagKontrol && FarkEtme)
        {
            AtesAc = true;
            Anim.SetBool("Geri", true);
            Anim.SetBool("Kosma", false);
            transform.position = Vector3.MoveTowards(transform.position, Player.position, -Speed * Time.deltaTime);
            //agent.SetDestination(-Player.transform.position);
            //transform.LookAt(Player.position);
            Vector3 direction = (Player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.5f);//Yumusak dönerek bakış.

        }
        else
        {
            AtesAc = false;
            Anim.SetBool("Geri", false);
            Anim.SetBool("Kosma", false);
        }
        
        
    }
    public void Shoot()
    {
        RaycastHit AteseBaslaNok;
        if (Physics.Raycast(DüsNamlu.position,DüsNamlu.transform.forward,out AteseBaslaNok,10000.0f))
        {
            if (AteseBaslaNok.transform.gameObject.CompareTag("Player"))
            {
                StopDistance = 15f;
                agent.stoppingDistance = 15;
                if (AtesAc == true && MermiSayisi > 0 && FindObjectOfType<JoyPlayerMovement>().Canim > 0)
                {

                    Anim.SetTrigger("Shoot");

                    if (Time.time >= KalanZaman)
                    {
                        
                        GameObject YeniShotPar = Instantiate(ShotParticle, DüsNamlu.position, DüsNamlu.transform.rotation);
                        Destroy(YeniShotPar, 0.2f);
                        MermiSayisi--;
                        KalanZaman = Firerate + Time.time;
                        RaycastHit temas;
                        SesCal.PlayOneShot(FireSound);
                        float SapmaPayix = Random.Range(-0.5f, 0.5f);
                        float SapmaPayiy = Random.Range(-0.5f, 0.5f);
                        DüsNamluCalisan.position = new Vector3(DüsNamlu.position.x + SapmaPayix, DüsNamlu.position.y + SapmaPayiy, DüsNamlu.position.z);

                        if (Physics.Raycast(DüsNamluCalisan.position, DüsNamluCalisan.transform.forward, out temas, 1000f))
                        {



                            if (temas.transform.gameObject.CompareTag("Player"))
                            {
                                FindObjectOfType<JoyPlayerMovement>().Cankontrol();
                                GameObject YeniKanPar = Instantiate(KanPar, temas.point, Quaternion.LookRotation(temas.normal));
                                FindObjectOfType<JoyPlayerMovement>().Canim -= 20;
                                FindObjectOfType<JoyPlayerMovement>().Anim.SetTrigger("Hit");
                                Destroy(YeniKanPar, 1f);
                            }

                        }




                    }





                }
            }
            //else
            //{
            //    StopDistance = 5f;
            //    agent.stoppingDistance = 5f;
            //}


        }
        
        
        
    }
    public void Reload()
    {
        if (MermiSayisi<=0)
        {
            Anim.SetTrigger("Reload");
            ReloadBekleme -= Time.deltaTime;
            if (ReloadBekleme <= 0)
            {
                
                MermiSayisi = 30;
                ReloadBekleme = 4.2f;
            }
        }
        
    }
    

}
