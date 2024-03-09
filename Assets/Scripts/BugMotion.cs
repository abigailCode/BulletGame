using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMotion : MonoBehaviour
{
    public GameObject Blood;
    [SerializeField] float bugSpeed = 2; // Variables privadas accesibles desde el inspector
    [SerializeField] GameObject target; // Referencias privadas accesibles desde el inspector
    [SerializeField] float circleAttackRadius = 0.1f; // Radio del collider de ataque
    [SerializeField] GameObject hitPoint; // Referencia al punto de ataque
    [SerializeField] int damageValue = 1; // Cantidad de da�o que hace el enemigo
                                          // Cantidad de tiempo que se pausa la corrutina antes de empezar a restar vida

    // Variable para controlar la longitud de la l�nea
    [SerializeField] float detectionDistance = 0.5f;

    // Variable para controlar el offset de comienzo de la l�nea y que no se solape con el collider del bicho
    [SerializeField] float offsetDistance = 0.2f;
    [SerializeField] float attackDelay = 0.68f;

    // Variable privada para garantizar que una misma instancia del prefab
    // s�lo sea capaz de lanzar una �nica corrutina WaitAndAttack() al mismo tiempo.
    bool executingCoroutine = false;

    // M�todo Start() del script BugMotion
    private void Start()
    {
        // Cargamos la referencia al objeto que tenga la etiqueta Player
        // (previamente deber�amos haber asociado dicha etiqueta a la torreta)
        target = GameObject.FindGameObjectWithTag("Player");

       
    }


    private void Update()
    {

        // Restamos las posiciones del enemigo y del objetivo
        // para calcular la direcci�n hacia la que debe mirar (normalizada = magnitud 1)
        Vector2 direction = (target.transform.position - transform.position).normalized;

        // Hacemos que el enemigo mire al objetivo en todo momento
        transform.up = direction;

        // Llamamos al m�todo que dibuja la l�nea del RayCastHit2D y devuelve true
        // si ha llegado al destino, false en caso contrario. Por lo tanto, se mover�
        // mientras no detecte al jugador
        if (!IsDetectingPlayer()) GetComponent<Rigidbody2D>().velocity = direction * GameObject.Find("GameManager").GetComponent<GameManager>().bugSpeed;
        else
        {
            SetAnimation("attacking");
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // ---------------------------------------------------------------------------
            // Si ha detectado al jugador se lanza la corrutina de Espera y Ataque siempre
            // que dicha corrutina no est� ya en ejecuci�n para la instancia del prefab
            if (!executingCoroutine) StartCoroutine(WaitAndAttack());
            // ---------------------------------------------------------------------------
        }

        //while bugSpeed <20 increment bugSpeed each 5 seconds
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) {

            //Bullet collision
            if (collision.gameObject.CompareTag("Bullet"))
            {
                AudioManager.instance.PlaySFX("EnemyDamage");
                Instantiate(Blood, transform.position, Quaternion.identity);
                GameObject.Find("GameManager").GetComponent<GameManager>().muertes++;
                GameObject.Find("GameManager").GetComponent<GameManager>().UpdateMuertes();
                // Se llama al generador de objetos aleatorios
                GameObject.Find("Instanciador").GetComponent<ItemCreator>().ItemGenerator(transform);
                Destroy(gameObject);
    
            }
          
        }
    }

    // -----------------------------------------------------------------
    // M�todo que detecta las colisiones de los enemigos con las trampas
    // -----------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si existe la colisi�n
        if (collision != null)
        {
            // Si la colisi�n es con una trampa
            if (collision.collider.CompareTag("Trap"))
            {
                AudioManager.instance.PlaySFX("EnemyDamage");
                Instantiate(Blood, transform.position, Quaternion.identity);
                // Se incrementa el n�mero de muertes
                GameObject.Find("GameManager").GetComponent<GameManager>().muertes++;
                // Se actualiza el texto
                GameObject.Find("GameManager").GetComponent<GameManager>().UpdateMuertes();
                // NO se llama al generador de objetos aleatorios porque si no es muy f�cil...
                // Se destruye el bicho
                Destroy(gameObject);
            }
        }
    }



    // -----------------------------------------------------------------------------
    // M�todo que desactiva todos los par�metros del Animator y activa uno concreto.
    // Hay que tener en cuenta que el componente Animator est� en el objeto hijo
    // y por eso se utiliza GetComponentInChildren<> en vez de GetComponent<>
    // -----------------------------------------------------------------------------
    public void SetAnimation(string name)
    {

        // Obtenemos todos los par�metros del Animator que est�n en el objeto hijo
        AnimatorControllerParameter[] parametros = this.GetComponentInChildren<Animator>().parameters;

        // Recorremos todos los par�metros y los ponemos a false
        foreach (var item in parametros) GetComponentInChildren<Animator>().SetBool(item.name, false);

        // Activamos el pasado por par�metro
        GetComponentInChildren<Animator>().SetBool(name, true);
    }


    // M�todo que devuelve True si el bicho detecta al jugador
    bool IsDetectingPlayer()
    {

        // Calculamos el vector direcci�n normalizado entre la posici�n actual y la posici�n del objetivo
        Vector2 direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;

        // Calculamos el punto final de la l�nea utilizando el vector direcci�n y la longitud deseada
        Vector2 endPoint = (Vector2)transform.position + direction * detectionDistance;

        // Dibujamos la l�nea RayCastHit2D teniendo en cuenta que no se solape el comienzo del collider propio
        Debug.DrawLine((Vector2)transform.position + direction * offsetDistance, endPoint, Color.black);

        // Generamos el RayCastHit2D coincidente con el DrawLine (los par�metros cambian un poco).
        // El primer par�metro es el origen + el offset, el segundo par�metro es la direcci�n
        // (en DrawLine era el punto de finalizaci�n), el tercer par�metro es la longitud del
        // rayo con el offset restado
        RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2)transform.position + direction *
                                            offsetDistance, direction, detectionDistance - offsetDistance);

        // Preguntamos si el RayCastHit2D est� colisionando
        if (raycastHit2D.collider != null)
        {
            if (raycastHit2D.collider.gameObject.CompareTag("Player"))
            {
                return true; // Como ha detectado al jugador, devuelve true
            }
        }
        // Devolvemos el valor por defecto si no detecta al jugador
        return false;
    }

    // M�todo para generar el collider del golpe y detectar la colisi�n
    public void Hit()
    {
        Collider2D collider = Physics2D.OverlapCircle(hitPoint.transform.position, circleAttackRadius);
        if (collider != null)
        {
            // Si el collider est� posicionado sobre el Player
            if (collider.CompareTag("Player"))
            {
                // Restamos vida al Player llamando al m�todo TakeDamage con el valor
                AudioManager.instance.PlaySFX("Damage");
                GameObject.Find("GameManager").GetComponent<GameManager>().TakeDamage(damageValue);
            }
        }
    }

    // M�todo que dibuja la forma del collider para que sea visible
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(hitPoint.transform.position, circleAttackRadius);
    }


    // Corrutina para atacar con delay
    public IEnumerator WaitAndAttack()
    {

        // Indica que la corrutina est� en ejecuci�n para bloquear
        // la posibilidad de que la misma instancia del prefab duplique
        // la corrutina
        executingCoroutine = true;

        // Pausamos el tiempo especificado en la variable attackDelay
        yield return new WaitForSeconds(attackDelay);

        // Restamos la vida
        Hit();

        // Restablece la variable de control para posibilitar
        // el lanzamiento de la corrutina otra vez desde el mismo prefab
        executingCoroutine = false;

    }






}


