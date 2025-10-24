using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

using Game.Prefab;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private Player_StatComponent baseStat;
    private Player_CursorComponent cursor;

    // 재화 컴포넌트 필요 ( 추후에 상점에서 사용하거나 건축에 사용될 재화가 포함 되어 있음 )
    // 레벨시스템 컴포넌트 필요 ( 레벨 관련된 경험치, 최대 경험치 등 )
    // 총이 가진 특수 기능만 스크립트를 바꾸는 게 가능한가

    [Header("Collision Details")]
    [SerializeField] private Transform collTransform;
    [SerializeField] private float circleRadius;

    [Header("Summoned Gun Information Details")]
    [SerializeField] private bool grabbedGun;
    [SerializeField] private GunName equipmentGunName;
    [SerializeField] private GunPrefab[] gunPrefab;
    private Gun summonedGun;

    [Header("Collision LayerMask Details")]
    [SerializeField] private LayerMask[] collisionLayer;

    private Camera cam;

    private void Awake()
    {
        input ??= new PlayerInputSet();

        baseStat ??= GetComponent<Player_StatComponent>();
        cam ??= Camera.main;
    }

    private void Update()
    {
        MouseChase();
    }

    private void MouseChase()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 targetPos = cam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, cam.nearClipPlane));
        targetPos.z = 0f;

        transform.position = targetPos;
    }

    private void CollisionClosest()
    {
        
    }

    public GameObject CreateGun(Transform spawnPoint)
    {
        if (null == gunPrefab[(int)equipmentGunName].prefab)
            return default;

        GameObject summonedObject = Instantiate(gunPrefab[(int)equipmentGunName].prefab, spawnPoint.position, Quaternion.identity);

        this.summonedGun = summonedObject?.GetComponent<Gun>();
        summonedGun.Initialize(this);
        grabbedGun = false;

        Debug.Log("Successfully Completed a Gun Spawn");

        return summonedObject;
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
        int limits = (int)GunName.end;

        if (null == gunPrefab || gunPrefab.Length != limits)
            System.Array.Resize(ref gunPrefab, limits);
    }
#endif

    // 1. 총이랑 콜라이더 충돌 후, 총을 클릭했을 때 grabbedGun true 전환
    // 2. summonedGun을 사용해서 플레이어 따라가는 bool 변수를 트리거하고 Update를 통해 따라다니도록 설계
}
