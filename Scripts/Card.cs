using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardType {
        Sword,
        Shield,
        Sorcery
    }

    AudioManager AM;
    LevelManager lvl;
    Rigidbody rb;

    public float projectileSpeed;
    public CardType cardType;

    void Start() {
        rb = GetComponent<Rigidbody>();
        lvl = FindObjectOfType<LevelManager>();
        AM = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        rb.AddForce(Vector3.forward * projectileSpeed);    
    }


    void CollisionParticle(CardType _CardType) {
        switch (_CardType) {
            case CardType.Sword:
                Destroy(gameObject);
                break;
            case CardType.Shield:
                Destroy(gameObject);
                break;
            case CardType.Sorcery:
                AM.BGM.PlayOneShot(lvl.Fireball);
                lvl.Explosion.gameObject.SetActive(true);
                Destroy(gameObject);
                break;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Knight")) {
            CollisionParticle(cardType);
        }
    }
}
