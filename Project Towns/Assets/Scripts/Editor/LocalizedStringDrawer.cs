﻿using UnityEngine;
using UnityEditor;

/// <summary>
/// ClaseLocalizedStringDrawer, para tener una GUI
/// </summary>
[CustomPropertyDrawer(typeof(LocalizedString))]
public class LocalizedStringDrawer : PropertyDrawer
{
    bool dropdown;
    float height;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (dropdown)
        {
            return height + 25;
        }

        return 20;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.width -= 34;
        position.height = 18;

        Rect valueRect = new Rect(position);
        valueRect.x += 15;
        valueRect.width -= 15;

        Rect foldButtonRect = new Rect(position);
        foldButtonRect.width = 15;

        dropdown = EditorGUI.Foldout(foldButtonRect, dropdown, "");

        position.x += 15;
        position.width -= 15;

        SerializedProperty key = property.FindPropertyRelative("key");
        key.stringValue = EditorGUI.TextField(position, key.stringValue);

        position.x += position.width + 2;
        position.width = 17;
        position.height = 17;

        Texture searchIcon = Resources.Load<Texture>("Icons/search");
        GUIContent searchContent = new GUIContent(searchIcon);

        if (GUI.Button(position, searchContent))
        {
            TextLocalizerSearchWindow.Open();
        }

        position.x += position.width + 2;

        Texture storeIcon = Resources.Load<Texture>("Icons/store");
        GUIContent storeContent = new GUIContent(storeIcon);

        if (GUI.Button(position, storeContent))
        {
            TextLocalizerEditWindow.Open(key.stringValue);
        }

        if (dropdown)
        {
            var value = "";
            int languageCount = System.Enum.GetNames(typeof(LocalizationSystem.Language)).Length;
            for (int i = 0; i < languageCount; i++)
            {
                value += LocalizationSystem.GetLocalizedValue(key.stringValue, i);
                if (i != languageCount - 1)
                    value += "\n";
            }
            
            LocalizationSystem.GetLocalizedValue(key.stringValue);
            GUIStyle style = GUI.skin.box;
            height = style.CalcHeight(new GUIContent(value), valueRect.width);

            valueRect.height = height;
            valueRect.y += 21;
            EditorGUI.LabelField(valueRect, value, EditorStyles.wordWrappedLabel);
        }

        EditorGUI.EndProperty();
    }
}