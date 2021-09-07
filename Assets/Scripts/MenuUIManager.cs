using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{
    public string nameInput;
    public void StartButtonPressed()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.LoadData();
    }
    public void QuitButtonPressed()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void NameInput(string name)
    {
        nameInput = name;
        Debug.Log(nameInput);
    }
}
