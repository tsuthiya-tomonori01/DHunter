using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public PlayetScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetPlayerIsDead() == true)
        {
            LoadScene_GO();
        }
    }

    void CombatManager()
    {

    }

    public void LoadScene_GO()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void LoadScene_GR()
    {     
        SceneManager.LoadScene("CrearResultScene");
    }
}
