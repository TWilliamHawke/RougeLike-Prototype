using UnityEngine;
using UnityEngine.EventSystems;

public class CustomEventButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CustomEvent _onClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClickEvent.Invoke();
    }

}