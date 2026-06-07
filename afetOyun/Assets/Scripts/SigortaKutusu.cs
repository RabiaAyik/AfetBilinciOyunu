using UnityEngine;
using TMPro;
public class SigortaKutusu : MonoBehaviour
{
    private bool oyuncuYakininda = false;
    private bool sigortaKapandi = false;

    [Header("Kapak Ayarlarý")]
    public GameObject kutuKapagi;
    public float kapakAcilmaAcisi = 90f; // Kapaðýn ne kadar açýlacaðý (Duruma göre 90, -90 veya 120 yapabilirsin)
    public float acilmaHizi = 2f; 

    private Quaternion hedefDonus;
    public TextMeshProUGUI uyariYazisiTextMesh;

    void Start()
    {
        // Oyun baþýnda kapaðýn mevcut dönüþünü (kapalý halini) kaydediyoruz
        if (kutuKapagi != null)
        {
            hedefDonus = kutuKapagi.transform.localRotation;
        }
    }

    void Update()
    {
        // Oyuncu kutunun yanýndaysa, sigorta henüz kapanmadýysa ve "E" tuþuna basarsa
        if (oyuncuYakininda && !sigortaKapandi && Input.GetKeyDown(KeyCode.E))
        {
            SigortayiKapat();
            if (GorevYonetici.instance != null) GorevYonetici.instance.GorevSigortaKapatildi();
        }

        // Eðer sigorta kapatýldýysa, kapaðý her karede yavaþça hedef dönüþe doðru döndür (Yumuþak animasyon)
        if (sigortaKapandi && kutuKapagi != null)
        {
            kutuKapagi.transform.localRotation = Quaternion.Slerp(kutuKapagi.transform.localRotation, hedefDonus, Time.deltaTime * acilmaHizi);
        }
    }

    private void SigortayiKapat()
    {
        sigortaKapandi = true;
        Debug.Log("Sigorta kapaðý açýldý ve elektrik kesildi!");

        // Görev yöneticisindeki listenin yanýna yeþil tiki atýyoruz
        if (GorevYonetici.instance != null)
        {
            GorevYonetici.instance.GorevSigortaKapatildi();
        }

        // Kapaðýn açýlmasý için yeni hedef açýsýný hesaplýyoruz (Y ekseninde döndürürüz)
        if (kutuKapagi != null)
        {
            // Kutunun model tasarýmýna göre gerekirse transform.localRotation.eulerAngles.x veya z ile de birleþtirebilirsin
            hedefDonus = Quaternion.Euler(0f, kapakAcilmaAcisi, 0f);
        }
    }

    // Oyuncu kutunun etrafýndaki yeþil alana girdiðinde
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !sigortaKapandi)
        {
            oyuncuYakininda = true;
            if (uyariYazisiTextMesh != null)
            {
                uyariYazisiTextMesh.text = "Sigortayý kapatýp elektriði kesmek için 'E' tuþuna bas!";
                Invoke("YaziyiTemizle", 3f);
            }
           
        }
    }

    // Oyuncu yeþil alandan uzaklaþtýðýnda
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuYakininda = false;
            
        }
    }

    void YaziyiTemizle()
    {
        if (uyariYazisiTextMesh != null) uyariYazisiTextMesh.text = "";
    }
}

