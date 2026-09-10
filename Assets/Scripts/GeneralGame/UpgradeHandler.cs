using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField] PlayerElementHandler pEH; 

    [SerializeField]  List<GameObject> elementAbilities = new List<GameObject>();


    Button[] upgradeButtons = new Button[3];



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

    void ButtonPopUp()
    {
        //Make it so that the game pauses when the player levels up and gets to choose their ability. 
    }

    void SetButtonData()
    {
        //Set the icon, abilitydata, and abilityname on each button.
        //Respective to what ability is rolled to it. 
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



    bool IsThereAbilities()
    {
        return elementAbilities.Count > 0;
    }
}
