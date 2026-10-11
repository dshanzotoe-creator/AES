using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [SerializeField] int maxLevel = 6;
    [SerializeField] int staringLevel = 1;
    [SerializeField] int currentLevel;
     public float xpNeededForNextLevel = 10;
     public float currentXP;

    [SerializeField] UpgradeHandler upgradeHandler;

    HandleXpBar xpBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeHandler = GameObject.Find("GameManager").GetComponent<UpgradeHandler>();
        xpBar = GameObject.Find("GameManager").GetComponent<HandleXpBar>();
        currentLevel = staringLevel;
        currentXP = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        XpHandler();
        xpBar.HandleXPBar(xpNeededForNextLevel, currentXP);
    }

    void XpHandler()
    {
        //Picking Up Object Adds Xp Logic

        while(currentXP >= xpNeededForNextLevel && currentLevel < maxLevel && !upgradeHandler.isUpgrading)
        {
            currentXP -= xpNeededForNextLevel;
            currentXP = Mathf.RoundToInt(currentXP);

            AddLevel();

            if (currentLevel < 3)
            {
                xpNeededForNextLevel *= 1.3f;
            }
            else
            {
                xpNeededForNextLevel *= 1.35f;          
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
