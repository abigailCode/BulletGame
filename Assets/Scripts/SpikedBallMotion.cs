using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallMotion : MonoBehaviour
{
    // Referencia privada al objeto ForcePoint
    GameObject forcePointRef;
    [SerializeField] float rotationForce = 30f;
    [SerializeField] float lifeSpan = 5f;
    private float time;

    void Start()
    {
        // Obtenemos la referencia al objeto sobre el
        // que se aplicar� la fuerza
        forcePointRef = GameObject.Find("ForcePoint");
    }


    void Update()
    {
        // Aplica una fuerza en la direcci�n local que apunta a la derecha (como al
        // cambiar la rotaci�n la direcci�n tambi�n lo hace, funcionar� perfectamente)
        forcePointRef.GetComponent<Rigidbody2D>().velocity = forcePointRef.transform.right * rotationForce;

        // Obtenemos el tiempo que ha pasado desde el anterior
        // frame y lo vamos acumulando en la variable time
        time += Time.deltaTime;

        // Comprobamos si se ha alcanzado el tiempo de vida y si lo hizo se
        // destruir� el gameObject de la trampa
        if (time >= lifeSpan) Destroy(gameObject);
    }

}
