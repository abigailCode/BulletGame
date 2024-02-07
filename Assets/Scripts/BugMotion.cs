using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMotion : MonoBehaviour
{
    [SerializeField] float bugSpeed = 5; // Variables privadas accesibles desde el inspector
    [SerializeField] GameObject target; // Referencias privadas accesibles desde el inspector

    // Método Start() del script BugMotion
    private void Start()
    {
        // Cargamos la referencia al objeto que tenga la etiqueta Player
        // (previamente deberíamos haber asociado dicha etiqueta a la torreta)
        target = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {

        // Restamos las posiciones del enemigo y del objetivo
        // para calcular la dirección hacia la que debe mirar (normalizada = magnitud 1)
        Vector2 direction = (target.transform.position - transform.position).normalized;

        transform.up = direction; // Hacemos que el enemigo mire al objetivo en todo momento

        // Ahora que el bicho ya mira todo el tiempo al objetivo
        // hay que iniciar el movimiento del mismo
        GetComponent<Rigidbody2D>().velocity = direction * bugSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) {

            //Bullet collision
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                GameObject.Find("GameManager").GetComponent<GameManager>().muertes++;
                GameObject.Find("GameManager").GetComponent<GameManager>().UpdateMuertes();
            }
          
        }
    }

  
}


