using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}

    public List<GameObject> obstaclePrefabs;


    public float obstacleInterval = 1;

    public float obstacleSpeed = 10;

    public Vector2 obstacleOffsetY = new Vector2(0,0);

    public float obstacleOffsetX = 0;

    [HideInInspector]
    public int score;

    [HideInInspector]
    private bool isGameOver = false;
    

    void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
    }

    public bool IsGameActive(){
        return !isGameOver;
    }

    public bool IsGameOver(){
        return isGameOver;
    }

    public void EndGame(){
        isGameOver = true;

        Debug.Log("Game Over... Your Score Was: " + score);

        //Reload Scene
        StartCoroutine(ReloadScene(2));
    }

    private IEnumerator ReloadScene(float delay){
        // Esperar 2 segundo (delay)
        yield return new WaitForSeconds(delay);
    
        string sceneName = SceneManager.GetActiveScene().name;
        //Recarregar a cena
        SceneManager.LoadScene(sceneName); 
        Debug.Log("Reload scene please");
    }
    
}
