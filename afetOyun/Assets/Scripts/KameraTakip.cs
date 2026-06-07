using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform hedef; 
    public Vector3 mesafe = new Vector3(0f, 10f, -5f); 

    void LateUpdate()
    {
        if (hedef != null)
        {
            // Kameranýn pozisyonunu karakterin pozisyonuna göre sürekli güncelle
            transform.position = hedef.position + mesafe;

            // Kameranýn sürekli karaktere doðru bakmasýný saðla
            transform.LookAt(hedef.position);
        }
    }
}
