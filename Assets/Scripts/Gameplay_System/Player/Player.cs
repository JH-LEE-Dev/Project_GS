using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

using Game.Prefab;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private Player_StatComponent baseStat;
    private Player_CursorComponent cursor;

    // ��ȭ ������Ʈ �ʿ� ( ���Ŀ� �������� ����ϰų� ���࿡ ���� ��ȭ�� ���� �Ǿ� ���� )
    // �����ý��� ������Ʈ �ʿ� ( ���� ���õ� ����ġ, �ִ� ����ġ �� )
    // ���� ���� Ư�� ��ɸ� ��ũ��Ʈ�� �ٲٴ� �� �����Ѱ�

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

    // 1. ���̶� �ݶ��̴� �浹 ��, ���� Ŭ������ �� grabbedGun true ��ȯ
    // 2. summonedGun�� ����ؼ� �÷��̾� ���󰡴� bool ������ Ʈ�����ϰ� Update�� ���� ����ٴϵ��� ����
}
