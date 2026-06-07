using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TelefonSistemi : MonoBehaviour
{
    public TextMeshProUGUI numaraYazisi; // UI'daki numara ekraný
    public GameObject basariEkrani;      // Oyun bittiðinde açýlacak tebrik paneli
    private string girilenNumara = "";
    public GameObject gorevEkrani;

    // Tuþlara (1,2,3) basýldýðýnda çalýþacak fonksiyon
    public void TusaBas(string rakam)
    {
        if (girilenNumara.Length < 3) // 3 haneden fazla yazýlmasýn
        {
            girilenNumara += rakam;
            numaraYazisi.text = girilenNumara;
        }
    }

    // Silme (C) tuþuna basýlýrsa
    public void NumarayiSil()
    {
        girilenNumara = "";
        numaraYazisi.text = "";
    }

    // Yeþil Arama butonuna basýldýðýnda
    public void AramayiYap()
    {
        if (girilenNumara == "112")
        {
            numaraYazisi.text = "Aranýyor... Ýtfaiye Yolda!";
            if (GorevYonetici.instance != null) GorevYonetici.instance.GorevTelefonArandi();
            // 4 saniye sonra baþarý ekranýný aç veya oyunu bitir
            Invoke("OyunBitti", 4f);
        }
        else
        {
            numaraYazisi.text = "Hatalý Numara!";
            girilenNumara = "";
        }
    }

    void OyunBitti()
    {
        if (basariEkrani != null) basariEkrani.SetActive(true);
        gorevEkrani.SetActive(false);
        Time.timeScale = 0f; // Oyunu dondur (Bitti)
    }
}
