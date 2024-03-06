using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Variables

    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] GameObject HPBar; // Para referenciar el relleno de la barra de vida

    // Variable pública para modificar desde cualquier script
    public float life = 100; // Vida que tiene el jugador en un momento determinado
    public float maxLife = 100; // Vida máxima del jugador

    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] GameObject dialoguesObject;
    // Referencia al sprite que contiene la imagen para el cursor
    [SerializeField] Texture2D cursorTarget;
    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] TextMeshProUGUI textoDisparos;
    [SerializeField] TextMeshProUGUI textoMuertes;
    // Variable pública para modificar desde cualquier script
    public int disparos = 0;
    public int muertes = 0;
    // Variables privadas
    private bool canShoot = true;

    public float bugSpeed = 2;

    private Inventory inventario; // Para guardar la referencia al script del inventario
    [SerializeField] GameObject itemButton_1;
    [SerializeField] GameObject itemButton_2;
    #endregion



    private void Start()
    {
        // Configuramos el cursor con el sprite de la mirilla.
        // El segundo parámetro indica el punto efectivo del cursor (como la
        // imagen del cursor tiene 32x32 píxeles, el centro estará en 16, 16)
        // y el tercer parámetro indica el modo de renderizado del mismo
        // Configuramos el cursor con el sprite de la mirilla
        Vector2 hotspot = new Vector2(cursorTarget.width / 2, cursorTarget.height / 2);
        Cursor.SetCursor(cursorTarget, hotspot, CursorMode.Auto);

        // Actualizamos la información
        UpdateDisparos();
        UpdateMuertes();

        // Iniciamos la corrutina para aumentar la velocidad
        StartCoroutine(IncreaseSpeedCoroutine());

        // Obtenemos la referencia al script del Inventario que hay asociado al GameManager
        inventario = GetComponent<Inventory>();
    }

    // ---------------------------------------------
    // DETECCIÓN DE CLICS DEL MOUSE SOBRE LOS ÍTEMS
    // ---------------------------------------------
    void Update()
    {

        // Actualización de la barra de vida
        HPBar.GetComponent<Image>().fillAmount = life / maxLife;

        // Código para probar que la barra de vida funciona
        if (Input.GetKey(KeyCode.UpArrow)) life = life + 0.25f;
        if (Input.GetKey(KeyCode.DownArrow)) life = life - 0.25f;

        // Verificar si se ha hecho clic con el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {


            // Obtener la posición del clic en la pantalla
            Vector3 clicPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Crear un RaycastHit2D para almacenar la información de colisión 2D
            // Desde la posición en la que se hizo clic en la dirección 0, 0, 0
            // por lo tanto el Raycast será un diminuto punto que detecta la colisión
            RaycastHit2D hit = Physics2D.Raycast(clicPosition, Vector2.zero);

            // Verificar si ha golpeado un objeto
            if (hit.collider != null)
            {


                // Filtrar los objetos que interesan según su etiqueta
                if (hit.collider.CompareTag("spikedball_item") && !dialoguesObject.activeSelf)
                {
                    Debug.Log("¡¡BOLA CON PINCHOS!!");
                    // Verificamos si existen huecos libres en el inventario
                    for (int i = 0; i < inventario.slots.Length; i++)
                    {
                        if (!inventario.isFull[i])
                        { // Se pueden añadir items
                            inventario.isFull[i] = true; // Ocupamos la posición
                                                         // Instanciamos un botón en la posición del slot
                            Instantiate(itemButton_1, inventario.slots[i].transform, false);
                            Destroy(hit.collider.gameObject); // Destruimos el objeto
                            DisableFire();
                            dialoguesObject.SetActive(true); // Activamos los diálogos
                            GameObject.Find("DialogPanel").GetComponent<dialogueController>().StartDialogue("spikedball_item");
                            break; // Salimos del bucle
                        }
                    }
                }
                if (hit.collider.CompareTag("sawblade_item") && !dialoguesObject.activeSelf)
                {
                    Debug.Log("¡¡DISCO DE SIERRA!!");
                    // Verificamos si existen huecos libres en el inventario
                    for (int i = 0; i < inventario.slots.Length; i++)
                    {
                        if (!inventario.isFull[i])
                        { // Se pueden añadir items
                            inventario.isFull[i] = true; // Ocupamos la posición
                                                         // Instanciamos un botón en la posición del slot
                            Instantiate(itemButton_2, inventario.slots[i].transform, false);
                            Destroy(hit.collider.gameObject); // Destruimos el objeto
                            DisableFire();
                            dialoguesObject.SetActive(true); // Activamos los diálogos
                            GameObject.Find("DialogPanel").GetComponent<dialogueController>().StartDialogue("sawblade_item");
                            break; // Salimos del bucle
                        }
                    }
                }

                }
            }
    }



    // Método público que actualiza el texto de los disparos
    public void UpdateDisparos()
    {
        // Actualizamos el texto de las balas disparadas
        textoDisparos.text = "Disparos: " + disparos;
    }
     public void UpdateMuertes()
    {
        // Actualizamos el texto de las balas disparadas
        textoMuertes.text = "Muertes: " + muertes;
    }

    // ---------------------------------------
    // Método para desactivar los disparos
    // ---------------------------------------
    public void DisableFire()
    {
        canShoot = false;
    }

    // ---------------------------------------
    // Método para activar los disparos
    // ---------------------------------------
    public void EnableFire()
    {
        canShoot = true;
    }

    // -------------------------------------------------
    // Método para devolver si se puede disparar o no
    // -------------------------------------------------
    public bool GetShootingStatus()
    {
        return canShoot;
    }

    // Método para restar vida al Player
    public void TakeDamage(int damage)
    {
        life = Mathf.Clamp(life - damage, 0, maxLife);
    }

    // Método para añadir vida al Player
    public void Heal(int lifeRecovered)
    {
        life = life = Mathf.Clamp(life + lifeRecovered, 0, maxLife); ;
    }

   public IEnumerator IncreaseSpeedCoroutine()
    {
        while (true)
        {
            // Esperamos 5 segundos
            yield return new WaitForSeconds(5f);

            // Aumentamos la velocidad en 2 puntos
            bugSpeed += 0.5f;

            // **Limitar la velocidad máxima**
            if (bugSpeed > 20f)
            {
                bugSpeed = 20f;
            }
        }
    }

}
