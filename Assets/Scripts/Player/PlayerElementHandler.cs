using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerElementHandler : MonoBehaviour
{
    [SerializeField] public ElemntData[] _chosenElements = new ElemntData[2];

    [SerializeField] public ElemntData _currentElement;

    [SerializeField] private PlayerShooting _playerShooting;

    [SerializeField] private InputAction _switchElementAction;

    [SerializeField] private float swapCooldown = 3f;

    private float _originalSwapCooldown;


    public List<GameObject> abilities = new List<GameObject>();


    private Dictionary<GameObject, ElemntData> abilityOwners
        = new Dictionary<GameObject, ElemntData>();


    private void Start()
    {
        _originalSwapCooldown = swapCooldown;

        SetOriginalElement();
    }


    private void Update()
    {
        swapCooldown -= Time.deltaTime;


        if (_switchElementAction.WasPressedThisFrame()
            && swapCooldown <= 0f)
        {
            StartCoroutine(SwitchElement());

            swapCooldown = _originalSwapCooldown;
        }
    }


    IEnumerator SwitchElement()
    {

        if (IsOriginalElementActive())
        {
            _currentElement = _chosenElements[1];
        }
        else
        {
            _currentElement = _chosenElements[0];
        }


        _playerShooting.projectile =
            _currentElement.projectile;



        _playerShooting.SetActiveElement(
            _currentElement
        );


       UpdateNormalAbilities();


        yield return null;
    }


    void SetOriginalElement()
    {
        _playerShooting = GetComponent<PlayerShooting>();


        _currentElement =
            _chosenElements[0];


        _playerShooting.projectile =
            _currentElement.projectile;


        _playerShooting.SetActiveElement(
            _currentElement
        );
    }


    public void SpawnPlayerAbilities(
    GameObject ability,
    ElemntData ownerElement)
    {
        GameObject spawnedAbility = Instantiate(
            ability,
            transform.position,
            Quaternion.identity
        );

        abilities.Add(spawnedAbility);


        abilityOwners.Add(
            spawnedAbility,
            ownerElement
        );


        AbilityProjectile abilityProjectile =
            spawnedAbility.GetComponent<AbilityProjectile>();


        if (abilityProjectile != null)
        {
            _playerShooting.RegisterAbilityProjectile(
                spawnedAbility,
                ownerElement
            );

            return;
        }


        spawnedAbility.SetActive(
            ownerElement == _currentElement
        );
    }



    void UpdateNormalAbilities()
    {
        for (int i = abilities.Count - 1; i >= 0; i--)
        {
            GameObject ability = abilities[i];



            if (ability == null)
            {
                abilities.RemoveAt(i);
                continue;
            }


            if (!abilityOwners.ContainsKey(ability))
                continue;


            AbilityProjectile abilityProjectile =
                ability.GetComponent<AbilityProjectile>();


            if (abilityProjectile != null)
                continue;


            ElemntData ownerElement =
                abilityOwners[ability];


            bool shouldBeActive =
                ownerElement == _currentElement;


            if (shouldBeActive)
            {
                ability.transform.position =
                    transform.position;
            }


            ability.SetActive(
                shouldBeActive
            );
        }
    }


    bool IsOriginalElementActive()
    {
        return _currentElement ==
               _chosenElements[0];
    }


    private void OnEnable()
    {
        _switchElementAction.Enable();
    }


    private void OnDisable()
    {
        _switchElementAction.Disable();
    }
}