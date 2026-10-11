using UnityEngine;
using UnityEngine.UI;

public class HandleXpBar : MonoBehaviour
{
    [SerializeField] Image xpBar;



    public void HandleXPBar(float maxXp, float currentXp)
    {
        xpBar.fillAmount = currentXp / maxXp;
    }
}
