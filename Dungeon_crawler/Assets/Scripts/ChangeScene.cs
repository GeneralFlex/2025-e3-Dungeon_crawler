using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public SceneAsset targetScene;
    public void Change()
    {
        SceneManager.LoadScene(targetScene.name);
    }
}
