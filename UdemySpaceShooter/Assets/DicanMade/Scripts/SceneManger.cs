using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{

    public void LoadGameScene(string sceneName) { SceneManager.LoadScene(sceneName); }
    
    //Habilitar esta linea cuando tenga GM
    //public void LoadLastLevel(){ SceneManager.LoadScene(FindObjectOfType<GameManager>().GetLasPlayedScene()) }

    public void LoadStartMenu() { SceneManager.LoadScene(0); }

    public void LoadGameOver() {  StartCoroutine(WaitAndLoadScreen());}

    public void QuitGame() { Application.Quit(); }

    IEnumerator WaitAndLoadScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

}
