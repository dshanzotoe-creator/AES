using UnityEngine;

public class AbilityReference : MonoBehaviour
{
    [SerializeField] private AbilityData abilityData;

    public AbilityData Data => abilityData; 
}
