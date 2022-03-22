using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public int randomType;

    #region Health
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int health = 3;
    public int numOfHealth = 3;
    #endregion

    #region References
    AudioManager AM;
    Animator anim;
    LevelManager level;
    #endregion

    #region EnemySFX
    public AudioClip death;
    public AudioClip grunt;
    public AudioClip winBGM;
    #endregion


    void Start(){
        level = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
        AM = FindObjectOfType<AudioManager>();
    }


    public IEnumerator ChooseRandom() {
        randomType = Random.Range(0, 3);    //picks a random int which corresponds with sword, shield or sorcery

        switch (randomType) {
            case 0:
                anim.Play("KnightSword");   //plays animations for enemy choices
                break;
            case 1:
                anim.Play("KnightShield");
                break;
            case 2:
                anim.Play("KnightFireball");
                break;
        }
        yield return null;
    }

    public void Damage(int amount) {
        anim.Play("KnightDamaged");     //enemy animation and sound when damaged
        AM.BGM.PlayOneShot(grunt);

        health -= amount;
        UpdateEnemyHealth();        //updates health

        if(health == 0) {
            level.WinScreen.gameObject.SetActive(true);

            AM.ChangeBGM(winBGM);
            anim.Play("KnightDeath");   //if enemy health = 0, win screen comes up, enemy feedbacks
            AM.BGM.PlayOneShot(death);

            level.Invoking();   //restarts game and brings player to menu screen
        }
    }

    void UpdateEnemyHealth() {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
