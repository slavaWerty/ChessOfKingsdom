using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class MainEntryPoint : IInitializable
{
    private MonoBehaviour _mono;

    [Inject]
    MainEntryPoint(MonoBehaviour mono)
    {
        _mono = mono;
    }

    public void Initialize()
    {
        _mono.StartCoroutine(OpenMenuScene(2f));
    }


    private IEnumerator OpenMenuScene(float seconds)
    {
        WaitForSeconds wait = new WaitForSeconds(seconds);

        yield return wait;

        SceneManager.LoadScene(1);
    }
}
