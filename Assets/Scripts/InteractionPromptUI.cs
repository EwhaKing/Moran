using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text promptText;

    private void Awake()
    {
        Hide();
    }

    public void Show(string text)
    {
        if (panel != null)
            panel.SetActive(true);

        if (promptText != null)
            promptText.text = text;
    }

    public void Hide()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}