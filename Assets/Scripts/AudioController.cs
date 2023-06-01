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
    public AudioSource attackPlayer;
    public AudioSource runningPlayer;
    public AudioSource jumpPlayer;
    public AudioSource hurtPlayer;

    [Header("Menu")]
    public AudioSource selection;
    public AudioSource move;

    [Header("Collectibles")]
    public AudioSource collect;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name.Equals("Vitoria") || SceneManager.GetActiveScene().name.Equals("Derrota"))
        {
            if (SceneManager.GetActiveScene().name.Equals("Vitoria"))
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
                AudioClip teste = soundAmbiance[0];
                soundAmbianceLevel.clip = teste;
            } else if (SceneManager.GetActiveScene().name.Equals("MenuInicial"))
            {
                AudioClip teste = soundAmbiance[1];
                soundAmbianceLevel.clip = teste;
            }else if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
            {
                AudioClip teste = soundAmbiance[2];
                soundAmbianceLevel.clip = teste;
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
            {
                AudioClip teste = soundAmbiance[3];
                soundAmbianceLevel.clip = teste;
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
            {
                AudioClip teste = soundAmbiance[4];
                soundAmbianceLevel.clip = teste;
            }else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
            {
                AudioClip teste = soundAmbiance[5];
                soundAmbianceLevel.clip = teste;
            }else if (SceneManager.GetActiveScene().name.Equals("Cutscene1"))
            {
                AudioClip teste = soundAmbiance[6];
                soundAmbianceLevel.clip = teste;
            }
            soundAmbianceLevel.volume = 0.635f;
            soundAmbianceLevel.Play();
        }      
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
        runningPlayer.Pause();
    }

    public void Collect()
    {
        AudioClip teste = collectibles[0];
        collect.clip = teste;
        collect.Play();
    }

    public void HurtPlayer()
    {
        AudioClip teste = playerHurt[0];
        hurtPlayer.clip = teste;
        hurtPlayer.Play();
    }

    public void HurtEnemy()
    {
        AudioClip teste = enemyHurt[Random.Range(0, enemyHurt.Length)];
        hurtEnemy.clip = teste;
        hurtEnemy.Play();
    }

    public void BattleSoundStart()
    {
        AudioClip teste = soundAmbiance[7];
        soundAmbianceLevel.clip = teste;
        soundAmbianceLevel.volume = 0.7f;
        soundAmbianceLevel.Play();
    }

    public void BattleSoundFinish()
    {
        Invoke("Start", 1f);
    }
}