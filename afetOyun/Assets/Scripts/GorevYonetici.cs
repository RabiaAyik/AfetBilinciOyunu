using UnityEngine;
using TMPro;

public class GorevYonetici : MonoBehaviour
{
    [Header("Görev Yazı Nesneleri")]
    public TextMeshProUGUI egilmeText;
    public TextMeshProUGUI bezText;
    public TextMeshProUGUI sigortaText;
    public TextMeshProUGUI kapiText;
    public TextMeshProUGUI telefonText;

    // Görevlerin tamamlanma durumları
    private bool egildiMi = false;
    private bool bezAlindiMi = false;
    private bool sigortaKapandiMi = false;
    private bool kapiyaUlastiMi = false;
    private bool telefonArandiMi = false;


    public GameObject cikisKapisiColliderNesnesi;
    // Diğer kodlardan bu koda kolayca ulaşabilmek için (Singleton)
    public static GorevYonetici instance;

    void Awake()
    {
        instance = this;
    }

   //gorev tamamlama fonkslari

    public void GorevEgilmeYapildi()
    {
        if (!egildiMi)
        {
            egildiMi = true;
            egilmeText.text = "✔️ Eğilerek yürümeyi buldun!";
            egilmeText.color = Color.green;
        }
    }

    public void GorevBezAlindi()
    {
        if (!bezAlindiMi)
        {
            bezAlindiMi = true;
            bezText.text = "✔️ Islak bezi aldın!";
            bezText.color = Color.green;
        }
    }

    public void GorevSigortaKapatildi()
    {
        if (!sigortaKapandiMi)
        {
            sigortaKapandiMi = true;
            if (cikisKapisiColliderNesnesi != null)
            {
                BoxCollider kapininCollideri = cikisKapisiColliderNesnesi.GetComponent<BoxCollider>();
                if (kapininCollideri != null)
                {
                    kapininCollideri.enabled = false; // Kapatmış olduğun o tiki kod otomatik açar!
                }
            }
            sigortaText.text = "✔️ Sigortayı kapattın!";
            sigortaText.color = Color.green;
        }
    }

    public void GorevKapiyaUlasti()
    {
        if (!kapiyaUlastiMi)
        {
            kapiyaUlastiMi = true;
            kapiText.text = "✔️ Çıkış kapısına ulaştın!";
            kapiText.color = Color.green;
        }
    }

    public void GorevTelefonArandi()
    {
        if (!telefonArandiMi)
        {
            telefonArandiMi = true;
            telefonText.text = "✔️ 112 Acil Çağrı Merkezini aradın!";
            telefonText.color = Color.green;
        }
    }
}

