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

    // 1. ���̶� �ݶ��̴� �浹 ��, ���� Ŭ������ �� grabbedGun true ��ȯ
    // 2. summonedGun�� ����ؼ� �÷��̾� ���󰡴� bool ������ Ʈ�����ϰ� Update�� ���� ����ٴϵ��� ����
}
