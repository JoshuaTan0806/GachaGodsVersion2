#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CustomizableAnimeGirl
{
    [CustomEditor(typeof(AnimeGirlManager))]
    public class AnimeGirlManagerEditor : Editor
    {
        public override void OnInspectorGUI(){
            base.OnInspectorGUI();
            if(GUILayout.Button("Auto Detect Transforms")){
                Undo.RecordObject(target, "Auto Detect Transforms");
                AnimeGirlManager agm = target as AnimeGirlManager;
                agm.AutoDetectGameObjects();
                EditorUtility.SetDirty(agm);
            }
        }
    }
}
#endif