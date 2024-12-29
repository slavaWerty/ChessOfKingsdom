using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using System.Collections;

public class ButtonChangeScene 
{
    private Button _button;
    private int _sceneIndex;

    [Inject]
    public ButtonChangeScene(Button button, int sceneIndex)
    {
        _button = button;
        _sceneIndex = sceneIndex;
    }
    
    public void SetSceneByIndex()
    {
        _button.onClick.AddListener(() => _button.StartCoroutine(OpenMenuScene(1f)));
    }

    private IEnumerator OpenMenuScene(float seconds)
    {
        WaitForSeconds wait = new WaitForSeconds(seconds);

        yield return wait;

        SceneManager.LoadScene(_sceneIndex);
    }
}
