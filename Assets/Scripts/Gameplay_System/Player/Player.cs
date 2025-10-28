using Game.GlobalFunc;
using Game.Prefab;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private Player_StatComponent baseStat;
    private Player_CursorComponent cursor;

    // ��ȭ ������Ʈ �ʿ� ( ���Ŀ� �������� ����ϰų� ���࿡ ���� ��ȭ�� ���� �Ǿ� ���� )
    // �����ý��� ������Ʈ �ʿ� ( ���� ���õ� ����ġ, �ִ� ����ġ �� )
    // ���� ���� Ư�� ��ɸ� ��ũ��Ʈ�� �ٲٴ� �� �����Ѱ�

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
        // �浹�� �� Collider2D[] �迭�� ��� �����ϴ� �͵� ���Ŀ� �ٲ�� ��
        LayerMask combinedLayer = grabbedGun ? (itemLayer | npcLayer) : (gunLayer | itemLayer | npcLayer);
        Collider2D[] colList = Physics2D.OverlapCircleAll(transform.position, searchBoundary, combinedLayer);

        int count = colList.Length;
        if (count <= 0)
        {
            colSelectIdx = 0;
            return;
        }

        ScrollDetectedForChangeIdx(count);

        // ���⼭ VFX Component�� ���� �Ǹ�, VFX ������Ʈ�� ������ ������ ���ϰ� �Ѿ
        if (input.Player.Interaction.WasPerformedThisFrame())
        {
            GameObject targetObj = colList[colSelectIdx].gameObject;
            int curTargetLayer = targetObj.layer;

            if (LayerUtill.CompareLayerMask(targetObj, gunLayer))
            {
                // �� ���迡���� ���� �� �ϳ� ������ ������ �׳� ĳ���� �� ����
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
            summonedGun.SetVelocity(0f, 0f);
            grabbedGun = false;
        }
    }

    private void ScrollDetectedForChangeIdx(int size)
    {
        // �浹 ������ �߿����� ����, �̰� ���ϴ� ���� ������ �ϰų� �ؾ� �Ǵµ�
        // �̰� ���콺 ��Ű ��, �ٿ����� �ε��� ���� �ø��� ������ �ϸ� �� �� ����

        if (input.Player.ScrollUp.WasPerformedThisFrame())
            colSelectIdx = Mathf.Min(colSelectIdx + 1, size - 1);
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
        SetCameraTarget();

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
    // 1. ���̶� �ݶ��̴� �浹 ��, ���� Ŭ������ �� grabbedGun true ��ȯ
    // 2. summonedGun�� ����ؼ� �÷��̾� ���󰡴� bool ������ Ʈ�����ϰ� Update�� ���� ����ٴϵ��� ����

    private void SetCameraTarget()
    {
        CinemachineCamera vCam = FindAnyObjectByType<CinemachineCamera>();
        vCam.Follow = summonedGun.transform;
    }
}
