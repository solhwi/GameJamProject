using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingButton : MonoBehaviour
{
    public void OnClickMoveToMainScene()
    {
        SceneManager.Instance.ChangeScene(SceneManager.ENUM_SCENE.Main);
        Debug.Log("Main Scene 으로 이동");
    }

}
