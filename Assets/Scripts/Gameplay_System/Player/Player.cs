using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;

    private PStat_Component stat;

    private void Awake()
    {
        input??= new PlayerInputSet();

        stat = GetComponent<PStat_Component>();
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        input?.Enable();
    }

    private void OnDisable()
    {
        input?.Disable();
    }
}
