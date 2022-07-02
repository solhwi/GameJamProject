using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundPack.BGMTag.stage3);
        
    }
    //������ ��(OnClick) Stage Scene ���� �̵�(Load Scene)
    public void OnClickMoveToStageScene()
    {
        SceneManager.Instance.ChangeScene(SceneManager.ENUM_SCENE.Stage);
        Debug.Log("Stage Scene ���� �̵�");
    }
}
