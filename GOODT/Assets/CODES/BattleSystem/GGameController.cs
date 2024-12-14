using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GGameController : MonoBehaviour
{
    private List<FighterStats> fighterStats;

    [Tooltip("Reference to the Action Menu GameObject")]
    public GameObject battleMenu;

    [Tooltip("Reference to the TextMeshProUGUI component for battle text")]
    public TextMeshProUGUI battleText;

    private void Awake()
    {
        if (battleMenu == null)
        {
            Debug.LogError("ActionMenu not assigned!");
        }
        if (battleText == null)
        {
            Debug.LogError("BattleText not assigned!");
        }
    }

    void Start()
    {
        fighterStats = new List<FighterStats>();

        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero == null)
        {
            Debug.LogError("Hero not found!");
        }
        else
        {
            FighterStats currentFighterStats = hero.GetComponent<FighterStats>();
            if (currentFighterStats == null)
            {
                Debug.LogError("FighterStats component not found on Hero!");
            }
            else
            {
                currentFighterStats.CalculateNextTurn(0);
                fighterStats.Add(currentFighterStats);
            }
        }

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy == null)
        {
            Debug.LogError("Enemy not found!");
        }
        else
        {
            FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
            if (currentEnemyStats == null)
            {
                Debug.LogError("FighterStats component not found on Enemy!");
            }
            else
            {
                currentEnemyStats.CalculateNextTurn(0);
                fighterStats.Add(currentEnemyStats);
            }
        }

        if (fighterStats.Count > 0)
        {
            fighterStats.Sort();
            NextTurn();
        }
    }

    public void NextTurn()
    {
        if (battleText != null)
        {
            battleText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("BattleText is not assigned!");
        }

        if (fighterStats.Count > 0)
        {
            FighterStats currentFighterStats = fighterStats[0];
            fighterStats.Remove(currentFighterStats);
            if (!currentFighterStats.GetDead())
            {
                GameObject currentUnit = currentFighterStats.gameObject;
                currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
                fighterStats.Add(currentFighterStats);
                fighterStats.Sort();

                if (currentUnit.tag == "Hero")
                {
                    if (battleMenu != null)
                    {
                        this.battleMenu.SetActive(true);
                    }
                    else
                    {
                        Debug.LogError("BattleMenu is not assigned!");
                    }
                    // Update the battle text here
                    UpdateBattleText("Hero's turn!");
                }
                else
                {
                    if (battleMenu != null)
                    {
                        this.battleMenu.SetActive(false);
                    }

                    // Always use melee attack for enemy
                    FighterAction action = currentUnit.GetComponent<FighterAction>();
                    if (action != null)
                    {
                        action.SelectAttack("melee");
                        // Update the battle text here
                        UpdateBattleText("Enemy attacks with melee!");
                    }
                    else
                    {
                        Debug.LogError("FighterAction component not found on " + currentUnit.name);
                    }
                }
            }
            else
            {
                NextTurn();
            }
        }
        else
        {
            Debug.LogError("No fighters in the fighterStats list!");
        }
    }

    private void UpdateBattleText(string text)
    {
        if (battleText != null)
        {
            battleText.gameObject.SetActive(true);
            battleText.text = text;
        }
        else
        {
            Debug.LogError("BattleText is not assigned!");
        }
    }
}
