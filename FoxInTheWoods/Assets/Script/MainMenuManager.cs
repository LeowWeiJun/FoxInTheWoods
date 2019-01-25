using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator SettingPanel;
    public Animator MainMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        SettingPanel = SettingPanel.GetComponent<Animator>();
        MainMenuPanel = MainMenuPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSetting()
    {
        MainMenuPanel.enabled = true;
        SettingPanel.enabled = true;
        MainMenuPanel.SetBool("isHidden", true);
        SettingPanel.SetBool("isHidden", false);
        
    }

    public void CloseSetting()
    {
        MainMenuPanel.SetBool("isHidden", false);
        SettingPanel.SetBool("isHidden", true);  
    }
}
