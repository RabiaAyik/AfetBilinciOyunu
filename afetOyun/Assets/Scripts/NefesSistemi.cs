using UnityEngine;
using UnityEngine.UI;

public class NefesSistemi : MonoBehaviour
{
    [Header("UI Ayarlari")]
    public Slider nefesSlider;
    public GameObject yenidenDeneEkrani;
    public Transform kalpNesnesi; 
    private float zamanSayaci = 0f;
    public GameObject gorevEkrani;

    [Header("Nefes Degerleri")]
    public float maxNefes = 100f;
    private float mevcutNefes;

    public float azalmaHizi = 15f;
    public float dolmaHizi = 10f;

    private bool dumanIcindeMi = false;
    private PlayerController playerController;

    void Start()
    {
        mevcutNefes = maxNefes;

        if (nefesSlider != null)
        {
            nefesSlider.maxValue = maxNefes;
            nefesSlider.value = maxNefes;
        }

        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        EtkilesimSistemi etkilesim = GetComponent<EtkilesimSistemi>();

        if (dumanIcindeMi && !playerController.isCrouching)
        {
            // mevcutNefes -= azalmaHizi * Time.deltaTime;
            if (etkilesim != null && etkilesim.bezIslandiMi)
            {
                // Ýslak bez agzinda oldugu icin duman etkilemiyor, can stabil kalir.
            }
            else
            {
                // Islak bez yoksa normal sekilde nefes azalir
                mevcutNefes -= (azalmaHizi -5)* Time.deltaTime;
            }
        }
        else 
        {
            if (mevcutNefes < maxNefes)
            {
                mevcutNefes += dolmaHizi * Time.deltaTime;
            }
        }

        mevcutNefes = Mathf.Clamp(mevcutNefes, 0f, maxNefes);

        if (nefesSlider != null)
        {
            nefesSlider.value = mevcutNefes;
        }

        if (mevcutNefes <= 0f)
        {
            OyunBitti();
        }
        if (mevcutNefes < 60f)
        {
            zamanSayaci += Time.deltaTime * 10f; 
            float olcek = 0.5f + Mathf.Sin(zamanSayaci) * 0.15f; 
            kalpNesnesi.localScale = new Vector3(olcek, olcek, 1f);

           
            kalpNesnesi.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
        else
        {
            
            kalpNesnesi.localScale = new Vector3(0.5f,0.5f);
            kalpNesnesi.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DumanAlani")
        {
            dumanIcindeMi = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DumanAlani")
        {
            dumanIcindeMi = false;
        }
    }

    void OyunBitti()
    {
        Debug.Log("Nefes bitti! Oyun bitti.");
        if (yenidenDeneEkrani != null)
        {
            yenidenDeneEkrani.SetActive(true); // Kýrmýzý uyarý panelini açar
            gorevEkrani.SetActive(false);
            // Oyunu durdurmak ve farenin ekranda çýkmasýný saðlamak için:
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
