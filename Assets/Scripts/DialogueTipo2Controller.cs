using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTipo2Controller : MonoBehaviour
{
    public GameObject dialogue;
    public Image iconNPC;
    public TextMeshProUGUI messageNPC;
    public TextMeshProUGUI nameNPC;

    private float speedWrite = 0.08f;
    private string[] messageSet;
    //private int varTemp = 0;
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

    IEnumerator WritingText()
    {
        for(int ii = 0; ii < messageSet.Length; ii++)
        {
            if (ii == 0)
            {

            }
            for (int jj = 0; jj < messageSet[ii].Length; jj++)
            {
                messageNPC.text += messageSet[ii][jj];
                yield return new WaitForSeconds(speedWrite);
            }
            yield return new WaitForSeconds(speedWrite+1.3f);
            messageNPC.text = "";
        }
        dialogue.GetComponent<Animation>().Play("DialogueEnd");
        FindObjectOfType<NPCTipo2>().ExitNPC();
    }
}
