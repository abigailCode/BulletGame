using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------------------------------------------------------------
// SCRIPT PARA RECORRER Y DESTRUIR TODOS LOS OBJETOS HIJO DE UN OBJETO PADRE
// --------------------------------------------------------------------------
public class Slot : MonoBehaviour
{
    private Inventory inventario; // Para guardar la referencia al script del inventario
    [SerializeField] int slotIndex; // Para indicar el slot con el que se vincula

    private void Start()
    {
        // Obtenemos la referencia al script del Inventario que hay asociado al GameManager
        inventario = GameObject.Find("GameManager").GetComponent<Inventory>();
    }

    void Update()
    {
        // Comprobamos si el slot ya no tiene objetos hijos y en caso de que no los tenga
        // marcamos la posición del vector isFull como FALSE (esto requiere configurar
        // cada Slot con el índice correspondiente de isFull).
        if (transform.childCount <= 0) inventario.isFull[slotIndex] = false;
    }

    // Método público para desechar objetos del inventario (es público porque lo
    // llamaremos desde un botón que no tiene dicho script como componente)
    public void DropItem()
    {

        // Recorre todos los hijos vinculados al objeto padre
        // que contiene este script y los destruye
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}
