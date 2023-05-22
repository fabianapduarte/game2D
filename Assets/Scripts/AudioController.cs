using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] jump;
    public AudioClip[] attack;
    public AudioClip[] running;
    public AudioClip[] soundAmbiance;

    public AudioSource levelOne;
    public AudioSource attackPlayer;
    public AudioSource runningPlayer;
    public AudioSource jumpPlayer;

    // Start is called before the first frame update
    void Start()
    {
        AudioClip teste = soundAmbiance[0];
        levelOne.clip = teste;
        levelOne.Play();
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
}
