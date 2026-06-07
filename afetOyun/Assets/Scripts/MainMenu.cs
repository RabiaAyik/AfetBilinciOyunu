using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void Oyna()
    {
        SceneManager.LoadScene("KutupScene");

    }


    public void Cikis()
    {
        Debug.Log("Oyundan çýkýþ yapýldý."); // Editörde çalýþýp çalýþmadýðýný test etmek için log bastýk.
        Application.Quit(); // Derlenmiþ (Build alýnmýþ) oyunda oyunu tamamen kapatýr.

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

   

}
