using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField] PlayerElementHandler pEH; 

    [SerializeField]  List<GameObject> elementAbilities = new List<GameObject>();

    public bool isUpgrading = false; 
    [SerializeField] GameObject[] upgradeButtons = new GameObject[3];



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
        List<int> storedNumbers = new List<int>();
        int randomNumber = 0;

        if (!isUpgrading)
        {
            PauseGame();

            foreach (GameObject button in upgradeButtons)
            {
                button.SetActive(true);
                if (IsThereAbilities())
                {
                 restart:
                    randomNumber = Random.Range(0, elementAbilities.Count);
                    Debug.Log(randomNumber);

                    if (storedNumbers.Count != 0)
                    {
                        foreach (int number in storedNumbers)
                        {
                            if (number == randomNumber)
                            {
                                Debug.Log("Dup detected");
                                goto restart; 
                            }
                                
                        }
                    }

                    button.name = elementAbilities[randomNumber].name;
                }

                storedNumbers.Add(randomNumber);
            } 
        }

    }

    public void ChooseUpgrade()
    {
        //Make it so that when whatever button is pressed, the player gets that upgrade. 
        //IF THE UPGRADE IS AN ABILITY remove the ability from the elemental abilities list. 
       

    }

    void RemoveElementalability(GameObject ability)
    {
        //It's in the name. Write the logic to remove whatever ability was chosen. 
    }


    private void PauseGame()
    {
        Debug.Log("Pasuing Game");
        Time.timeScale = 0f;
        isUpgrading = true; 
    } 



    bool IsThereAbilities()
    {
        return elementAbilities.Count > 0;
    }
}
