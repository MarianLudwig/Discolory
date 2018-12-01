using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void StartLevel()
    {
        // save settings
        GameData.Instance.SaveSettings();

        // Load Scene with the Loading ID 1
        SceneManager.LoadScene(1);
    }
}
