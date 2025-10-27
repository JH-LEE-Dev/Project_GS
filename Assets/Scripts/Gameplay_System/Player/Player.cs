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
    [SerializeField] private float searchBoundary = 1f;
    [SerializeField] private int colSelectIdx = 0;

    [Header("Summoned Gun Information Details")]
    [SerializeField] private bool grabbedGun;
    [SerializeField] private GunName equipmentGunName;
    [SerializeField] private GunPrefab[] gunPrefab;
    private Gun summonedGun;

    [Header("Collision LayerMask Details")]
    [SerializeField] private LayerMask gunLayer;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private LayerMask npcLayer;

    private Camera cam;

    private void Awake()
    {
        input ??= new PlayerInputSet();

        baseStat ??= GetComponent<Player_StatComponent>();
        cam ??= Camera.main;
    }

    private void Update()
    {
        CollisionClosest();
        MouseChase();
        PutdownGun();
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
        // 충돌할 때 Collider2D[] 배열을 계속 생성하는 것도 추후에 바꿔야 함
        LayerMask combinedLayer = grabbedGun ? (itemLayer | npcLayer) : (gunLayer | itemLayer | npcLayer);
        Collider2D[] colList = Physics2D.OverlapCircleAll(transform.position, searchBoundary, combinedLayer);

        int count = colList.Length;
        if (count <= 0)
        {
            colSelectIdx = 0;
            return;
        }

        MouseScrollPerformed(colList);
        Debug.Log("Test");

        // 여기서 VFX Component가 들어가게 되면, VFX 컴포넌트를 빼오고 없으면 못하게 넘어감
        if (input.Player.Interaction.WasPerformedThisFrame())
        {
            //Debug.Log("Mouse Clicked.");
            GameObject targetObj = colList[colSelectIdx].gameObject;
            int curTargetLayer = targetObj.layer;

            if (CompareLayer(targetObj, gunLayer))
            {
                // 이 세계에서는 총이 딱 하나 나오기 때문에 그냥 캐싱한 거 쓰자
                summonedGun.ChangeFollowHand(GrabState.grabbed);
                grabbedGun = true;
            }
        }
    }

    private void PutdownGun()
    {
        if (!grabbedGun)
            return;

        if (input.Player.Drop.WasPerformedThisFrame())
        {
            summonedGun.ChangeFollowHand(GrabState.dropped);
            grabbedGun = false;
        }
    }

    private void MouseScrollPerformed(Collider2D[] colList)
    {
        // 충돌 순서는 중요하지 않음, 이걸 정하는 순간 정렬을 하거나 해야 되는데
        // 이건 마우스 휠키 업, 다운으로 인덱스 값을 올리고 내리고 하면 될 것 같음

        if (input.Player.ScrollUp.WasPerformedThisFrame())
            colSelectIdx = Mathf.Min(colSelectIdx + 1, colList.Length - 1);
        else if (input.Player.ScrollDown.WasPerformedThisFrame())
            colSelectIdx = Mathf.Max(colSelectIdx - 1, 0);
    }

    public GameObject CreateGun(Transform spawnPoint)
    {
        if (null == gunPrefab[(int)equipmentGunName].prefab)
            return default;

        GameObject summonedObject = Instantiate(gunPrefab[(int)equipmentGunName].prefab, spawnPoint.position, Quaternion.identity);

        this.summonedGun = summonedObject?.GetComponent<Gun>();
        summonedGun.Initialize(this);
        grabbedGun = false;

        //Debug.Log("Successfully Completed a Gun Spawn");

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchBoundary);
    }

    private bool CompareLayer(GameObject target, LayerMask src)
    {
        return 0 != (1 << target.layer & gunLayer.value);
    }
    // 1. 총이랑 콜라이더 충돌 후, 총을 클릭했을 때 grabbedGun true 전환
    // 2. summonedGun을 사용해서 플레이어 따라가는 bool 변수를 트리거하고 Update를 통해 따라다니도록 설계
}
