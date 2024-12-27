using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]
    private string SceneToPlay;
    // Start is called before the first frame update
    public void ToPlayScene()
        
    {
            SceneManager.LoadScene(SceneToPlay);
    }

    public void OnMouseDown()
    {
        Debug.Log($"Нажат на гроб/переход на сцену {SceneToPlay}");
        ToPlayScene();
    }
}
