using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Referencia al sprite que contiene la imagen para el cursor
    [SerializeField] Texture2D cursorTarget;
    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] TextMeshProUGUI textoDisparos;
    [SerializeField] TextMeshProUGUI textoMuertes;
    // Variable pública para modificar desde cualquier script
    public int disparos = 0;
    public int muertes = 0;

    private void Start()
    {
        // Configuramos el cursor con el sprite de la mirilla.
        // El segundo parámetro indica el punto efectivo del cursor (como la
        // imagen del cursor tiene 32x32 píxeles, el centro estará en 16, 16)
        // y el tercer parámetro indica el modo de renderizado del mismo
        // Configuramos el cursor con el sprite de la mirilla
        Vector2 hotspot = new Vector2(cursorTarget.width / 2, cursorTarget.height / 2);
        Cursor.SetCursor(cursorTarget, hotspot, CursorMode.Auto);

        // Actualizamos la información
        UpdateDisparos();

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    // Método público que actualiza el texto de los disparos
    public void UpdateDisparos()
    {
        // Actualizamos el texto de las balas disparadas
        textoDisparos.text = "Disparos: " + disparos;
    }

}
