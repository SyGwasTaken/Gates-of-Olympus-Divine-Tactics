using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private void Start()
    {
        // Assign hero and enemy dynamically or verify they are assigned
        hero = GameObject.FindWithTag("Hero");
        enemy = GameObject.FindWithTag("Enemy");

        if (hero == null || enemy == null)
        {
            Debug.LogError("Hero or Enemy is not assigned. Check your tags and scene setup.");
        }

        // Example of instantiating attacks (if needed)
        meleeAttack = Instantiate(attackPrefab);
        shieldAttack = Instantiate(shieldPrefab);
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = (tag == "Hero") ? enemy : hero;

        if (victim == null)
        {
            Debug.LogError("Victim is null. Ensure hero or enemy is assigned or instantiated.");
            return;
        }

        if (btn.CompareTo("melee") == 0)
        {
            if (meleeAttack != null)
            {
                meleeAttack.GetComponent<ActionScript>().Attack(victim);
            }
            else
            {
                Debug.LogError("meleeAttack is null. Ensure it's properly initialized.");
            }
        }
        else if (btn.CompareTo("shield") == 0)
        {
            if (shieldAttack != null)
            {
                shieldAttack.GetComponent<ActionScript>().Attack(victim);
            }
            else
            {
                Debug.LogError("shieldAttack is null. Ensure it's properly initialized.");
            }
        }
        else
        {
            Debug.Log("Run");
        }
    }
}

