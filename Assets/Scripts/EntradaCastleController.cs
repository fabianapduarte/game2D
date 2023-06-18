using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaCastleController : MonoBehaviour
{
    public GameObject envinronmentsExtern;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && Mathf.Abs(GameObject.Find("Simetra").transform.position.x) > Mathf.Abs(transform.position.x))
        {
            envinronmentsExtern.SetActive(false);
        }
        else
        {
            envinronmentsExtern.SetActive(true);
        }
    }
}
