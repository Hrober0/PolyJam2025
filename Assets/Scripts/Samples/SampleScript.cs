using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SampleScript : MonoBehaviour
{
    [SerializeField] private List<string> names;
    [SerializeField] private Button prefab;
    [SerializeField] private Transform parent;

    private ListBehavior<Button> buttonsList;

    private void Start()
    {
        buttonsList = new(parent, prefab);
        UpdateList();
    }

    [ContextMenu("UpdateList")]
    private void UpdateList()
    {
        buttonsList.SetElements(names, BindDataToButton);
    }

    private void BindDataToButton(Button button, string name)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = name;
    }
}
