using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IrParaLevelOne : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            string levelOne = SceneUtility.GetScenePathByBuildIndex(3);
            SceneManager.LoadScene(levelOne);
        }
    }

}
