using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private void Start()
    {
        
    }
    //눌렀을 때(OnClick) Stage Scene 으로 이동(Load Scene)
    public void OnClickMoveToStageScene()
    {
        SceneManager.Instance.ChangeScene(SceneManager.ENUM_SCENE.Stage);
        Debug.Log("Stage Scene 으로 이동");
    }
}
