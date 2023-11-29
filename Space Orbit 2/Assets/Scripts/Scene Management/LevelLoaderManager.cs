using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelLoaderManager : MonoBehaviour
{
    private const int GALAXY_ID = 1;

    private Vector3 _positionOnGalaxyMap;

    private bool _planetIsLoaded;

    [SerializeField] Animator _loadAnimator;

    List<Scene> _currentlyLoadedScenes = new List<Scene>();

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        LoadLandOnPlanet(1);
    }

    public void LoadLandOnPlanet(int planetID)
    {
        // _positionOnGalaxyMap = BasicMovement.Instance.transform.position;

        StartCoroutine(LoadingPlanetIEnumerator(planetID));
    }

    IEnumerator LoadingPlanetIEnumerator(int sceneID)
    {
        _planetIsLoaded = false;

        _loadAnimator.Play("Hide");
        yield return new WaitForSeconds(_loadAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Unload galaxy scene
        // SceneManager.UnloadSceneAsync(1);

        // Load specific planet
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);

        while(_planetIsLoaded == false)
        {
            yield return null;
        }

        _loadAnimator.Play("Show");
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _planetIsLoaded = true;

        if (scene.buildIndex > 3)
        {
            //custom logic if landing on a planet.
        }
    }
}
