using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Scriptable Objects/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string abilityName; 

    public string abilityDescription;

    public float damage;
    public float cooldown;

    public Sprite icon; 


}
