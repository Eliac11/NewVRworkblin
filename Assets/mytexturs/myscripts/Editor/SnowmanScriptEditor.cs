using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(SnowManScript))]
public class SnowmanScriptEditor : Editor
{
    private SnowManScript script;

    private void OnEnable()
    {
        script = (SnowManScript)target;
    }


    public override void OnInspectorGUI()
    {
        script.lives = EditorGUILayout.FloatField("Жизки короче", script.lives);
        script.Target = (GameObject)EditorGUILayout.ObjectField("за кем бегать",script.Target, typeof(GameObject), true);

        script.isEvil = EditorGUILayout.Toggle("злой", script.isEvil);


        if (script.isEvil)
        {
            script.sspoint = (GameObject)EditorGUILayout.ObjectField("где снежки спавнить", script.sspoint, typeof(GameObject), true);
            script.psnow = (GameObject)EditorGUILayout.ObjectField("префаб снежка", script.psnow, typeof(GameObject), true);

            script.ballspeed = EditorGUILayout.FloatField("скорость снежка", script.ballspeed);
            script.firespeed = EditorGUILayout.FloatField("частота пиупиупиу", script.firespeed);
        }





        if (GUI.changed)
        {
            EditorUtility.SetDirty(script.gameObject);
            EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
        }
    }
}
