using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
   
    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] GameObject HPBar; // Para referenciar el relleno de la barra de vida

    // Variable p�blica para modificar desde cualquier script
    public float life = 100; // Vida que tiene el jugador en un momento determinado
    public float maxLife = 100; // Vida m�xima del jugador

    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] GameObject dialoguesObject;
    // Referencia al sprite que contiene la imagen para el cursor
    [SerializeField] Texture2D cursorTarget;
    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] TextMeshProUGUI textoDisparos;
    [SerializeField] TextMeshProUGUI textoMuertes;
    // Variable p�blica para modificar desde cualquier script
    public int disparos = 0;
    public int muertes = 0;
    // Variables privadas
    private bool canShoot = true;

    public float bugSpeed = 2;

    private Inventory inventario; // Para guardar la referencia al script del inventario
    [SerializeField] GameObject itemButton_1;
    [SerializeField] GameObject itemButton_2;

    private string totalTime="";




    private void Start()
    {
        // Configuramos el cursor con el sprite de la mirilla.
        // El segundo par�metro indica el punto efectivo del cursor (como la
        // imagen del cursor tiene 32x32 p�xeles, el centro estar� en 16, 16)
        // y el tercer par�metro indica el modo de renderizado del mismo
        // Configuramos el cursor con el sprite de la mirilla
        Vector2 hotspot = new Vector2(cursorTarget.width / 2, cursorTarget.height / 2);
        Cursor.SetCursor(cursorTarget, hotspot, CursorMode.Auto);

        // Actualizamos la informaci�n
        UpdateDisparos();
        UpdateMuertes();

        // Iniciamos la corrutina para aumentar la velocidad
        StartCoroutine(IncreaseSpeedCoroutine());

        // Obtenemos la referencia al script del Inventario que hay asociado al GameManager
        inventario = GetComponent<Inventory>();
    }

    // ---------------------------------------------
    // DETECCI�N DE CLICS DEL MOUSE SOBRE LOS �TEMS
    // ---------------------------------------------
    void Update()
    {

        // Actualizaci�n de la barra de vida
        HPBar.GetComponent<Image>().fillAmount = life / maxLife;

        // C�digo para probar que la barra de vida funciona
        if (Input.GetKey(KeyCode.UpArrow)) life = life + 0.25f;
        if (Input.GetKey(KeyCode.DownArrow)) life = life - 0.25f;

        // Verificar si se ha hecho clic con el bot�n izquierdo del rat�n
        if (Input.GetMouseButtonDown(0))
        {


            // Obtener la posici�n del clic en la pantalla
            Vector3 clicPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Crear un RaycastHit2D para almacenar la informaci�n de colisi�n 2D
            // Desde la posici�n en la que se hizo clic en la direcci�n 0, 0, 0
            // por lo tanto el Raycast ser� un diminuto punto que detecta la colisi�n
            RaycastHit2D hit = Physics2D.Raycast(clicPosition, Vector2.zero);

            // Verificar si ha golpeado un objeto
            if (hit.collider != null)
            {


                // Filtrar los objetos que interesan seg�n su etiqueta
                if (hit.collider.CompareTag("spikedball_item") && !dialoguesObject.activeSelf)
                {
                    Debug.Log("��BOLA CON PINCHOS!!");
                    AudioManager.instance.PlaySFX("Pickup");

                    // Verificamos si existen huecos libres en el inventario
                    for (int i = 0; i < inventario.slots.Length; i++)
                    {
                        if (!inventario.isFull[i])
                        { // Se pueden a�adir items
                            inventario.isFull[i] = true; // Ocupamos la posici�n
                                                         // Instanciamos un bot�n en la posici�n del slot
                            Instantiate(itemButton_1, inventario.slots[i].transform, false);
                            Destroy(hit.collider.gameObject); // Destruimos el objeto
                            DisableFire();
                            dialoguesObject.SetActive(true); // Activamos los di�logos
                            GameObject.Find("DialogPanel").GetComponent<dialogueController>().StartDialogue("spikedball_item");
                            break; // Salimos del bucle
                        }
                    }
                }
                if (hit.collider.CompareTag("sawblade_item") && !dialoguesObject.activeSelf)
                {
                    Debug.Log("��DISCO DE SIERRA!!");
                    AudioManager.instance.PlaySFX("Pickup");
                    
                    // Verificamos si existen huecos libres en el inventario
                    for (int i = 0; i < inventario.slots.Length; i++)
                    {
                        if (!inventario.isFull[i])
                        { // Se pueden a�adir items
                            inventario.isFull[i] = true; // Ocupamos la posici�n
                                                         // Instanciamos un bot�n en la posici�n del slot
                            Instantiate(itemButton_2, inventario.slots[i].transform, false);
                            Destroy(hit.collider.gameObject); // Destruimos el objeto
                            DisableFire();
                            dialoguesObject.SetActive(true); // Activamos los di�logos
                            GameObject.Find("DialogPanel").GetComponent<dialogueController>().StartDialogue("sawblade_item");
                            break; // Salimos del bucle
                        }
                    }
                }

                }
            }
    }



    // M�todo p�blico que actualiza el texto de los disparos
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
    // M�todo para desactivar los disparos
    // ---------------------------------------
    public void DisableFire()
    {
        canShoot = false;
    }

    // ---------------------------------------
    // M�todo para activar los disparos
    // ---------------------------------------
    public void EnableFire()
    {
        canShoot = true;
    }

    // -------------------------------------------------
    // M�todo para devolver si se puede disparar o no
    // -------------------------------------------------
    public bool GetShootingStatus()
    {
        return canShoot;
    }

    // M�todo para restar vida al Player
    public void TakeDamage(int damage)
    {
        life = Mathf.Clamp(life - damage, 0, maxLife);
        if(life == 0) {
            SetTotalTime(GameObject.Find("Timer").GetComponent<TMP_Text>().text);
            SceneController.instance.LoadScene("GameOver");
        } 
    }

    // M�todo para a�adir vida al Player
    public void Heal(int lifeRecovered)
    {
        life = life = Mathf.Clamp(life + lifeRecovered, 0, maxLife);
    }

   public IEnumerator IncreaseSpeedCoroutine()
    {
        while (true)
        {
            // Esperamos 5 segundos
            yield return new WaitForSeconds(5f);

            // Aumentamos la velocidad en 2 puntos
            bugSpeed += 0.5f;

            // **Limitar la velocidad m�xima**
            if (bugSpeed > 20f)
            {
                bugSpeed = 20f;
            }
        }
    }

    public void SetTotalTime(string time){
    this.totalTime = time;
    PlayerPrefs.SetString("TotalTime", totalTime);
    }



}
