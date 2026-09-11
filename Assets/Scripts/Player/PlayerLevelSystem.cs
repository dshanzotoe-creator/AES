using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [SerializeField] int maxLevel = 15;
    [SerializeField] int staringLevel = 1;
    [SerializeField] int currentLevel;
    [SerializeField] float xpNeededForNextLevel = 10;
    [SerializeField] float currentXP;

    [SerializeField] UpgradeHandler upgradeHandler;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeHandler = GameObject.Find("GameManager").GetComponent<UpgradeHandler>();
        currentLevel = staringLevel;
        currentXP = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        XpHandler();
    }

    void XpHandler()
    {
        //Picking Up Object Adds Xp Logic

        while(currentXP >= xpNeededForNextLevel && currentLevel < maxLevel && !upgradeHandler.isUpgrading)
        {
            currentXP -= xpNeededForNextLevel;
            currentXP = Mathf.RoundToInt(currentXP);

            AddLevel();

            if (currentLevel < 10)
            {
                xpNeededForNextLevel *= 1.3f;
            }
            else
            {
                xpNeededForNextLevel *= 1.37f;          
            }

             

            xpNeededForNextLevel = Mathf.RoundToInt(xpNeededForNextLevel);
        }
    }

    void AddLevel()
    {
        if (currentLevel != maxLevel)
        {
            currentLevel++;
            currentLevel = Mathf.Clamp(currentLevel, staringLevel, maxLevel);

            //Add choosing ability/upgrade after leveling up
            upgradeHandler.ActivateButtonLogic(); 

        }
    }
}
