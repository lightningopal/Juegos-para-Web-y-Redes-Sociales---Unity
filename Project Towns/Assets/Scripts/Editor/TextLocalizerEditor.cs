using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Clase TextLocalizerEditWindow, que contiene la ventana de editar clave
/// </summary>
public class TextLocalizerEditWindow : EditorWindow
{
    public static void Open(string key)
    {
        TextLocalizerEditWindow window = CreateInstance<TextLocalizerEditWindow>();
        window.titleContent = new GUIContent("Add new Localizer Key");
        window.ShowUtility();
        window.key = key;
    }

    public string key, value;

    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Key: ", GUILayout.MaxWidth(50));
        key = EditorGUILayout.TextField(key, EditorStyles.textArea, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Value: ", GUILayout.MaxWidth(50));

        EditorStyles.textArea.wordWrap = true;
        value = EditorGUILayout.TextArea(value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(400));
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Add"))
        {
            if (LocalizationSystem.GetLocalizedValue(key) != string.Empty)
            {
                LocalizationSystem.Replace(key, value);
            }
            else
            {
                LocalizationSystem.Add(key, value);
            }
            Close();
        }

        minSize = new Vector2(460, 250);
        maxSize = minSize;
    }
}

/// <summary>
/// Clase TextLocalizerEditWindow, que contiene la ventana de buscar clave
/// </summary>
public class TextLocalizerSearchWindow : EditorWindow
{
    public static void Open()
    {
        TextLocalizerSearchWindow window = CreateInstance<TextLocalizerSearchWindow>();
        window.titleContent = new GUIContent("Localization Search");

        Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect r = new Rect(mouse.x - 450, mouse.y +10 , 10, 10);
        window.ShowAsDropDown(r, new Vector2(500, 300));
    }

    public string value;
    public Vector2 scroll;
    public Dictionary<string, string> dictionary;

    private void OnEnable()
    {
        dictionary = LocalizationSystem.GetDictionaryForEditor();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
        value = EditorGUILayout.TextField(value);
        EditorGUILayout.EndHorizontal();

        GetSearchResults();
    }

    private void GetSearchResults()
    {
        EditorGUILayout.BeginVertical();
        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (KeyValuePair<string, string> element in dictionary)
        {
            if (value == null)
            {
                EditorGUILayout.BeginHorizontal("box");
                Texture checkIcon = Resources.Load<Texture>("check");

                GUIContent contentCheck = new GUIContent(checkIcon);

                if (GUILayout.Button(contentCheck, GUILayout.MaxHeight(20), GUILayout.MaxWidth(20)))
                {
                    Close();
                    AssetDatabase.Refresh();
                }

                Texture closeIcon = Resources.Load<Texture>("close");

                GUIContent contentClose = new GUIContent(closeIcon);

                if (GUILayout.Button(contentClose, GUILayout.MaxHeight(20), GUILayout.MaxWidth(20)))
                {
                    if (EditorUtility.DisplayDialog("Remove key " + element.Key + "?",
                        "This will remove the element from localizaton, are you sure?", "Do it"))
                    {
                        LocalizationSystem.Remove(element.Key);
                        AssetDatabase.Refresh();
                        LocalizationSystem.Init();
                        dictionary = LocalizationSystem.GetDictionaryForEditor();
                    }
                }

                EditorGUILayout.TextField(element.Key);
                EditorGUILayout.LabelField(element.Value);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                if (element.Key.ToLower().Contains(value.ToLower()) || element.Value.ToLower().Contains(value.ToLower()))
                {
                    EditorGUILayout.BeginHorizontal("box");
                    Texture checkIcon = Resources.Load<Texture>("check");

                    GUIContent contentCheck = new GUIContent(checkIcon);

                    if (GUILayout.Button(contentCheck, GUILayout.MaxHeight(20), GUILayout.MaxWidth(20)))
                    {
                        Close();
                        AssetDatabase.Refresh();
                    }

                    Texture closeIcon = Resources.Load<Texture>("close");

                    GUIContent contentClose = new GUIContent(closeIcon);

                    if (GUILayout.Button(contentClose, GUILayout.MaxHeight(20), GUILayout.MaxWidth(20)))
                    {
                        if (EditorUtility.DisplayDialog("Remove key " + element.Key + "?",
                            "This will remove the element from localizaton, are you sure?", "Do it"))
                        {
                            LocalizationSystem.Remove(element.Key);
                            AssetDatabase.Refresh();
                            LocalizationSystem.Init();
                            dictionary = LocalizationSystem.GetDictionaryForEditor();
                        }
                    }

                    EditorGUILayout.TextField(element.Key);
                    EditorGUILayout.LabelField(element.Value);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}