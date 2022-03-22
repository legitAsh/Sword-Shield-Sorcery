using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region Audio
    public AudioClip loseBGM;
    public AudioClip shieldBash;
    public AudioClip Fireball;
    public AudioClip Swoosh;
    #endregion

    #region Health
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int health = 3;
    public int numOfHealth = 3;
    #endregion

    #region Gameplay
    public int playerChoose = -1;   //int that makes sure none of the choices are selected
    public bool playersTurn = false;
    bool ScannedCard;   //Boolean to see if player card has been scanned yet, if yes, wait 5 seconds to spawn confirmation panel

    public Transform SwordPanel;
    public Transform ShieldPanel;
    public Transform SorceryPanel;

    public GameObject DrawScreen;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    #endregion

    #region References
    AudioManager AM;
    Animator anim;
    EnemyManager enemy;
    #endregion

    #region Particles
    public GameObject BloodSplurt;
    public GameObject Dust;
    public GameObject Explosion;
    #endregion

    public enum PlayerCards {
        SwordCard = 0,
        ShieldCard,
        SorceryCard
    }

    private void Start() {
        enemy = FindObjectOfType<EnemyManager>();
        anim = FindObjectOfType<Animator>();
        AM = FindObjectOfType<AudioManager>();
    }

    public void ChooseCard(int choose) {    //scanning a player card that corresponds with an int that determines choice
        if (!playersTurn) { 
            playerChoose = choose;
            playersTurn = true;
            BotChoose();
        }
    }

    public void BotChoose() {
        StartCoroutine(enemy.ChooseRandom());
        StartCoroutine(checkOutcome(3));    //plays outcome animation and code after enemy animation(average : 3s)
    }
    
    public void ResetTurn() {
        ScannedCard = false;
        if (playersTurn) {
            playerChoose = -1;
            playersTurn = false;
        }
        Explosion.gameObject.SetActive(false);
        BloodSplurt.gameObject.SetActive(false);
        Dust.gameObject.SetActive(false);
        DrawScreen.gameObject.SetActive(false);
    }

    public void ShowPanel(int panelType) {
        switch (panelType) {
            case 0:
                if (!ScannedCard) {
                    SwordAlive(5);
                }
                break;
            case 1:
                if (!ScannedCard) {
                    ShieldAlive(5);
                }
                break;
            case 2:
                if (!ScannedCard) {
                    SorceryAlive(5);
                }
                break;
        }
    }


    void SwordAlive(int duration) {
        StartCoroutine(SwordOn(duration));
        ScannedCard = true;
    }

    void ShieldAlive(int duration) {
        StartCoroutine(ShieldOn(duration));
        ScannedCard = true;
    }

    void SorceryAlive(int duration) {
        StartCoroutine(SorceryOn(duration));
        ScannedCard = true;
    }



    IEnumerator SwordOn(int duration) {
        yield return new WaitForSeconds(duration);
        SwordPanel.gameObject.SetActive(true);
    }

    IEnumerator ShieldOn(int duration) {
        yield return new WaitForSeconds(duration);
        ShieldPanel.gameObject.SetActive(true);
    }

    IEnumerator SorceryOn(int duration) {
        yield return new WaitForSeconds(duration);
        SorceryPanel.gameObject.SetActive(true);
    }



    IEnumerator checkOutcome(int waitforanim) {
        yield return new WaitForSeconds(waitforanim);

        if (playerChoose == enemy.randomType) { //if player and enemy picks same, spawns draw panel and resets turn
            DrawScreen.gameObject.SetActive(true);
            ResetTurn();
        }
        else if (playerChoose == (int)PlayerCards.ShieldCard && (enemy.randomType == 0)) {  //if player picks shield and enemy picks sword
            AM.BGM.PlayOneShot(shieldBash);
            enemy.Damage(1);
            BloodSplurt.gameObject.SetActive(true);
            ResetTurn();
        }
        else if (playerChoose == (int)PlayerCards.SorceryCard && (enemy.randomType == 1)) { //if player picks sorcery and enemy picks shield
            Dust.gameObject.SetActive(true); 
            enemy.Damage(1);
            ResetTurn();
        }
        else if (playerChoose == (int)PlayerCards.SwordCard && (enemy.randomType == 2)) {   //if player picks sword and enemy picks sorcery
            AM.BGM.PlayOneShot(Fireball);
            Explosion.gameObject.SetActive(true);
            AM.BGM.PlayOneShot(Swoosh);
            enemy.Damage(1);
            BloodSplurt.gameObject.SetActive(true);
            ResetTurn();
        }
        else if (playerChoose == (int)PlayerCards.SorceryCard && (enemy.randomType == 0)) { //if player picks sorcery and enemy picks sword
            AM.BGM.PlayOneShot(Swoosh);
            PlayerDamaged(1);
            ResetTurn();
        }
        else if (playerChoose == (int)PlayerCards.SwordCard && (enemy.randomType == 1)) {   //if player picks sword and enemy picks shield
            AM.BGM.PlayOneShot(shieldBash);
            Dust.gameObject.SetActive(true);
            PlayerDamaged(1);
            ResetTurn();
        }
        else if (playerChoose == (int)PlayerCards.ShieldCard && (enemy.randomType == 2)) {  //if player picks shield and enemy picks sorcery
            AM.BGM.PlayOneShot(Fireball);
            Explosion.gameObject.SetActive(true);
            PlayerDamaged(1);
            ResetTurn();
        }
    }

    public void PlayerDamaged(int amount) { 
        health -= amount;
        UpdateHealth();

        if(health == 0) {
            AM.ChangeBGM(loseBGM);
            LoseScreen.gameObject.SetActive(true);  //lose screen spawns in
            Invoking(); //resets game and brings player to menu
        }
    }

    void UpdateHealth() {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Invoking() {
        ScannedCard = true;
        Invoke("EndGame", 7f);
    }

    public void EndGame() {
        SceneManager.LoadScene("Menu");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void LoadLevel() {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame() {
        Application.Quit();
    }

}