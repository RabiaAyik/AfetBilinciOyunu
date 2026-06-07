using TMPro;
using UnityEngine;

public class EtkilesimSistemi : MonoBehaviour
{
    [Header("UI Ayarlari")]
    public TextMeshProUGUI uyariText; 

    [Header("Envanter Durumu")]
    private bool bezVarMi = false;
    private bool suVarMi = false;
    public bool bezIslandiMi = false; // Bunu birazdan NefesSistemi okuyacak!

    private Collider yakinlasilanObje = null;

    void Update()
    {
        // Eger oyuncu bir objeye yakinsa VE klavyeden E tusuna bastiysa
        if (yakinlasilanObje != null && Input.GetKeyDown(KeyCode.E))
        {
            ObjeTopla(yakinlasilanObje);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Karakter toplanabilir bir objenin yanina geldiginde hafizaya al
        if (other.CompareTag("KuruBez") || other.CompareTag("SuSisesi"))
        {
            yakinlasilanObje = other;
            uyariText.text = "Almak için [E] tusuna bas.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Objeden uzaklasinca hafizayi temizle ve yaziyi sil
        if (other == yakinlasilanObje)
        {
            yakinlasilanObje = null;
            uyariText.text = "";
        }
    }

    void ObjeTopla(Collider obje)
    {
        if (obje.CompareTag("KuruBez"))
        {
            bezVarMi = true;
            Destroy(obje.gameObject); // Sahnedeki bezi yok et
            yakinlasilanObje = null;

            // Mantik kontrolu
            if (!suVarMi)
            {
                uyariText.text = "Bezi aldýn, ama kuru. Bezi ýslatman gerekiyor! Su bul.";
            }
            else
            {
                BeziIslat();
            }
        }
        else if (obje.CompareTag("SuSisesi"))
        {
            suVarMi = true;
            Destroy(obje.gameObject); // Sahnedeki su sisesini yok et
            if (GorevYonetici.instance != null) GorevYonetici.instance.GorevBezAlindi();
            yakinlasilanObje = null;

            if (!bezVarMi)
            {
                uyariText.text = "Su þisesini aldýn. Þimdi bir bez bulmalýsýn.";
            }
            else
            {
                BeziIslat();
            }
        }
    }

    void BeziIslat()
    {
        bezIslandiMi = true;
        uyariText.text = "Harika! Bezi suyla ýslattýn ve aðzýný kapattýn.Koruma aktif! Ayaða kalkabilirsin.";
        Invoke("YaziyiTemizle", 3f); // 4 saniye sonra ekrandaki tebrik yazisini siler
    }

    void YaziyiTemizle()
    {
        if (uyariText != null) uyariText.text = "";
    }
}
