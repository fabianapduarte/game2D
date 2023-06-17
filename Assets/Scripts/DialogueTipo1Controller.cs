using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTipo1Controller : MonoBehaviour
{
    public GameObject dialogue;
    public Image iconNPC;
    public TextMeshProUGUI messageNPC;
    public TextMeshProUGUI nameNPC;

    private float speedWrite = 0.08f;
    private string[] messageSet;
    private int index = 0;
    //private int varTemp = 0;
    private int maxSize = "ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss".Length;

    public void Speech(Sprite icon, string[] message, string name)
    {
        dialogue.GetComponent<Animation>().Play("DialogueStart");
        dialogue.SetActive(true);
        iconNPC.sprite = icon;
        messageSet = message;
        nameNPC.text = name;
        StartCoroutine(WritingText(0));
    }

    IEnumerator WritingText(int startingPoint)
    {
        for (int jj = startingPoint; jj < messageSet[index].Length; jj++)
        {
            messageNPC.text += messageSet[index][jj];
            yield return new WaitForSeconds(speedWrite);
        }
    }

    public void NextSentence()
    {
        if(messageNPC.text == messageSet[index])
        {
            if(messageNPC.text.Length > maxSize)
            {
                messageNPC.text = "";
                StartCoroutine(WritingText(maxSize));
            }
            else
            {
                if(index < messageSet.Length - 1)
                {
                    index++;
                    messageNPC.text = "";
                    StartCoroutine(WritingText(0));
                }
                else
                {
                    index = 0;
                    messageNPC.text = "";
                    dialogue.GetComponent<Animation>().Play("DialogueEnd");
                    Invoke("Sleep", 0.4f);
                }
            }
        }
        /*else
        {
            messageNPC.text = "";
            while(varTemp != messageSet[index].Length-1) 
            { 
                messageNPC.text += messageSet[index][varTemp];
                varTemp++;
            }
        }*/
    }

    public void Sleep()
    {
        dialogue.SetActive(false);
        return;
    }

    public GameObject getDialogue()
    {
        return dialogue;
    }
}
