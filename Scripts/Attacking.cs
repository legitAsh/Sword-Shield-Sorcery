using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
  
    public GameObject SwordAttack;
    public GameObject ShieldAttack;
    public GameObject SorceryAttack;

    void Start(){
       
    }

    public void CardAtk(int whichCard) {
        switch (whichCard) {
            case 0:
                GameObject newSword = Instantiate(SwordAttack, transform.position, transform.rotation);
                break;
            case 1:
                GameObject newShield = Instantiate(ShieldAttack, transform.position, transform.rotation);
                break;
            case 2:
                GameObject newSorcery = Instantiate(SorceryAttack, transform.position, transform.rotation);
                break;
        }
    }
}
