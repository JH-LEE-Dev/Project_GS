using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private Player_StatComponent baseStat;
    private Player_CursorComponent cursor;

    // 재화 컴포넌트 필요 ( 추후에 상점에서 사용하거나 건축에 사용될 재화가 포함 되어 있음 )
    // 레벨시스템 컴포넌트 필요 ( 레벨 관련된 경험치, 최대 경험치 등 )

    [Header("Gun Information Detail")]
    [SerializeField] private bool grabbedGun;
    [SerializeField] private GunName curGunName;
    [SerializeField] private GameObject[] gunPrefab;
    private Gun summonedGun;

    [Header("Collision LayerMask Detail")]
    [SerializeField] private LayerMask[] collisionLayer;

    private Camera cam;
    private CircleCollider2D col;

    private void Awake()
    {
        input ??= new PlayerInputSet();

        baseStat ??= GetComponent<Player_StatComponent>();
        col ??= GetComponent<CircleCollider2D>();
        cam ??= Camera.main;

        curGunName = GunName.M92;
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

    public bool CreateGun(Transform spawnPoint)
    {
        if (null == gunPrefab[(int)curGunName])
            return false;

        GameObject summonedObject = Instantiate(gunPrefab[(int)curGunName], spawnPoint.position, Quaternion.identity);

        this.summonedGun = summonedObject?.GetComponent<Gun>();
        summonedGun.Initialize(this);
        grabbedGun = false;

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

    // 1. 총이랑 콜라이더 충돌 후, 총을 클릭했을 때 grabbedGun true 전환
    // 2. summonedGun을 사용해서 플레이어 따라가는 bool 변수를 트리거하고 Update를 통해 따라다니도록 설계
}
