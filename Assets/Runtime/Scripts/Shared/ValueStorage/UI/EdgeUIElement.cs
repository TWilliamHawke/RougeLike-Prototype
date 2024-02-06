using UnityEngine;
using UnityEngine.Events;

public abstract class EdgeUIElement : MonoBehaviour, IObserver<IValueStorage>
{
    [SerializeField] float _edgeValue = 0;
    [SerializeField] ActiveConditions _activeif = ActiveConditions.moreThanEdge;
    public bool isActive { get; set; } = true;
    
    public event UnityAction<EdgeUIElement> OnStateChange;

    protected abstract void UpdateVisual();
    protected abstract void UpdateText(float edgeValue);

    void Start()
    {
        UpdateText(_edgeValue);
    }

    public void AddToObserve(IValueStorage target)
    {
        target.OnValueChange += UpdateState;
        UpdateState(target.currentValue);
    }

    public void RemoveFromObserve(IValueStorage target)
    {
        target.OnValueChange -= UpdateState;
    }

    public void SetEdge(float newEdge)
    {
        _edgeValue = newEdge;
        UpdateVisual();
        UpdateText(_edgeValue);
    }

    public void UpdateState(int value)
    {
        bool oldIsActive = isActive;

        isActive = value >= _edgeValue;
        if (_activeif == ActiveConditions.lessThanEdge)
        {
            isActive = value <= _edgeValue;
        }

        if (oldIsActive == isActive) return;
        UpdateVisual();
        OnStateChange?.Invoke(this);
    }

    enum ActiveConditions
    {
        moreThanEdge = 1,
        lessThanEdge = 2,
    }
}
