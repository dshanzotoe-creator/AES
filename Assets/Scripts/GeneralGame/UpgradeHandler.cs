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
        List<GameObject> abilities = new List<GameObject>(elementAbilities);
        int randomNumber = 0;

        if (!isUpgrading && IsThereAbilities())
        {
            PauseGame();

            int amountChoices = Mathf.Min(upgradeButtons.Length, abilities.Count);

            for (int j = 0; j < upgradeButtons.Length; j++)
            {
                upgradeButtons[j].SetActive(j < amountChoices);
            }

            for (int i = 0; i < amountChoices; i++)
            {
                randomNumber = Random.Range(0, abilities.Count);
                
                buttonAbilities[i] = abilities[randomNumber];
                upgradeButtons[i].name = abilities[randomNumber].name;
                abilities.RemoveAt(randomNumber);
            }

        }

    }

    public void ChooseUpgrade(int buttonNumber)
    {
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
