using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    // Start is called before the first frame update
    public string nameNPC;
    public string[] message;
    public Sprite icon;
    public GameObject dialogueHUD;
    public LayerMask playerLayer;
    private DialogueController dc;
    private bool onArea;

    void Start()
    {
        dc = FindObjectOfType<DialogueController>();
    }

    private void FixedUpdate()
    {
        DetectionPlayer();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && onArea)
        {
            dc.Speech(icon, message, nameNPC);
        }
        
    }

    public void DetectionPlayer()
    {
        Collider2D col = Physics2D.OverlapCircle(new Vector2(transform.position.x+0.3f, transform.position.y + 1.3f), 2.3f, playerLayer);

        if (col != null)
        {
            onArea = true;
            if (dc.getDialogue().activeSelf)
            {
                dialogueHUD.SetActive(false);
            }
            else
            {
                dialogueHUD.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animation>().Play("Arrow");
                dialogueHUD.SetActive(true);
                
            }
        }
        else
        {
            onArea = false;
            dialogueHUD.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x+0.3f, transform.position.y + 1.3f), 2.3f);
    }
}
