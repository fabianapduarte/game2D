using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySeguraCogumelo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(GameObject.Find("seguraCogumelo"));
        }
    }
}
