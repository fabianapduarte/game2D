using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesScriptController : MonoBehaviour
{
    public void ClosedZone()
    {
        transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = false;
        transform.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public void DestroyZone()
    {
        Destroy(gameObject);
    }

}
