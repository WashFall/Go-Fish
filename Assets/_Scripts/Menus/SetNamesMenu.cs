using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SetNamesMenu : MonoBehaviour
{
    public GameObject inputFieldPanel;
    public GameObject inputFieldPrefab;
    public static List<string> names = new List<string>();

    private float panelHeight;
    private RectTransform rectTransform;
    private List<GameObject> inputFields = new List<GameObject>();

    public void SetNames()
    {
        rectTransform = inputFieldPanel.GetComponent<RectTransform>();
        panelHeight = rectTransform.sizeDelta.y;
        for (int i = 0; i < LocalPlayMenu.playerAmount; i++)
        {
            GameObject inputField = Instantiate(inputFieldPrefab, inputFieldPanel.transform, false);
            inputField.transform.localPosition = new Vector3(0, (panelHeight / LocalPlayMenu.playerAmount) * -i, 0);
            inputFields.Add(inputField);
        }
    }

    public void ConfirmNames()
    {
        names.Clear();
        foreach (GameObject inputField in inputFields)
        {
            Text[] texts = inputField.GetComponentsInChildren<Text>();
            names.Add(texts[1].text);
        }
    }

    public void DestroyInputFields()
    {
        int amount = inputFields.Count;
        for(int i = 0; i < amount; i++)
        {
            var destroyInput = inputFields[0];
            inputFields.RemoveAt(0);
            Destroy(destroyInput);
        }
    }
}
