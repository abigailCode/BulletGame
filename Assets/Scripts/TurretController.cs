using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    // Referencias privadas accesibles desde el Inspector
    [SerializeField] GameObject bulletPrefab; // Prefab con la bala
    [SerializeField] GameObject spawnPoint; // Posición desde la que instanciar balas                          
    [SerializeField] float bulletSpeed = 10;
    Vector2 mousePosition; // Para almacenar las coordenadas en píxeles del mouse
    Vector2 worldMousePosition; // Para almacenar las coordenadas equivalentes del mundo                    
    Vector2 pointDirection; // Para almacenar el vector de dirección que apunta al mouse


    private void Update()
    {
      
        // Obtenemos la posición del mouse
        mousePosition = Input.mousePosition; // Coordenadas de pantalla

        // Transformamos las coordenadas de pantalla en coordenadas del mundo
        worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Hacemos que el GameObject apunte a la posición del mouse
        // calculando el vector que contiene la dirección hacia el mouse
        pointDirection = worldMousePosition - (Vector2)transform.position;


        // Ajustamos la rotación del objeto para que apunte hacia la dirección del ratón
        // Recordatorio: un vector normalizado es un vector que tiene magnitud 1
        transform.up = pointDirection.normalized;

    
       
        // Detectamos si se ha hecho clic con el botón primario del ratón
        // En ese caso instanciamos la bala en la posición de spawn
        if (Input.GetMouseButtonDown(0))
        {

            // Instanciamos la bala en la posición de spawn
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform);

            // Eliminamos la dependencia del objeto padre spawnPoint
            bullet.transform.SetParent(null);

            // Configuramos la velocidad de movimiento
            bullet.GetComponent<Rigidbody2D>().velocity = pointDirection.normalized * bulletSpeed;
        }


    }
}

