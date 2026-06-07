using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneYonetici : MonoBehaviour
{
        public void AnaMenuyeDon()
        {
            Time.timeScale = 1f;
           SceneManager.LoadScene("MenuScene");
        }
    public void OyunaDon()
    {
        // ÇOK ÖNEMLÝ: Sahne yüklenmeden ÖNCE zamaný mutlaka 1 yapmalýyýz!
        Time.timeScale = 1f;

        // Oyun sahnenin adý neyse onu yaz (Örn: "KutupScene")
        SceneManager.LoadScene("KutupScene");
    }
}
