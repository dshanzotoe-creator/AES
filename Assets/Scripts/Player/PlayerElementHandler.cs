using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerElementHandler : MonoBehaviour
{
    [SerializeField] ElemntData[] _chosenElements = new ElemntData[2];
    [SerializeField]  PlayerShooting _playerShooting;

    bool isOriginalElementActive = true;

    [SerializeField] InputAction _switchElementAction; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_switchElementAction.WasPressedThisFrame())
        {
            Debug.Log("Switched");
        }
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
