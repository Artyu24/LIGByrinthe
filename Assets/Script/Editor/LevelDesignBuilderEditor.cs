using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

[CanEditMultipleObjects]
public class LevelDesignBuilderEditor : EditorWindow
{
    private LevelNumberState levelChoose = LevelNumberState.ONE;

    private int nbrWall = 0;
    private int posX = 0;
    private int posY = 0;
    private int posZ = 0;
    private bool isVertical = true;

    private int idTemp = 0;
    private int posXTemp = 0;
    private int posYTemp = 0;
    private int posZTemp = 0;
    private bool verticalTemp = true;
    private List<GameObject> allWallTemp = new List<GameObject>();

    [MenuItem("Tools/Level Builder")]
    static void InitWindow()
    {
        LevelDesignBuilderEditor window = GetWindow<LevelDesignBuilderEditor>();
        window.titleContent = new GUIContent("Level Builder");
        window.Show();
    }

    private void Awake()
    {
        idTemp = (int) levelChoose;
        posXTemp = posX;
        posYTemp = posY;
        posZTemp = posZ;
        verticalTemp = isVertical;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("-------Level Builder-------");

        WallManager wallManager = FindWallManagerInScene();
        if (wallManager == null)
        {
            EditorGUILayout.HelpBox("MISSING WALL MANAGER IN SCENE", MessageType.Error);
            EditorGUILayout.EndVertical();
            return;
        }

        GameObject wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Wall.prefab");
        if (wallPrefab == null)
        {
            EditorGUILayout.HelpBox("MISSING WALL PREFAB", MessageType.Error);
            EditorGUILayout.EndVertical();
            return;
        }

        Material wallMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Material/Wall.mat");
        if (wallMat == null)
        {
            EditorGUILayout.HelpBox("MISSING WALL MATERIAL", MessageType.Error);
            EditorGUILayout.EndVertical();
            return;
        }

        levelChoose = (LevelNumberState)EditorGUILayout.EnumPopup(levelChoose);

        nbrWall = EditorGUILayout.IntField("Size Wall", nbrWall);

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 50f;
        posX = EditorGUILayout.IntField("X:", posX, GUILayout.ExpandWidth(false));
        posY = EditorGUILayout.IntField("Y:", posY, GUILayout.ExpandWidth(false));
        posZ = EditorGUILayout.IntField("Z:", posZ, GUILayout.ExpandWidth(false));
        EditorGUILayout.EndHorizontal();
        
        if (isVertical)
        {
            if (GUILayout.Button("Horizontal"))
            {
                isVertical = false;
            }
        }
        else
        {
            if (GUILayout.Button("Vertical"))
            {
                isVertical = true;
            }
        }

        EditorGUILayout.EndVertical();

        if (nbrWall <= 0)
        {
            foreach (GameObject wallTemp in allWallTemp)
                DestroyImmediate(wallTemp);

            allWallTemp.Clear();
            return;
        }

        if (nbrWall != allWallTemp.Count || idTemp != (int)levelChoose || verticalTemp != isVertical || posXTemp != posX || posYTemp != posY || posZTemp != posZ)
        {
            idTemp = (int)levelChoose;
            posXTemp = posX;
            posYTemp = posY;
            posZTemp = posZ;
            verticalTemp = isVertical;

            foreach (GameObject wallTemp in allWallTemp)
                DestroyImmediate(wallTemp);
            
            allWallTemp.Clear();

            int xTemp = posX;
            int zTemp = posZ;

            for (int i = 0; i < nbrWall; i++)
            {
                if (isVertical)
                    zTemp += 1;
                else
                    xTemp += 1;

                GameObject newWallTemp = Instantiate(wallPrefab, new Vector3(xTemp, posY, zTemp), Quaternion.identity, wallManager.transform.GetChild(idTemp));
                allWallTemp.Add(newWallTemp);
            }
        }

        if (GUILayout.Button("Confirm"))
        {
            foreach (GameObject wallTemp in allWallTemp)
            {
                wallTemp.GetComponent<MeshRenderer>().material = wallMat;
                wallManager.allWalls.Add(wallTemp.GetComponent<WallObject>());
            }
            allWallTemp.Clear();
        }
    }

    public static WallManager FindWallManagerInScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (!scene.IsValid())
            return null;

        GameObject[] rootGameObjects = scene.GetRootGameObjects();
        foreach (GameObject obj in rootGameObjects)
        {
            if (!obj.activeInHierarchy)
                continue;

            WallManager WM = obj.GetComponent<WallManager>();
            if (WM != null) 
                return WM;
        }

        return null;
    }
}
