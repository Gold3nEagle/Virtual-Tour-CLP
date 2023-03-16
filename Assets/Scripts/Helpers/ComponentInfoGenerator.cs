using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Text;

public class ComponentInfoGenerator : MonoBehaviour
{
    public void GenerateComponentInfo()
    {
        string info = "";

        Component[] components = GetComponents<Component>();
        foreach (Component component in components)
        {
            info += component.GetType().Name + "\n";
            var fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                string fieldName = field.Name;
                object fieldValue = field.GetValue(component);

                // check if the field is a Vector3 and convert it to a string
                if (fieldValue is Vector3 vector3Value)
                {
                    info += "\t" + fieldName + ": " + vector3Value.ToString() + "\n";
                }
                else
                {
                    info += "\t" + fieldName + ": " + fieldValue.ToString() + "\n";
                }
            }
            info += "\n";
        }

        Debug.Log(info);
        EditorGUIUtility.systemCopyBuffer = info;
        EditorUtility.DisplayDialog("Information Copied", "Component information has been copied to the clipboard.", "OK");
    }
    [ContextMenu("Log Component Info")]
    void LogComponentInfo()
    {
        Component[] components = GetComponents<Component>();
        StringBuilder sb = new StringBuilder();
        foreach (Component component in components)
        {
            sb.AppendLine(component.GetType().ToString());
            FieldInfo[] fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                sb.AppendLine("\t" + field.Name + ": " + field.GetValue(component).ToString());
            }
        }
        Debug.Log(sb.ToString());
    }
}
[CustomEditor(typeof(ComponentInfoGenerator))]
public class ComponentInfoGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ComponentInfoGenerator componentInfoGenerator = (ComponentInfoGenerator)target;
        if (GUILayout.Button("Generate Component Info"))
        {
            componentInfoGenerator.GenerateComponentInfo();
        }
    }
}
//using UnityEngine;
//using UnityEditor;
//using System.Reflection;

//public class ComponentInfoGenerator : MonoBehaviour
//{
//    [MenuItem("GameObject/Log Component Info", false, 10)]
//    static void LogComponentInfo()
//    {
//        GameObject selectedObject = Selection.activeGameObject;

//        if (selectedObject != null)
//        {
//            string info = GetComponentInfoString(selectedObject);
//            Debug.Log(info);
//        }
//        else
//        {
//            Debug.LogWarning("No object selected.");
//        }
//    }

//    static string GetComponentInfoString(GameObject go)
//    {
//        string info = "Components attached to " + go.name + ":\n";

//        Component[] components = go.GetComponents<Component>();
//        foreach (Component c in components)
//        {
//            string typeName = c.GetType().Name;
//            info += typeName + "\n";

//            FieldInfo[] fields = c.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
//            foreach (FieldInfo f in fields)
//            {
//                object value = f.GetValue(c);
//                info += " - " + f.Name + ": " + value.ToString() + "\n";
//            }

//            PropertyInfo[] properties = c.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
//            foreach (PropertyInfo p in properties)
//            {
//                object value = p.GetValue(c);
//                info += " - " + p.Name + ": " + value.ToString() + "\n";
//            }

//            info += "\n";
//        }

//        return info;
//    }
//}
