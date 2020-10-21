using DitzeGames.MobileJoystick;
using System.Linq;
using TMPro;
using UnityEngine;

public class JoyPlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Değerler") ]
    public Joystick Joy;
    
    
    //public Button Button;
    public float rotSpeed;
    public float rotX;
    public float rotY;
    public float rotZ;
    public Animator Anim;
    public float Hiz;
    float Vertical;
    float Horizontal;
    public Button Fire;
    public Button Aim;
    public Button ReloadButton;
    public RectTransform Hedef_Poziyon;
    public Transform Hedef_İcon;
    public GameObject Kamera;
    public Transform Hedef;
    public GameObject Kursun;
    public Transform Namlu;
    public GameObject FireParticle;
    public GameObject KanParticle;
    public GameObject MermiEtki;
    public AudioSource FireSound;
    public AudioClip FireSes;
    public AudioClip ReloadSes;
    public AudioClip Nobullet;
    public TouchField TouchAlani;
    float Ver;
    float Hor;
    [SerializeField]
    int MermiSayisi;
   public bool ReloadKontrol = false;
    bool ReloadZaman = false;
    float ReloadBekleTime = 3.2f;
    public GameObject MainKamera;
    public GameObject YakınKamera;
    float AtesAralık = 0.1f;
    float KalanZaman = 0f;
   public int AimSayac = 0;
    public float Range = 1000.0f;
    public float Canim = 1000f;
    Rigidbody Rb;
    public Button KnifeBut;
    public GameObject Silah;
    public GameObject Bıc;
    bool BeklemeZamanKnife = false;
    float KnifeBekleme = 0.3f;
    public AudioClip KnifeHit;
    public AudioClip KnifeCekme;
    public AudioClip CekmeSes;
    public AudioClip WalkSound;
    bool YakinlasmaAktif = false;
    public GameObject CrosYakin;
    public GameObject Cross;
    public LayerMask layermask;
    public bool ShootAktif=false;
    public TextMeshProUGUI BulletSayı;
    public TextMeshProUGUI Health;
    public GameObject GameOver;
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        //Button = FindObjectOfType<Button>();
        Anim = GetComponent<Animator>();
        
        
        
    }

    // Update is called once per frame
    //void Update()
    //{




    //}
    private void Update()
    {

        //HedefTespit();
        //Debug.Log("cANİM" + Canim);
        BulletSayı.text = "Bullet:" + MermiSayisi.ToString();
        Health.text = "Health:" + Canim.ToString();
        Reload();
        Cankontrol();
        //Debug.Log(ReloadBekleTime);
        Yakınlasma();
        //Debug.Log(AimSayac);
        
    }
    void FixedUpdate()
    {
        Movement();
        Animasyon();
        Shoot();


    }
  public  void Cankontrol()
    {
        if (Canim <= 0)
        {
            Anim.SetTrigger("Dead");
            FindObjectOfType<JoyPlayerMovement>().enabled = false;
            GameOver.SetActive(true);
            Canim = 0;
            //Debug.Log("öLDÜM");
        }
    }

    void Movement()
    {

        //var input = new Vector3(transform.position.x + Joy.AxisNormalized.x * Time.fixedDeltaTime * Hiz ,transform.position.y, transform.position.z + Joy.AxisNormalized.y * Time.fixedDeltaTime * Hiz );
        //transform.position = input;
        var input = new Vector3(Joy.AxisNormalized.x, 0, Joy.AxisNormalized.y);
        if (FindObjectOfType<CameraFollow>()!=null)
        {
            var vel = Quaternion.AngleAxis(FindObjectOfType<CameraFollow>().CameraAngel,Vector3.up) * input * Hiz;
            
            Rb.velocity = new Vector3(vel.x, Rb.velocity.y, vel.z);
        }
        else
        {
            var vel = Quaternion.AngleAxis(FindObjectOfType<CameraFollow>().CameraAngel, Vector3.up) *input * Hiz;
            
            Rb.velocity = new Vector3(vel.x, Rb.velocity.y, vel.z);
        }
        
        

        //Vector3 Vec=new Vector3(transform.position.x + Joy.InputVector.x * Time.fixedDeltaTime * 3f , transform.position.y,

        //    transform.position.z + Joy.AxisNormalized.y* Time.fixedDeltaTime * 3f );
        //transform.LookAt(Vec);
        if (transform.position.y<0)
        {
            Anim.SetTrigger("Fall");
        }
        if (FindObjectOfType<CameraFollow>() != null)
        {
            transform.rotation = Quaternion.AngleAxis(FindObjectOfType<CameraFollow>().CameraAngel + Vector3.SignedAngle(Vector3.forward,  Vector3.forward * 0.001f, Vector3.up), Vector3.up);
            
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(FindObjectOfType<CameraFollowYakin>().CameraAngel + Vector3.SignedAngle(Vector3.forward,  Vector3.forward * 0.001f, Vector3.up), Vector3.up);
            //input.normalized + çıkartıldı.
        }




    }
    //void WalkDüsGeri()
    //{
    //    FireSound.PlayOneShot(WalkSound);
    //}
    void Walk()
    {
        FireSound.PlayOneShot(WalkSound);
    }
    void Run()
    {
        FireSound.PlayOneShot(WalkSound);
    }
    public  void Animasyon()
    {
        //Anim.SetBool("İdle", false);
        Ver = Joy.AxisNormalized.x;
        Hor = Joy.AxisNormalized.y;

        Anim.SetFloat("vertical", Ver);
        Anim.SetFloat("horizontal", Hor);
        if (Ver == 0 && Hor == 0)
        {
            //Anim.SetBool("İdle", true);
            Anim.SetBool("WalkBool", false);

        }
        if (Hor>0.9)
        {
            Hiz = 5f;
        }
        else
        {
            Hiz = 2f;
        }
        
        //if (Ver != 0 || Hor != 0)
        //{
        //    Anim.SetBool("WalkBool", true);
        //}


    }
    public void HedefTespit()
    {
        RaycastHit temas;
        Ray İsik;
        
        if (YakinlasmaAktif)
        {
            İsik = YakınKamera.GetComponent<Camera>().ScreenPointToRay(CrosYakin.GetComponent<RectTransform>().position);
            Debug.DrawRay(İsik.origin, İsik.direction * 10f, Color.red);
        }
        else
        {
            İsik = Kamera.GetComponent<Camera>().ScreenPointToRay(Hedef_İcon.position);
            Debug.DrawRay(İsik.origin, İsik.direction * 10f, Color.red);
        }
        
        
        


        if (Physics.Raycast(İsik,out temas,Range,layermask))//Burda maskeleme yaparak Playeri devre dışı bıraktık maskelemede player hariç hepsi tickli
        {
            //Debug.Log("I hit " + temas.transform.name, temas.transform);
            Hedef.position = temas.point;
            //Hedef_İcon.position = temas.point;
            
            if (temas.collider.CompareTag("Düsman"))
            {
                temas.transform.GetComponentInParent<DüsmanKod>().Can -= 50;
                temas.transform.GetComponentInParent<DüsmanKod>().Anim.SetTrigger("Hit");
                GameObject Yenikan = Instantiate(KanParticle, temas.point, Quaternion.LookRotation(temas.normal));//temas.normal önemli yoksa gözükmüyor.
                Destroy(Yenikan, 2f);
            }
           else if (temas.collider.CompareTag("DüsKafa"))
            {
                temas.transform.GetComponentInParent<DüsmanKod>().Can -= 100;
                temas.transform.GetComponentInParent<DüsmanKod>().Anim.SetTrigger("Hit");
                GameObject Yenikan = Instantiate(KanParticle, temas.point, Quaternion.LookRotation(temas.normal));//temas.normal önemli yoksa gözükmüyor.
                Destroy(Yenikan, 2f);
            }
            else
            {
                GameObject Yenietki = Instantiate(MermiEtki, temas.point, Quaternion.LookRotation(temas.normal));
                Destroy(Yenietki, 5f);
            }
        }
        Namlu.LookAt(new Vector3(Hedef.position.x, Hedef.position.y, Hedef.position.z));
        //Namlu.transform.rotation = Quaternion.LookRotation(Hedef.forward);
        
    }
    private void Cekme()
    {
        FireSound.PlayOneShot(CekmeSes);
    }
    public void Shoot()
    {
        

        if (KnifeBut.Pressed&&ReloadKontrol==false)
        {
            float Aralik = 0.3f;
            if (Time.time>=KalanZaman)
            {
                KalanZaman = Time.time + Aralik;
                Anim.SetTrigger("Knife");
                FireSound.PlayOneShot(KnifeCekme);
            }
            BeklemeZamanKnife = true;
            
            Silah.SetActive(false);
            Bıc.SetActive(true);
            

        }
        if (BeklemeZamanKnife)
        {
            KnifeBekleme -= Time.deltaTime;

            if (KnifeBekleme <= 0)
            {
                Silah.SetActive(true);
                Bıc.SetActive(false);
                KnifeBekleme = 0.3f;
            }
        }
        if (Fire.Pressed&&Hor<=0.9&&Hor>=-0.9&&Ver<=0.9&&Ver>=-0.9&&MermiSayisi>0&&ReloadKontrol==false)
        {
            
            if (Time.time>=KalanZaman)
            {
                ShootAktif = true;
                HedefTespit();
                MermiSayisi--;
                KalanZaman = Time.time + AtesAralık;
                Anim.SetTrigger("Shoot");
                FireSound.PlayOneShot(FireSes);
                GameObject FirePar = Instantiate(FireParticle, Namlu.position, Namlu.transform.rotation);
                //GameObject YeniKursun = Instantiate(Kursun, Namlu.position, Quaternion.identity);
                //YeniKursun.GetComponent<Rigidbody>().velocity = Namlu.forward * 3000f * Time.deltaTime;
                //YeniKursun.GetComponent<Rigidbody>().AddForce(Namlu.forward * 30f);
                //YeniKursun.transform.rotation = Quaternion.LookRotation(Namlu.forward);
                Destroy(FirePar, 0.2f);
                GameObject[] Düsmanlar = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < Düsmanlar.Length; i++)
                {
                    Düsmanlar[i].GetComponent<DüsmanKod>().FarketmeDistanse = 500f;
                }
                //Destroy(YeniKursun, 30f);
            }
            
        }
        else
        {
            ShootAktif = false;
        }
        if (MermiSayisi<=0&&Fire.Pressed)
        {
            if (Time.time>=KalanZaman)
            {
                KalanZaman = Time.time + AtesAralık;
                FireSound.PlayOneShot(Nobullet);
            }
            
            MermiSayisi = 0;

        }
    }
    
    public void Reload()
    {
        if (ReloadButton.Pressed&&MermiSayisi<30)
        {
            ReklamInterstitial.showAd = true;
            FireSound.PlayOneShot(ReloadSes);
            ReloadKontrol = true;
            ReloadZaman = true;
            ReloadBekleTime -= Time.deltaTime;
            
            Anim.SetTrigger("Reload");
            MermiSayisi = 30;
            if (ReloadBekleTime <= 0)
            {
                ReloadKontrol = false;
                ReloadBekleTime = 3.2f;
            }
        }
        if (ReloadZaman==true)
        {
            
            ReloadBekleTime -= Time.deltaTime;
            if (ReloadBekleTime <= 0)
            {
                ReloadKontrol = false;
                ReloadBekleTime = 3.2f;
                ReloadZaman = false;
            }
        }
    }
    void Yakınlasma()
    {
        if (Aim.Pressed)
        {
            MainKamera.GetComponent<Camera>().enabled=false;
            YakınKamera.GetComponent<Camera>().enabled = true;
            YakinlasmaAktif = true;
            CrosYakin.SetActive(true);
            Cross.SetActive(false);



        }
        else
        {
            MainKamera.GetComponent<Camera>().enabled = true;
            YakınKamera.GetComponent<Camera>().enabled = false;
            YakinlasmaAktif = false;
            CrosYakin.SetActive(false);
            Cross.SetActive(true);
        }
        


    }
    
}
