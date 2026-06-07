using UnityEngine;

public class KapiTetikleyici : MonoBehaviour
{
    [Header("UI Ayarlarý")]
    public GameObject tahliyeEkrani;
   
    private void OnTriggerEnter(Collider other)
    {
        // Kapýdan geçen nesnenin Tag'ý "Player" ise (yani bizim oyuncumuzsa)
        if (other.CompareTag("Player"))
        {
            if (tahliyeEkrani != null)
            {
                tahliyeEkrani.SetActive(true); // Telefonlu siyah ekraný görünür yap
               
                // Fare imlecini (mouse) serbest býrak ve görünür yap ki tuþlara basýlabilsin
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (GorevYonetici.instance != null) GorevYonetici.instance.GorevKapiyaUlasti();
            }
        }
    }
}
