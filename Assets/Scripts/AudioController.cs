using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioClip[] jump;
    public AudioClip[] attack;
    public AudioClip[] running;
    public AudioClip[] soundAmbiance;
    public AudioClip[] selectInMenu;
    public AudioClip[] moveInMenu;
    public AudioClip[] collectibles;
    public AudioClip[] finishLevel;

    public AudioClip[] enemyHurt;
    public AudioClip[] playerHurt;


    [Header("Ambiance")]
    public AudioSource soundAmbianceLevel;

    [Header("FinishLevel")]
    public AudioSource endLevel;

    [Header("Enemy")]
    public AudioSource hurtEnemy;

    [Header("Player")]
    public GameObject Audios;

    [Header("Menu")]
    public AudioSource selection;
    public AudioSource move;

    [Header("Collectibles")]
    public AudioSource collect;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name.Equals("Vitoria") || SceneManager.GetActiveScene().name.Equals("Derrota") || SceneManager.GetActiveScene().name.Equals("FimDeJogo"))
        {
            if (SceneManager.GetActiveScene().name.Equals("Vitoria") || SceneManager.GetActiveScene().name.Equals("FimDeJogo"))
            {
                AudioClip teste = finishLevel[0];
                endLevel.clip = teste;
            }

            if (SceneManager.GetActiveScene().name.Equals("Derrota"))
            {
                AudioClip teste = finishLevel[1];
                endLevel.clip = teste;
            }
            endLevel.volume = 0.635f;
            endLevel.Play();
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
            {
                AudioClip teste = soundAmbiance[1];
                soundAmbianceLevel.clip = teste;
            } else if (SceneManager.GetActiveScene().name.Equals("MenuInicial"))
            {
                AudioClip teste = soundAmbiance[0];
                soundAmbianceLevel.clip = teste;
            }else if (SceneManager.GetActiveScene().name.Equals("LevelOne") || SceneManager.GetActiveScene().name.Equals("MLevelOne"))
            {
                AudioClip teste = soundAmbiance[2];
                soundAmbianceLevel.clip = teste;
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelTwo") || SceneManager.GetActiveScene().name.Equals("MLevelTwo"))
            {
                AudioClip teste = soundAmbiance[3];
                soundAmbianceLevel.clip = teste;
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelTree") || SceneManager.GetActiveScene().name.Equals("MLevelTree"))
            {
                AudioClip teste = soundAmbiance[4];
                soundAmbianceLevel.clip = teste;
            }else if (SceneManager.GetActiveScene().name.Equals("LevelFour") || SceneManager.GetActiveScene().name.Equals("MLevelFour"))
            {
                AudioClip teste = soundAmbiance[5];
                soundAmbianceLevel.clip = teste;
            }
            else if (SceneManager.GetActiveScene().name.Equals("Acidron1") || SceneManager.GetActiveScene().name.Equals("LevelFive") || SceneManager.GetActiveScene().name.Equals("MLevelFive"))
            {
                AudioClip teste = soundAmbiance[6];
                soundAmbianceLevel.clip = teste;
            }
            else if (SceneManager.GetActiveScene().name.Equals("Cutscene1") || SceneManager.GetActiveScene().name.Equals("Cutscene2") || SceneManager.GetActiveScene().name.Equals("FimDeJogo"))
            {
                AudioClip teste = soundAmbiance[8];
                soundAmbianceLevel.clip = teste;
            }else if (SceneManager.GetActiveScene().name.Equals("Acidron2"))
            {
                AudioClip teste = soundAmbiance[7];
                soundAmbianceLevel.clip = teste;
            }
            soundAmbianceLevel.volume = 0.635f;
            soundAmbianceLevel.Play();
        }      
    }

    public void Attack1()
    {
        Audios.transform.GetChild(2).GetComponent<AudioSource>().PlayOneShot(attack[Random.Range(0, attack.Length - 3)]);
    }
    public void Attack2()
    {
        Audios.transform.GetChild(2).GetComponent<AudioSource>().PlayOneShot(attack[2]);
    }

    public void AttackCombo()
    {
        Audios.transform.GetChild(2).GetComponent<AudioSource>().PlayOneShot(attack[3]);
    }

    public void AttackBlock()
    {
        Audios.transform.GetChild(2).GetComponent<AudioSource>().PlayOneShot(attack[4]);
    }

    public void Jump()
    {
        Audios.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(jump[Random.Range(0, jump.Length)]);
    }

    public void RunningPlay()
    {
        if(!Audios.transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
        {
            Audios.transform.GetChild(0).GetComponent<AudioSource>().clip = running[Random.Range(0, running.Length)];
            Audios.transform.GetChild(0).GetComponent<AudioSource>().volume = 0.5f;
            Audios.transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }

    public void MoveButton()
    {
        AudioClip teste = moveInMenu[0];
        move.clip = teste;
        move.Play();
    }

    public void Click()
    {
        AudioClip teste = selectInMenu[0];
        selection.clip = teste;
        selection.Play();
    }

    public float GetVolume()
    {
        return AudioListener.volume;
    }

    public void ControlaVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void RunningBreak()
    {
        if (Audios.transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
        {
            Audios.transform.GetChild(0).GetComponent<AudioSource>().Stop();
        }
    }

    public void Collect()
    {
        AudioClip teste = collectibles[0];
        collect.clip = teste;
        collect.Play();
    }

    public void HurtPlayer()
    {
        Audios.transform.GetChild(3).GetComponent<AudioSource>().PlayOneShot(playerHurt[0]);
    }

    public void HurtEnemy()
    {
        AudioClip teste = enemyHurt[Random.Range(0, enemyHurt.Length)];
        hurtEnemy.clip = teste;
        hurtEnemy.Play();
    }

    public void BattleSoundStart(bool boss)
    {
        AudioClip teste;
        if (boss)
        {
            teste = soundAmbiance[10];
        }
        else
        {
            teste = soundAmbiance[9];
        }
        soundAmbianceLevel.clip = teste;
        soundAmbianceLevel.volume = 0.7f;
        soundAmbianceLevel.Play();
    }

    public void BattleSoundFinish()
    {
        Invoke("Start", 1f);
    }
}
