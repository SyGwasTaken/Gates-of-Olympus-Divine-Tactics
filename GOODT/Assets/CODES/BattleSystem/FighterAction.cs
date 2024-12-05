using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;

    [SerializeField]
    private GameObject attackPrefab;

    [SerializeField]
    private GameObject shieldPrefab;

    [SerializeField]
    private Sprite faceIcon;

    private GameObject currentAttack;
    private GameObject meleeAttack;
    private GameObject shieldAttack;

    public void SelectAttack(string btn)
    {
        GameObject victim = hero;
        if(tag == "Hero")
        {
            victim = enemy;
        }
        if(btn.CompareTo("melee") == 0)
        {
            meleeAttack.GetComponent<ActionScript>().Attack(victim);
        }
        else if(btn.CompareTo("shield") == 0)
        {
            shieldAttack.GetComponent<ActionScript>().Attack(victim);
        }
        else
        {
            Debug.Log("Run");
        }
    }
}
