using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueTipo2Controller : MonoBehaviour
{
    public GameObject dialogue;
    public Image iconNPC;
    public TextMeshProUGUI messageNPC;
    public TextMeshProUGUI nameNPC;

    private float speedWrite = 0.08f;
    private string[] messageSet;
    private bool startDialogue = true;
    private int maxSize = "ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss".Length;

    public void Speech(Sprite icon, string[] message, string name)
    {
        FindObjectOfType<NPCTipo2>().setDialogue(false);
        dialogue.transform.GetChild(3).gameObject.SetActive(false);
        dialogue.GetComponent<Animation>().Play("DialogueStart");
        dialogue.SetActive(true);
        iconNPC.sprite = icon;
        messageSet = message;
        nameNPC.text = name;
        StartCoroutine(WritingText());
    }


    public bool GetStart()
    {
        return startDialogue;
    }

    public void Speech(Sprite icon, string[] message, string name, params int[] index)
    {
        startDialogue = false;
        dialogue.transform.GetChild(3).gameObject.SetActive(false);
        dialogue.GetComponent<Animation>().Play("DialogueStart");
        dialogue.SetActive(true);
        iconNPC.sprite = icon;
        messageSet = message;
        nameNPC.text = name;
        StartCoroutine(WritingText(index));
    }

    IEnumerator WritingText()
    {
        for(int ii = 0; ii < messageSet.Length; ii++)
        {
            for (int jj = 0; jj < messageSet[ii].Length; jj++)
            {
                messageNPC.text += messageSet[ii][jj];
                yield return new WaitForSeconds(speedWrite);
            }
            yield return new WaitForSeconds(speedWrite+1.3f);
            messageNPC.text = "";
        }
        dialogue.GetComponent<Animation>().Play("DialogueEnd");
        dialogue.SetActive(false);
        GameObject.Find("Simetra").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    IEnumerator WritingText(int[] index)
    {
        for(int ii=0; ii < index.Length; ii++)
        {
            for (int jj = 0; jj < messageSet[ii].Length; jj++)
            {
                messageNPC.text += messageSet[ii][jj];
                yield return new WaitForSeconds(speedWrite);
            }
            yield return new WaitForSeconds(speedWrite + 1.3f);
            messageNPC.text = "";
        }
        dialogue.GetComponent<Animation>().Play("DialogueEnd");
        if(nameNPC.text.Equals("Rainha Lechna"))
        {
            GameObject.Find("Queen").GetComponent<ConstantForce2D>().relativeForce = new Vector2(-0.25f, 0f);
        }
        startDialogue = true;
    }
}
