using UnityEditor;
using UnityEngine;

public class ItemDataEditorWindow : EditorWindow
{
    private ItemData selectedData;
    private Vector2 scrollPosition;

    [MenuItem("Window/Item Data Editor")]
    public static void ShowWindow()
    {
        GetWindow<ItemDataEditorWindow>("Item Data Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Item Data Editor", EditorStyles.boldLabel);

        // Add your GUI elements here

        GUILayout.Space(10);

        DisplayItemDataList();

        GUILayout.Space(10);

        if (GUILayout.Button("Add New Item"))
        {
            CreateNewItemData();
        }

        if (selectedData != null)
        {
            DisplaySelectedItemData();
        }
    }

    private void DisplayItemDataList()
    {
        GUILayout.Label("Item Data List", EditorStyles.boldLabel);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        // Display a scrollable list of ItemData
        foreach (var itemData in Resources.LoadAll<ItemData>("Items"))
        {
            if (GUILayout.Button(itemData.name))
            {
                selectedData = itemData;
            }
        }

        GUILayout.EndScrollView();
    }

    private void DisplaySelectedItemData()
    {
        GUILayout.Label("Selected Item Data", EditorStyles.boldLabel);

        // Add GUI elements to display and edit the selected ItemData
        selectedData.ItemName = EditorGUILayout.TextField("Item Name", selectedData.ItemName);
        selectedData.ItemIcon = (Sprite)EditorGUILayout.ObjectField("Item Icon", selectedData.ItemIcon, typeof(Sprite), false);

        if (GUILayout.Button("Save"))
        {
            SaveSelectedData();
        }
    }

    private void CreateNewItemData()
    {
        // Create a new ItemData
        selectedData = ScriptableObject.CreateInstance<ItemData>();

        // Set default values or prompt the user for initial values
        selectedData.name = "Item_"; // Set a default name or prompt the user for a name
        selectedData.SetID(System.Guid.NewGuid().ToString());

        // Focus on the newly created ItemData
        EditorGUIUtility.PingObject(selectedData);
    }


    private void SaveSelectedData()
    {
        if (selectedData != null)
        {
            // Save the changes made to the selected ItemData
            selectedData.name = $"Item_{selectedData.ItemName}"; // Set a default name or prompt the user for a name
            string path = $"Assets/Resources/Items/{selectedData.name}.asset";
            AssetDatabase.CreateAsset(selectedData, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = selectedData;
        }
    }
}
