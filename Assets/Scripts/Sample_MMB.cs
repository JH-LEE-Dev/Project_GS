using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sample_MMB : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;

    [SerializeField] private Sprite hoverImage;
    [SerializeField] private Sprite ClikedImage;
    private Sprite originalImage;

    private void Awake()
    {
        image??= GetComponent<Image>();
        originalImage = image.sprite;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = originalImage;
    }
}
