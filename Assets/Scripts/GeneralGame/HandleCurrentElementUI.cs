using UnityEngine;
using UnityEngine.UI;

public class HandleCurrentElementUI : MonoBehaviour
{
    [SerializeField] private Image currentElementIMG;
    [SerializeField] private Image cooldownIMG;


    public void UpdateCurrentElementImage(Sprite _elementIcon, float _cooldown)
    {

        currentElementIMG.sprite = _elementIcon;
        cooldownIMG.fillAmount = _cooldown;
    }
}
