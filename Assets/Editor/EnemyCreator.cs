using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class EnemyCreator : EditorWindow
{
    private string enemyName = "New Enemy";

    private GameObject prefab;  

    private float health = 100f;

    private float damage = 10f; 

    private float movementSpeed = 5f;

    private int xpGained = 10; 

    private Sprite enemySprite;

    private bool isBoss = false;
    private bool canShoot = false;

    NavMeshAgent agent;


    [MenuItem("Tools/Enemy Creator")]
    public static void ShowWindow()
    {
        GetWindow<EnemyCreator>("Enemy Creator");
    }

    private void OnGUI()
    {
       GUILayout.Label("Create a New Enemy", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        DrawGeneralSection();

        EditorGUILayout.Space(10);

        DrawStatsSection();

        EditorGUILayout.Space(10);

        DrawRewardsSection();

        EditorGUILayout.Space(10);

        DrawBooleansSection();

        EditorGUILayout.Space(10);

        DrawSpriteSection();

        EditorGUILayout.Space(10);

        DrawAgentSection();

        EditorGUILayout.Space(10);

        DrawPrefabSection();

        EditorGUILayout.Space(10);

        DrawValidationSection();

        EditorGUILayout.Space(20);

        DrawCreateButton();

    }


    private void CreateEnemy()
    {
        if(!ValidateEnemy())
        {
            return;
        }

        CreateFolders(); 

        EnemyData newEnemy = CreateInstance<EnemyData>();

        newEnemy.name = enemyName;

        newEnemy.Health = health;

        newEnemy.Damage = damage;

        newEnemy.Speed = movementSpeed;

        newEnemy.XpGained = xpGained;

        newEnemy.IsBoss = isBoss;

        newEnemy.IsShootingEnemy = canShoot;

        newEnemy.enemySprite = enemySprite;
        
        newEnemy.agent = agent;

        string path = $"Assets/Scripts/Enemy/Data/{enemyName}.asset";

        path = AssetDatabase.GenerateUniqueAssetPath(path);



        AssetDatabase.CreateAsset(newEnemy, path);

        GameObject enemyObject = new GameObject(enemyName);

        EnemyContacts enemyContact = enemyObject.AddComponent<EnemyContacts>();

        enemyContact.SetData(newEnemy);

        NavMeshAgent _agent = enemyObject.AddComponent<NavMeshAgent>();

        _agent.speed = movementSpeed; 

        enemyObject.AddComponent<EnemyMovement>();

        BoxCollider2D _collider = enemyObject.AddComponent<BoxCollider2D>();

        _collider.isTrigger = true;

        Rigidbody2D rb2D = enemyObject.AddComponent<Rigidbody2D>();

        rb2D.gravityScale = 0;

        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        rb2D.sleepMode = RigidbodySleepMode2D.NeverSleep;

        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;

        SpriteRenderer spriteRenderer = enemyObject.AddComponent<SpriteRenderer>();

        spriteRenderer.sprite = enemySprite;

        enemyObject.tag = "Enemy";

        if(canShoot) enemyObject.AddComponent<EnemyShoot>();

        string prefabPath = $"Assets/Scripts/Enemy/Prefabs/{enemyName}.prefab";

        GameObject _createdPrefab = PrefabUtility.SaveAsPrefabAsset(enemyObject, prefabPath);

        newEnemy.prefab = _createdPrefab;

        EditorUtility.SetDirty(newEnemy);

        DestroyImmediate(enemyObject);

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();

        Selection.activeObject = newEnemy;
    }

    private void DrawGeneralSection()
    {
        GUILayout.Label(
          "General",
          EditorStyles.boldLabel
      );

        enemyName = EditorGUILayout.TextField(
            "Enemy Name",
            enemyName
        );

        prefab = (GameObject)EditorGUILayout.ObjectField(
            "Visual Prefab",
            prefab,
            typeof(GameObject),
            false
        );
    }

    private void DrawStatsSection()
    {
        GUILayout.Label(
            "Stats",
            EditorStyles.boldLabel
        );
        health = EditorGUILayout.FloatField(
            "Health",
            health
        );
        damage = EditorGUILayout.FloatField(
            "Damage",
            damage
        );
        movementSpeed = EditorGUILayout.FloatField(
            "Movement Speed",
            movementSpeed
        );
    }

    private void DrawRewardsSection()
    {
        GUILayout.Label(
            "Rewards",
            EditorStyles.boldLabel
        );
        xpGained = EditorGUILayout.IntField(
            "XP Gained",
            xpGained
        );
    }

    private void DrawBooleansSection()
    {
        GUILayout.Label(
            "Booleans",
            EditorStyles.boldLabel
        );
        isBoss = EditorGUILayout.Toggle(
            "Is Boss",
            isBoss
        );
        canShoot = EditorGUILayout.Toggle(
            "Can Shoot",
            canShoot
        );
    }

    private void DrawSpriteSection()
    {
        GUILayout.Label(
            "Enemy Sprite",
            EditorStyles.boldLabel
        );
        enemySprite = (Sprite)EditorGUILayout.ObjectField(
            "Enemy Sprite",
            enemySprite,
            typeof(Sprite),
            false
        );
    }

    private void DrawAgentSection()
    {
        GUILayout.Label(
            "NavMeshAgent",
            EditorStyles.boldLabel
        );
        agent = (NavMeshAgent)EditorGUILayout.ObjectField(
            "NavMeshAgent Component",
            agent,
            typeof(NavMeshAgent),
            true
        );
    }

    private void DrawPrefabSection()
    {
        GUILayout.Label(
            "Prefab",
            EditorStyles.boldLabel
        );
        prefab = (GameObject)EditorGUILayout.ObjectField(
            "Enemy Prefab",
            prefab,
            typeof(GameObject),
            false
        );
    }

    private void DrawValidationSection()
    {
        GUILayout.Label(
            "Validation",
            EditorStyles.boldLabel
        );
        if (GUILayout.Button("Validate Enemy"))
        {
            if (ValidateEnemy())
            {
                EditorUtility.DisplayDialog("Validation Successful", "The enemy data is valid.", "OK");
            }
        }
    }

    private void DrawCreateButton()
    {
        EditorGUILayout.Space(20);
        if (GUILayout.Button("Create Enemy"))
        {
            CreateEnemy();
        }
    }

    private bool ValidateEnemy()
    {
       if(string.IsNullOrWhiteSpace(enemyName))
       {
            EditorUtility.DisplayDialog("Error", "Enemy name cannot be empty.", "OK");

            return false; 
       }

       if(health <= 0)
       {
            EditorUtility.DisplayDialog("Error", "Enemy health must be a positive value.", "OK");

            return false;
       }

       if(damage < 0)
       {
            EditorUtility.DisplayDialog("Error", "Enemy damage must be a non-negative value.", "OK");

            return false;
       }

       if(movementSpeed < 0)
       {
            EditorUtility.DisplayDialog("Error", "Enemy movement speed must be a non-negative value.", "OK");

            return false;
       }

       if (xpGained < 0)
       {
            EditorUtility.DisplayDialog("Error", "XP gained must be a non-negative value.", "OK");
            return false;
       }

       if(enemySprite == null)
       {
            EditorUtility.DisplayDialog("Error", "Enemy sprite cannot be null.", "OK");
            return false;
       }

       if(agent == null)
        {
            EditorUtility.DisplayDialog("Error", "NavMeshAgent component is required for the enemy.", "OK");
            return false;
        }

        return true; 
    }

    private void CreateFolders()
    {
        if(!AssetDatabase.IsValidFolder("Assets/Scripts/Enemy/ScriptableObjs"))
        {
            AssetDatabase.CreateFolder("Assets/Scripts/Enemy", "ScriptableObjs");
        }

        if (!AssetDatabase.IsValidFolder("Assets/Scripts/Enemy/Prefabs"))
        {
            AssetDatabase.CreateFolder("Assets/Scripts/Enemy", "Prefabs");
        }

        if(!AssetDatabase.IsValidFolder("Assets/Scripts/Enemy/Data"))
        {
            AssetDatabase.CreateFolder("Assets/Scripts/Enemy", "Data");
        }

    }
}
