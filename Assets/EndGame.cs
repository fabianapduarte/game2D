using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] messageSet;
    private float speedWrite = 0.08f;
    public TextMeshProUGUI messageNPC;

    private void Start()
    {
        StartCoroutine(WritingText());
    }

    IEnumerator WritingText()
    {
        for (int ii = 0; ii < messageSet.Length; ii++)
        {
            for (int jj = 0; jj < messageSet[ii].Length; jj++)
            {
                messageNPC.text += messageSet[ii][jj];
                yield return new WaitForSeconds(speedWrite);
            }
            yield return new WaitForSeconds(speedWrite + 1.3f);
            messageNPC.text = "";
        }
    }
}
