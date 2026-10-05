using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField] private PlayerElementHandler pEH;

    [SerializeField]
    private List<GameObject> elementAbilities =
        new List<GameObject>();


    private Dictionary<GameObject, ElemntData> abilityOwners =
        new Dictionary<GameObject, ElemntData>();


    public bool isUpgrading = false;


    [SerializeField]
    private GameObject[] upgradeButtons =
        new GameObject[3];


    private GameObject[] buttonAbilities =
        new GameObject[3];


    private void Start()
    {
        GetElementalAbilities();
    }




    void GetElementalAbilities()
    {
        pEH = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerElementHandler>();


        foreach (ElemntData element in pEH._chosenElements)
        {
            foreach (GameObject ability in element.abillities)
            {
                if (ability == null)
                    continue;



                elementAbilities.Add(ability);



                if (!abilityOwners.ContainsKey(ability))
                {
                    abilityOwners.Add(
                        ability,
                        element
                    );
                }
            }
        }
    }



    public void ActivateButtonLogic()
    {
        if (isUpgrading)
            return;


        if (!IsThereAbilities())
            return;


        PauseGame();



        List<GameObject> availableAbilities =
            new List<GameObject>(elementAbilities);


        int amountChoices =
            Mathf.Min(
                upgradeButtons.Length,
                availableAbilities.Count
            );


        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            bool shouldBeActive =
                i < amountChoices;


            upgradeButtons[i].SetActive(
                shouldBeActive
            );



            buttonAbilities[i] = null;
        }



        for (int i = 0; i < amountChoices; i++)
        {
            int randomNumber =
                Random.Range(
                    0,
                    availableAbilities.Count
                );


            GameObject chosenAbility =
                availableAbilities[randomNumber];


            buttonAbilities[i] =
                chosenAbility;


            upgradeButtons[i].name =
                chosenAbility.name;


            availableAbilities.RemoveAt(
                randomNumber
            );
        }
    }



    public void ChooseUpgrade(int buttonNumber)
    {
        if (buttonNumber < 0 ||
            buttonNumber >= buttonAbilities.Length)
        {
            return;
        }


        GameObject ability =
            buttonAbilities[buttonNumber];


        if (ability == null)
            return;


        if (!abilityOwners.TryGetValue(
                ability,
                out ElemntData ownerElement))
        {
            Debug.LogError(
                "Could not find element owner for: "
                + ability.name
            );

            ResumeGame();

            return;
        }



        pEH.SpawnPlayerAbilities(
            ability,
            ownerElement
        );


        elementAbilities.Remove(
            ability
        );


        abilityOwners.Remove(
            ability
        );


        ResumeGame();
    }



    private void PauseGame()
    {
        Debug.Log("Pausing Game");

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