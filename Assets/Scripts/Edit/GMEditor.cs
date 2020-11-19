#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(GameManager))]
public class GMEditor : Editor
{
    private GameManager gm;

    public void OnEnable()
    {
        gm = (GameManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (gm.dataBase.Count > 0)
        {
            foreach (DataBase dataBase in gm.dataBase)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                dataBase.img = EditorGUILayout.ObjectField(dataBase.img, typeof(Sprite), false, GUILayout.Width(60), GUILayout.Height(60)) as Sprite;
                
                EditorGUILayout.BeginVertical();
                dataBase.id = EditorGUILayout.IntField("ID", dataBase.id);
                dataBase.name = EditorGUILayout.TextField("Имя",dataBase.name);
                dataBase.prifab = (GameObject)EditorGUILayout.ObjectField("Прифаб", dataBase.prifab, typeof(GameObject), true);
                dataBase.button = (GameObject)EditorGUILayout.ObjectField("Кнопка",dataBase.button, typeof(GameObject), true);
                dataBase.panelTown = (GameObject)EditorGUILayout.ObjectField("Панель",dataBase.panelTown, typeof(GameObject), true);
                
                dataBase.price = EditorGUILayout.FloatField("Стоимость", dataBase.price);
                
                dataBase.countWood = EditorGUILayout.FloatField("Дерево",dataBase.countWood);
                dataBase.countStone = EditorGUILayout.FloatField("Камень",dataBase.countStone);
                dataBase.countMetal = EditorGUILayout.FloatField("Металл",dataBase.countMetal);EditorGUILayout.EndVertical();
                
                if (GUILayout.Button("Delete", GUILayout.Width(60), GUILayout.Height(60)))
                {
                    gm.dataBase.Remove(dataBase);
                    break;
                }
                
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
        else EditorGUILayout.LabelField("no elements");

        if (GUILayout.Button("Add", GUILayout.Height(30)))
            gm.dataBase.Add(new DataBase());

        if (GUI.changed)
            SetObjectDirty(gm.gameObject);
    }
    
    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}
#endif