using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    

    [Header("Hareket Ayarlarý")]
    public float yurumeHizi = 5f;
    public float egilmeHizi = 2.5f; 
    private float aktifHiz;

    [Header("Eðilme Ayarlarý")]
    public Transform gorselNesne; 
    private Vector3 orijinalBoyut;
    private Vector3 egilmeBoyutu;
    public bool isCrouching = false; 

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        Time.timeScale=1f;
        aktifHiz = yurumeHizi;

        if (gorselNesne != null)
        {
            orijinalBoyut = gorselNesne.localScale;
            
            egilmeBoyutu = new Vector3(orijinalBoyut.x, orijinalBoyut.y * 0.5f, orijinalBoyut.z);


            Vector3 yerelPozisyon = gorselNesne.localPosition;
            yerelPozisyon.y = 0f;
            gorselNesne.localPosition = yerelPozisyon;
        }
    }

    void Update()
    {
        float yatay = Input.GetAxis("Horizontal"); // A-D veya Sol-Sað ok tuþlarý
        float dikey = Input.GetAxis("Vertical");   // W-S bilge Üst-Alt ok tuþlarý

        Vector3 hareketYonu = new Vector3(yatay, 0f, dikey).normalized;
        Vector3 yercekimiHareketi = Vector3.zero;

        // Eðer karakter yerde DEÐÝLSE (havadaysa) sürekli aþaðý doðru bir kuvvet uygula
        if (!controller.isGrounded)
        {
            // Karakteri saniyede 9.81f hýzýyla sürekli aþaðý çeker (Gerçek fizik)
            yercekimiHareketi.y -= 9.81f * Time.deltaTime;
        }
        else
        {
            // Yerdeyken hafifçe zemine bastýr ki isGrounded kontrolü tam çalýþsýn
            yercekimiHareketi.y = -0.1f;
        }

        // Yerçekimi kuvvetini her karede (tuþa basýlsýn ya da basýlmasýn) karaktere uygula
        controller.Move(yercekimiHareketi * Time.deltaTime);
        // ----------------------------------------

        if (hareketYonu.magnitude >= 0.2f) // Önceki adýmda titremeyi önlemek için 0.2f yapmýþtýk
        {
            Quaternion hedefDonus = Quaternion.LookRotation(hareketYonu);
            transform.rotation = Quaternion.Slerp(transform.rotation, hedefDonus, Time.deltaTime * 5f);

            // Karakteri yatay düzlemde yürütüyoruz
            controller.Move(hareketYonu * aktifHiz * Time.deltaTime);

            if (anim != null) anim.SetBool("isWalking", true);
        }
        else
        {
            if (anim != null) anim.SetBool("isWalking", false);
        }

        // Eðilme Kontrolleri (C Tuþu)
        if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
            aktifHiz = egilmeHizi;
            if (gorselNesne != null) gorselNesne.localScale=egilmeBoyutu; // Mevcut boyut küçültme mantýðýn

            if (anim != null) anim.SetBool("isCrouching", true);
            if (GorevYonetici.instance != null) GorevYonetici.instance.GorevEgilmeYapildi();
        }
        else
        {
            isCrouching = false;
            aktifHiz = yurumeHizi;
            if (gorselNesne != null) gorselNesne.localScale = orijinalBoyut;

            if (anim != null) anim.SetBool("isCrouching", false);
        }
    }
    /*if (hareketYonu.magnitude >= 0.1f)
    {
        Quaternion hedefDonus = Quaternion.LookRotation(hareketYonu);
        transform.rotation = Quaternion.Slerp(transform.rotation, hedefDonus, Time.deltaTime * 2f);

        controller.Move(hareketYonu * aktifHiz * Time.deltaTime);
        controller.Move(Vector3.down * 2f * Time.deltaTime);
        if (anim != null) anim.SetBool("isWalking", true);          
    }
    else
    {

        if (anim != null) anim.SetBool("isWalking", false);
    }

    if (Input.GetKey(KeyCode.C))
    {
        isCrouching = true;
        aktifHiz = egilmeHizi;
        if (gorselNesne != null) gorselNesne.localScale = egilmeBoyutu;

        if (anim != null) anim.SetBool("isCrouching", true);
    }
    else // C tuþu býrakýldýðýnda ayaða kalk
    {
        isCrouching = false;
        aktifHiz = yurumeHizi;
        if (gorselNesne != null) gorselNesne.localScale = orijinalBoyut;

        if (anim != null) anim.SetBool("isCrouching", false);
    }*/
}

