using UnityEngine;

public class ItemActions : MonoBehaviour
{

    // Referencia p�blica al prefab de la trampa con la bola de pinchos
    public GameObject spikedballTrap;

    // Referencia p�blica al prefab de la trampa con las sierras
    public GameObject sawTrap;

    // M�todo para spawnear la bola con pinchos
    public void SpawnSpikedball()
    {
        // Controlamos que no se instancie la trampa si ya hay una activa en pantalla
        // OJO al nombre que le hayas puesto a la etiqueta de tu prefab
        if (GameObject.FindGameObjectWithTag("SpikedballTrap") == null)
        {
            Instantiate(spikedballTrap);
            AudioManager.instance.PlaySFX("Trap");
            Destroy(this.gameObject); // Destruimos el bot�n
        }
    }

    // M�todo para spawnear la trampa de sierras
    public void SpawnSaw()
    {
        // Controlamos que no se instancie la trampa si ya hay una activa en pantalla
        // OJO al nombre que le hayas puesto a la etiqueta de tu prefab
        if (GameObject.FindGameObjectWithTag("SawTrap") == null)
        {
            Instantiate(sawTrap);
            AudioManager.instance.PlaySFX("Trap");
            Destroy(this.gameObject); // Destruimos el bot�n
        }
    }
}
