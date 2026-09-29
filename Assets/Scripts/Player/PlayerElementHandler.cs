using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerElementHandler : MonoBehaviour
{
    [SerializeField] public ElemntData[] _chosenElements = new ElemntData[2];
    [SerializeField] ElemntData _currentElement; 
    [SerializeField]  PlayerShooting _playerShooting;

    [SerializeField] InputAction _switchElementAction;

    float swapCooldown = 3f;

    float _originalSwapCooldown; 

    public List<GameObject> abilities = new List<GameObject>();

    //Every ability, including passives, are going to be created as prefabs. 
    //So once the player clicks on the button to unlock an ability, that ability gets added to the list.


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _originalSwapCooldown = swapCooldown; 
        SetOriginalElement();
    }

    // Update is called once per frame
    void Update()
    {
        swapCooldown -= Time.deltaTime; 

        if (_switchElementAction.WasPressedThisFrame() && swapCooldown <= 0)
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

        _playerShooting.projectile = _currentElement.projectile;

        yield return null;
    }

    void SetOriginalElement()
    {
        _playerShooting = GetComponent<PlayerShooting>();
        _currentElement = _chosenElements[0];
        _playerShooting.projectile = _currentElement.projectile;
    }

    public void SpawnPlayerAbilities(GameObject ability)
    {
            Instantiate(ability, transform.position, Quaternion.identity);   
    }

    bool IsOriginalElementActive()
    {
        return _currentElement == _chosenElements[0]; 
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
