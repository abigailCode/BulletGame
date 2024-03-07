using UnityEngine;

public class UpdateImageRotation : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        // Obtener la referencia al RectTransform de la imagen
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Obtener la posición del cursor en la ventana
        Vector3 mousePosition = Input.mousePosition;

        // Obtener el centro de la pantalla en coordenadas del mundo
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 centerDirection = mousePosition - screenCenter;

        // Calcular el ángulo en radianes entre el eje central y la posición del cursor
        float angle = Mathf.Atan2(centerDirection.y, centerDirection.x) * Mathf.Rad2Deg;

        // Rotar la imagen hacia la posición del cursor
        rectTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
}
