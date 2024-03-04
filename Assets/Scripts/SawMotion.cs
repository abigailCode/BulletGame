using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMotion : MonoBehaviour
{
    // Variables privadas accesibles desde el inspector
    [SerializeField] float sawMotionSpeed = 2.0f;
    [SerializeField] int offsetScreen = 50;


    void Start()
    {
        // Obtenemos la coordenada X del Player
        float xCoord = GameObject.FindGameObjectWithTag("Player").transform.position.x;

        // Posicionamos la trampa a la misma altura del Player
        transform.position = new Vector3(xCoord, transform.position.y, transform.position.z);
    }


    // Método que se ejecuta en cada frame del juego
    private void Update()
    {
        // Movemos el objeto utiliza el método Translate
        transform.Translate(Vector3.right * sawMotionSpeed * Time.deltaTime);

        // Obtenemos la posición horizontal de la trampa en píxeles
        float xPosInPixels = Camera.main.WorldToScreenPoint(transform.position).x;

        // Calculamos si la sierra ha superado el límite derecho de la pantalla sumándole
        // un offset para darle un margen y que se destruya cuando esté totalmente fuera
        if (xPosInPixels > Screen.width + offsetScreen) Destroy(gameObject);
    }


}
