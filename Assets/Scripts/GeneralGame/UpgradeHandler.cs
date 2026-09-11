using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField] PlayerElementHandler pEH;

    [SerializeField] List<GameObject> elementAbilities = new List<GameObject>();

    public bool isUpgrading = false;
    [SerializeField] GameObject[] upgradeButtons = new GameObject[3];

    GameObject[] buttonAbilities = new GameObject[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetElementalAbilities();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GetElementalAbilities()     //I feel like a genius doing basic ass shit without AI loooooooool
    {
        pEH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerElementHandler>();

        foreach(ElemntData element in pEH._chosenElements)
        {
            for (int i = 0; i < element.abillities.Length; i++)
            {
                elementAbilities.Add(element.abillities[i]);
            }
           
        }
    }

    public void ActivateButtonLogic()
    {
        //Make it so that the game pauses when the player levels up and gets to choose their ability
        List<GameObject> abilities = elementAbilities;
        int randomNumber = 0;

        if (!isUpgrading && IsThereAbilities())
        {
            PauseGame();

            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                GameObject button = upgradeButtons[i];

                button.SetActive(true);

                for(int j = elementAbilities.Count; j < upgradeButtons.Length; j++)
                {
                    upgradeButtons[j].SetActive(false);
                }

                randomNumber = Random.Range(0, abilities.Count);
                
                buttonAbilities[i] = elementAbilities[randomNumber];
                button.name = elementAbilities[randomNumber].name;
                abilities.RemoveAt(randomNumber);
            }

        }

    }

    public void ChooseUpgrade(int buttonNumber)
    {
        //Make it so that when whatever button is pressed, the player gets that upgrade. 
        //IF THE UPGRADE IS AN ABILITY remove the ability from the elemental abilities list. 
        GameObject ability = buttonAbilities[buttonNumber];
        pEH.abilities.Add(ability);
        elementAbilities.Remove(ability);

        ResumeGame();

    }


    private void PauseGame()
    {
        Debug.Log("Pasuing Game");
        Time.timeScale = 0f;
        isUpgrading = true; 
    } 

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isUpgrading = false;

        foreach (GameObject button in upgradeButtons)
        {
            button.SetActive(false);
        }
    }


    bool IsThereAbilities()
    {
        return elementAbilities.Count > 0;
    }
}
