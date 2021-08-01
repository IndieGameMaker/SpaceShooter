using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬을 호출하기 위한 네임스페이스

public class UIManager : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        Debug.Log("버튼 클릭");
        SceneManager.LoadScene("Play");
    }
}
