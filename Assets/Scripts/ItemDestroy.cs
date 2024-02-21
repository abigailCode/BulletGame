using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    public int timeToDestroy = 2;

    private void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DestroyDelay()
    {
        
        yield return new WaitForSeconds(timeToDestroy);
        DestroyItem();
    }
}
