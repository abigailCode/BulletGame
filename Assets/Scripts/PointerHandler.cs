// -------------------------------------------------
// SCRIPT PARA DETECTAR INTERACCIONES CON EL MOUSE
// -------------------------------------------------
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI; // Añadir librería de interfaz de usuario (si no estuviera ya)

// Implementamos las interfaces IPointerEnterHandler & IPointerExitHandler
public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Variable privada para controlar si el mouse está sobre el elemento
    private bool isHover = false;

    // Método que detecta cuando se pasa el ratón por encima del área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        // El mouse entra en el área del botón
        isHover = true; // Marcamos la variable para indicar que el mouse se superpone
    }

    // Método que detecta cuando sale el botón del área del ratón
    public void OnPointerExit(PointerEventData eventData)
    {
        // El mouse sale del área del botón
        isHover = false; // Desmarcamos la variable indicando que el mouse ya no se superpone
    }

    // Método que se ejecuta una vez en cada frame
    void Update()
    {
        // Si el mouse está sobre el elemento se desactivan los disparos hay que mantenerlos
        // desactivados constantemente porque hay otro script que los reactiva
        if (isHover)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().DisableFire();
        }
    }
}
