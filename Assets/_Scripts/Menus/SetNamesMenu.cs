using UnityEngine;
using UnityEngine.UI;

public class SetNamesMenu : MonoBehaviour
{
    public GameObject inputFieldPanel;
    public InputField inputFieldPrefab;
    RectTransform rectTransform;
    float panelHeight;

    void Start()
    {
        rectTransform = inputFieldPanel.GetComponent<RectTransform>();
        panelHeight = rectTransform.sizeDelta.y;
    }

    void Update()
    {
        
    }

    public void SetNames()
    {
        for (int i = 0; i < LocalPlayMenu.playerAmount; i++)
        {
            InputField inputField = Instantiate(inputFieldPrefab, inputFieldPanel.transform, false);
            inputField.transform.localPosition = new Vector3(0, (panelHeight / LocalPlayMenu.playerAmount) * i, 0);
        }
    }
}
