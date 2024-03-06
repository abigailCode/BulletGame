using UnityEngine;

public class ItemActions : MonoBehaviour
{

    // Referencia pública al prefab de la trampa con la bola de pinchos
    public GameObject spikedballTrap;

    // Referencia pública al prefab de la trampa con las sierras
    public GameObject sawTrap;

    // Método para spawnear la bola con pinchos
    public void SpawnSpikedball()
    {
        // Controlamos que no se instancie la trampa si ya hay una activa en pantalla
        // OJO al nombre que le hayas puesto a la etiqueta de tu prefab
        if (GameObject.FindGameObjectWithTag("SpikedballTrap") == null)
        {
            Instantiate(spikedballTrap);
            Destroy(this.gameObject); // Destruimos el botón
        }
    }

    // Método para spawnear la trampa de sierras
    public void SpawnSaw()
    {
        // Controlamos que no se instancie la trampa si ya hay una activa en pantalla
        // OJO al nombre que le hayas puesto a la etiqueta de tu prefab
        if (GameObject.FindGameObjectWithTag("SawTrap") == null)
        {
            Instantiate(sawTrap);
            Destroy(this.gameObject); // Destruimos el botón
        }
    }
}
