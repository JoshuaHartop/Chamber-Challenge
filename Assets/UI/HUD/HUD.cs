using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private string _interactionIndicatorElementName;

    private UIDocument _document;

    private VisualElement _interactionIndicator;

    private void OnEnable()
    {
        _document = GetComponent<UIDocument>();

        _interactionIndicator = _document.rootVisualElement.Query<VisualElement>(_interactionIndicatorElementName);
    }

    private void FixedUpdate()
    {
        if (CameraInteraction.IsPlayerLookingAtInteracatible)
        {
            _interactionIndicator.style.display = DisplayStyle.Flex;
        }
        else
        {
            _interactionIndicator.style.display = DisplayStyle.None;
        }
    }
}
