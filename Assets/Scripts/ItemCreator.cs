using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    // Referencias privadas accesibles desde el inspector
    [SerializeField] GameObject[] itemPrefabs; // Vector con los prefabs de los �tems

    // Variables p�blicas
    public float probability = 0.25f;

    // M�todo que instancia uno de los objetos de la lista
    // de prefabs configurada en el inspector teniendo en cuenta
    // el valor de la probabilidad y la posici�n pasada como par�metro
    public void ItemGenerator(Transform dropPosition)
    {

        // Seleccionamos una de las posiciones del vector al azar
        int options = itemPrefabs.Length;
        int randomOption = Random.Range(0, options);

        // Calculamos la probabilidad generando un
        // un n�mero aleatorio entre 0 y 1
        float randomProbability = Random.Range(0f, 1f);

        // Si la probabilidad se cumple se genera el �tem aleatorio
        if (randomProbability <= probability)
        {
            // Instanciamos un objeto aleatorio de la lista
            GameObject newItem = Instantiate(itemPrefabs[randomOption], dropPosition);

            // Cambiamos la capa del objeto que se acaba de instanciar
            newItem.layer = LayerMask.NameToLayer("DroppedItems");

            // Ignoramos la colisi�n entre objetos de las capas por defecto
            // (o la que hayas configurado para los enemigos) y DroppedItems
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("DroppedItems"), LayerMask.NameToLayer("Default"));

            // Lo desvinculamos del objeto padre
            newItem.transform.SetParent(null);
        }
    }

}
