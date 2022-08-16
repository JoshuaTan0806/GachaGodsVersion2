#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomizableAnimeGirl {
    [CustomEditor (typeof (CustomizeManager))]
    public class CustomizeManagerEditor : Editor {
        public override void OnInspectorGUI () {
            base.OnInspectorGUI ();
            if (GUILayout.Button ("Auto Detect Meshes")) {
                Undo.RecordObject(target, "Auto Detect Meshes");
                CustomizeManager cm = target as CustomizeManager;
                cm.AutoDetectMeshes ();
                EditorUtility.SetDirty(cm);
            }
            if (GUILayout.Button ("Auto Detect Materials")) {
                Undo.RecordObject(target, "Auto Detect Materials");
                CustomizeManager cm = target as CustomizeManager;
                cm.AutoDetectMaterials ();
                EditorUtility.SetDirty(cm);
            }
            if (GUILayout.Button ("Init Equipments In Scene")) {
                Undo.RecordObject(target, "Init Equipment In Scene");
                CustomizeManager cm = target as CustomizeManager;
                cm.InitEquipmentInScene ();
                EditorUtility.SetDirty(cm);
            }
        }
    }
}
#endif