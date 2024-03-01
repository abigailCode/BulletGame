using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Referencias privadas accesibles desde el inspector
    [SerializeField] GameObject enemy;

    // Variables públicas
    public float timeBetweenGenerations = 1f;
    public float spawnLineLength = 2f;

    // Método que se ejecuta en el primer frame
    void Start()
    {
        // Lanzamos la corrutina al arrancar el script
        StartCoroutine(GenerateEnemy());

        // Iniciamos la corrutina para aumentar la velocidad
        StartCoroutine(IncreaseSpeedCoroutine());
    }

    IEnumerator GenerateEnemy()
    {

        // La corrutina estará siempre en ejecución
        while (true)
        {
            // Posición aleatoria de spawn a lo largo de la línea
            float randomPosY = Random.Range(transform.position.y - spawnLineLength, transform.position.y + spawnLineLength);

            // Instanciamos el nuevo enemigo en una posición vertical aleatoria
            GameObject newEnemy = Instantiate(enemy, new Vector2(transform.localPosition.x, randomPosY), Quaternion.identity);
            newEnemy.transform.SetParent(null); // Desvinculamos el enemigo del objeto padre

            // La corrutina se pausa durante el tiempo indicado
            // en la variable pública timeBetweenGenerations
            yield return new WaitForSeconds(timeBetweenGenerations);

        }
    }

    public IEnumerator IncreaseSpeedCoroutine()
    {
        while (true)
        {
            // Esperamos 5 segundos
            yield return new WaitForSeconds(5f);

            // Aumentamos la velocidad
            timeBetweenGenerations -= 0.25f;

            // **Limitar la velocidad máxima**
            if (timeBetweenGenerations < 0.25f)
            {
                timeBetweenGenerations = 0.25f;
            }
        }
    }

}
