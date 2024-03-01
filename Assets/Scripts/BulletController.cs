using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Cuando las balas salen de la escena se autodestruyen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Al generarse cada bala se incrementa la cuenta y se actualiza el texto
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().disparos++;
        GameObject.Find("GameManager").GetComponent<GameManager>().UpdateDisparos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {

                Destroy(gameObject);
            

        }
    }
}
