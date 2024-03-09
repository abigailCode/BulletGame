using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    // Referencias privadas accesibles desde el Inspector
    [SerializeField] GameObject bulletPrefab; // Prefab con la bala
    [SerializeField] GameObject spawnPoint; // Posici�n desde la que instanciar balas                          
    [SerializeField] float bulletSpeed = 10;
    Vector2 mousePosition; // Para almacenar las coordenadas en p�xeles del mouse
    Vector2 worldMousePosition; // Para almacenar las coordenadas equivalentes del mundo                    
    Vector2 pointDirection; // Para almacenar el vector de direcci�n que apunta al mouse
    // Variables privadas
    private bool turretCanShoot; // Para almacenar si se puede disparar o no

    private void Update()
    {

        // Obtenemos el valor del GameManager que controla si se puede disparar o no
        turretCanShoot = GameObject.Find("GameManager").GetComponent<GameManager>().GetShootingStatus();

        // Detectamos si se ha hecho clic con el bot�n primario del rat�n
        // En ese caso instanciamos la bala en la posici�n de spawn
        if (Input.GetMouseButtonDown(0) && turretCanShoot)
        {

            // Instanciamos la bala en la posici�n de spawn
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform);
            AudioManager.instance.PlaySFX("Shoot");


            // Eliminamos la dependencia del objeto padre spawnPoint
            bullet.transform.SetParent(null);

            // Configuramos la velocidad de movimiento
            bullet.GetComponent<Rigidbody2D>().velocity = pointDirection.normalized * bulletSpeed;
        }

        // Volvemos a activar los disparos si no se hace clic en el �tem
        GameObject.Find("GameManager").GetComponent<GameManager>().EnableFire();

        // Obtenemos la posici�n del mouse
        mousePosition = Input.mousePosition; // Coordenadas de pantalla

        // Transformamos las coordenadas de pantalla en coordenadas del mundo
        worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Hacemos que el GameObject apunte a la posici�n del mouse
        // calculando el vector que contiene la direcci�n hacia el mouse
        pointDirection = worldMousePosition - (Vector2)transform.position;


        // Ajustamos la rotaci�n del objeto para que apunte hacia la direcci�n del rat�n
        // Recordatorio: un vector normalizado es un vector que tiene magnitud 1
        transform.up = pointDirection.normalized;

    
       
     


    }
}

