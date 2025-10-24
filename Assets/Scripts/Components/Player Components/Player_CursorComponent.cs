using UnityEngine;

public enum HandType { idle, select, grab, magnifier, end }

[System.Serializable]
public class Hand
{
    public HandType tpye;
    public Sprite sprite;
}

public class Player_CursorComponent : MonoBehaviour
{
    [SerializeField] private Hand[] cursor;
    private SpriteRenderer sr;

    private HandType curHandType;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        ChangeCursor(HandType.idle);
    }

    private void Update()
    {
        
    }

    public void ChangeCursor(HandType type)
    {
        if(HandType.end == type)
            type = HandType.idle;

        curHandType = type;
        sr.sprite = cursor[(int)curHandType].sprite;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        int limits = (int)HandType.end;

        if (null == cursor || cursor.Length != limits)
            System.Array.Resize(ref cursor, limits);
    }
#endif
}
