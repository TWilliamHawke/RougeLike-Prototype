using UnityEngine.EventSystems;

public static class UIBehaviourExtension
{
    public static void Show(this UIBehaviour uiElement)
    {
        uiElement.gameObject.SetActive(true);
    }

    public static void Hide(this UIBehaviour uiElement)
    {
        uiElement.gameObject.SetActive(false);
    }
}

