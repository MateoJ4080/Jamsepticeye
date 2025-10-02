using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent onClick;

    private TextMeshProUGUI text;

    void Start() => text = GetComponent<TextMeshProUGUI>();

    public void OnPointerEnter(PointerEventData data)
        => text.fontStyle = FontStyles.Bold;

    public void OnPointerExit(PointerEventData data)
        => text.fontStyle = FontStyles.Normal;

    public void OnPointerClick(PointerEventData data) => onClick.Invoke();
}
