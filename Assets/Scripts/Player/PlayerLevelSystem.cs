using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [SerializeField] int maxLevel = 15;
    [SerializeField] int staringLevel = 1;
    [SerializeField] int currentLevel;
    [SerializeField] float xpNeededForNextLevel = 10;
    [SerializeField] float currentXP;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        while(currentXP >= xpNeededForNextLevel && currentLevel < maxLevel)
        {
            currentXP -= xpNeededForNextLevel;
            currentXP = Mathf.Clamp(currentXP, 0, xpNeededForNextLevel);

            if (currentLevel < 10)
            {
                xpNeededForNextLevel *= 1.3f;
            }
            else
            {
                xpNeededForNextLevel *= 1.36f;          
            }

            xpNeededForNextLevel = Mathf.RoundToInt(xpNeededForNextLevel);

            AddLevel();
        }
    }

    void AddLevel()
    {
        if (currentLevel != maxLevel)
        {
            currentLevel++;
            currentLevel = Mathf.Clamp(currentLevel, staringLevel, maxLevel);

            //Add choosing ability/upgrade after leveling up
        }
    }
}
