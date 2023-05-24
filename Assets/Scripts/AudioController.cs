using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioClip[] jump;
    public AudioClip[] attack;
    public AudioClip[] running;
    public AudioClip[] soundAmbiance;
    public AudioClip[] selectInMenu;
    public AudioClip[] MoveInMenu;

    public AudioSource soundAmbianceLevel;
    public AudioSource attackPlayer;
    public AudioSource runningPlayer;
    public AudioSource jumpPlayer;
    public AudioSource selection;
    public AudioSource move;

    public static AudioController instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if (SceneManager.GetActiveScene().name.Equals("MenuInicial"))
        {
            AudioClip teste = soundAmbiance[0];
            soundAmbianceLevel.clip = teste;
        }else if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            AudioClip teste = soundAmbiance[1];
            soundAmbianceLevel.clip = teste;
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            AudioClip teste = soundAmbiance[2];
            soundAmbianceLevel.clip = teste;
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            AudioClip teste = soundAmbiance[3];
            soundAmbianceLevel.clip = teste;
        }else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            AudioClip teste = soundAmbiance[4];
            soundAmbianceLevel.clip = teste;
        }
        soundAmbianceLevel.volume = 0.635f;
        soundAmbianceLevel.Play();
        
    }

    public void Attack()
    {
        AudioClip teste = attack[Random.Range(0, attack.Length)];
        attackPlayer.clip = teste;
        attackPlayer.Play();
    }

    public void Jump()
    {
        AudioClip teste = jump[Random.Range(0, jump.Length)];
        jumpPlayer.clip = teste;
        jumpPlayer.Play();
    }

    public void RunningPlay()
    {
        AudioClip teste = running[0];
        runningPlayer.clip = teste;
        runningPlayer.Play();
    }

    public void MoveButton()
    {
        AudioClip teste = MoveInMenu[0];
        move.clip = teste;
        move.Play();
    }

    public void Click()
    {
        AudioClip teste = selectInMenu[0];
        selection.clip = teste;
        selection.Play();
    }

    public void ControlaVolume(float volume)
    {
        AudioListener.volume = volume;
        //Falta a parte matematica
        /*soundAmbianceLevel.volume = volume;
        attackPlayer.volume = volume;
        runningPlayer.volume = volume;
        jumpPlayer.volume = volume;*/
    }

    public void RunningBreak()
    {
        runningPlayer.Pause();
    }
}
