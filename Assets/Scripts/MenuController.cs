using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerNP : MonoBehaviour {

	public void	LoadScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
}
