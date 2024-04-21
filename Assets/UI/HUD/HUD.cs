using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private string _interactionIndicatorElementName;

    [SerializeField]
    private string _messageContainerElementName;

    [SerializeField]
    private string _hudMessageStyleClassName;

    private UIDocument _document;

    private VisualElement _interactionIndicator;
    private VisualElement _messageContainer;

    private void OnEnable()
    {
        _document = GetComponent<UIDocument>();

        _interactionIndicator = _document.rootVisualElement.Query<VisualElement>(_interactionIndicatorElementName);
        _messageContainer = _document.rootVisualElement.Query<VisualElement>(_messageContainerElementName);
    }

    private void Update()
    {
        // Todo: Delete
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMessage("This is a test", 2f);
        }
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

    /// <summary>
    /// Shows a rich-text message on the HUD for the given duration in seconds.
    /// </summary>
    public void ShowMessage(string text, float duration)
    {
        TextElement element = new TextElement
        {
            text = text,
            enableRichText = true
        };

        element.AddToClassList(_hudMessageStyleClassName);

        _messageContainer.Add(element);

        StartCoroutine(ShowMessageCoroutine(_messageContainer, element, duration));
    }

    private IEnumerator ShowMessageCoroutine(VisualElement container, TextElement element, float duration)
    {
        yield return new WaitForSeconds(duration);
        container.Remove(element);
    }
}
