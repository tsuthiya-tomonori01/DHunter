using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    //�ݒ�UI�t���O
    //bool SettingActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Setting()
    {
        //SettingActive = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
