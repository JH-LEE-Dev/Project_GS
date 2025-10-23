using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;

    private void Awake()
    {
        input??= new PlayerInputSet();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input?.Disable();
    }

    private void Update()
    {

    }
}
