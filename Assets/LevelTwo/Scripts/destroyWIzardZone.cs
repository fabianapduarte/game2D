using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWIzardZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(GameObject.Find("wizardZoneInit"));
        }
    }
}
