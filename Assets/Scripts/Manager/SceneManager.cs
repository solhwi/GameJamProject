using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
	public enum ENUM_SCENE
	{
		Main = 0,
		Stage = 1,
	}

	BaseScene currScene;

	protected override void OnAwakeInstance()
	{
        InitializeScene();
    }

    private void InitializeScene()
	{
        currScene = FindObjectOfType<BaseScene>();
    }

	public void OnAwakeStageCallbackRegister(Action callback)
	{
		currScene.OnAwakeStageCallbackRegister(callback);
	}

	public void OnStartStageCallbackRegister(Action callback)
	{
		currScene.OnStartStageCallbackRegister(callback);
	}

	public void OnEndStageCallbackRegister(Action callback)
	{
		currScene.OnEndStageCallbackRegister(callback);
	}
	public void OnAwakeStageCallbackUnegister(Action callback)
	{
		currScene.OnAwakeStageCallbackUnegister(callback);
	}

	public void OnStartStageCallbackUnregister(Action callback)
	{
		currScene.OnStartStageCallbackUnregister(callback);
	}

	public void OnEndStageCallbackUnregister(Action callback)
	{
		currScene.OnEndStageCallbackUnregister(callback);
	}

	public void ChangeScene(ENUM_SCENE scene, Action<float> onSceneLoad = null)
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += LoadSceneEnd;
        StartCoroutine(OnLoadSceneCoroutine(scene, onSceneLoad));
    }

    private IEnumerator OnLoadSceneCoroutine(ENUM_SCENE scene, Action<float> onSceneLoad)
    {
        var asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;

		currScene?.End();

		while (asyncOperation.progress < 0.9f)
        {
            yield return null;

            onSceneLoad?.Invoke(asyncOperation.progress);
        }

        asyncOperation.allowSceneActivation = true;
    }

    private void LoadSceneEnd(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        InitializeScene();
    }
}
