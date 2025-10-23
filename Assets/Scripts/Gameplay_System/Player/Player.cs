using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private Player_StatComponent stat;

    [Header("Gun Information Detail")]
    [SerializeField] private GunName curGunName;
    [SerializeField] private GameObject[] gunPrefab;
    private Gun summonedGun;

    private void Awake()
    {
        input??= new PlayerInputSet();

        stat??= GetComponent<Player_StatComponent>();
        curGunName = GunName.M92;
    }

    private void Update()
    {

    }

    public bool CreateGun(Transform spawnPoint)
    {
        if (null == gunPrefab[(int)curGunName])
            return false;

        GameObject summonedObject = Instantiate(gunPrefab[(int)curGunName], spawnPoint.position, Quaternion.identity);

        this.summonedGun = summonedObject?.GetComponent<Gun>();
        summonedGun.Initialize(this);

        return true;
    }

    private void OnEnable()
    {
        input?.Enable();
    }

    private void OnDisable()
    {
        input?.Disable();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        int limits = (int)GunName.End;

        if (null == gunPrefab || gunPrefab.Length != limits)
            System.Array.Resize(ref gunPrefab, limits);
    }
#endif
}
