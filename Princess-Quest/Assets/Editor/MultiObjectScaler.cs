using UnityEngine;
using UnityEditor;

// Clase hecha con IA para redimensionar múltiples objetos sin perjudicar su posición relativa
public class MultiObjectScaler : EditorWindow
{
    private float scaleFactor = 0.5f; // Factor de escala por defecto (0.5 = 50% más pequeño)

    // Crea la opción en el menú de Unity
    [MenuItem("Tools/Multi-Object Scaler")]
    public static void ShowWindow()
    {
        GetWindow<MultiObjectScaler>("Multi-Object Scaler");
    }

    // Dibuja la interfaz de la ventana del editor
    void OnGUI()
    {
        GUILayout.Label("Scale Selected Objects", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Selecciona los objetos que quieres escalar en la jerarquía, ajusta el factor y haz clic en 'Scale'.", MessageType.Info);

        scaleFactor = EditorGUILayout.FloatField("Scale Factor", scaleFactor);

        if (GUILayout.Button("Scale Objects"))
        {
            ScaleSelectedObjects();
        }
    }

    private void ScaleSelectedObjects()
    {
        // Obtener todos los objetos seleccionados
        Transform[] selection = Selection.transforms;

        if (selection.Length == 0)
        {
            Debug.LogWarning("No objects selected to scale.");
            return;
        }

        // 1. Calcular el punto central de todos los objetos seleccionados
        Vector3 center = Vector3.zero;
        foreach (Transform t in selection)
        {
            center += t.position;
        }
        center /= selection.Length;

        // Registrar la acción para poder deshacerla (Ctrl+Z)
        Undo.RecordObjects(selection, "Scale Multiple Objects");

        // 2. Escalar cada objeto y ajustar su posición relativa al centro
        foreach (Transform t in selection)
        {
            // Vector desde el centro al objeto
            Vector3 direction = t.position - center;

            // Nueva posición escalada
            t.position = center + direction * scaleFactor;

            // Nueva escala del objeto
            t.localScale *= scaleFactor;
        }

        Debug.Log(selection.Length + " objects scaled successfully!");
    }
}
