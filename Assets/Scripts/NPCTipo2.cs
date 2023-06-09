using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCTipo2 : MonoBehaviour
{
    public GameObject player;
    private DialogueTipo2Controller dc;
    public GameObject explosion;

    public string nameNPC;
    public string[] message;
    public Sprite icon;
    private bool startDialogue = true;
    // Start is called before the first frame update
    void Start()
    {
        dc = FindObjectOfType<DialogueTipo2Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 8f)
        {
            if (!SceneManager.GetActiveScene().name.Equals("Acidron2"))
            {
                if(startDialogue)
                {
                    player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    dc.Speech(icon, message, nameNPC);
                }
            }
        }
    }

    public void toCallDialogue(params int[] index)
    {
        dc.Speech(icon, message, nameNPC, index);
    }
    public void setDialogue(bool status)
    {
        startDialogue = status;
    }

    public void ExitNPC()
    {
        explosion.SetActive(true);
        gameObject.SetActive(false);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Invoke("Sleep", 0.8f);

    }

    public void Sleep()
    {
        explosion.SetActive(false);
    }
}
