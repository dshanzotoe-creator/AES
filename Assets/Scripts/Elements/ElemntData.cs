using UnityEngine;

[CreateAssetMenu(fileName = "ElemntData", menuName = "Scriptable Objects/ElemntData")]
public class ElemntData : ScriptableObject
{
    public string elementName;
    public Sprite icon;
    public GameObject projectile;
    public GameObject[] abillities;
}
