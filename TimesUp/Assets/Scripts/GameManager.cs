using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager manager;

	public Vector3 spawn;


	public Vector3 platform;
	public Vector3 platform2;
	public Vector3 platform3;

	public Vector3 enemy1;
	public Vector3 enemy2;
	public Vector3 enemy3;
	public Vector3 enemy4;

	public GameObject instr;
	public GameObject win;

	void Awake()
	{
		if (manager == null) {
			DontDestroyOnLoad (gameObject);
			manager = this;
		}
		else if (manager != this) {
			Destroy (gameObject);
		}

	}

	// Use this for initialization
	void Start () {
		spawn = new Vector3 (-7, 2.37f, 0);

		platform = new Vector3(23.24f,-4.08f);
		platform2 = new Vector3 (51.6f, 4.6f);
		platform3 = new Vector3 (64.1f, 3.2f);

		enemy1 = new Vector3(43, 0.08f);
		enemy2 = new Vector3(99.57f, 0.08f);
		enemy3 = new Vector3(64.4f, 0.08f);
		enemy4 = new Vector3(110.59f, 0.08f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			instr.gameObject.SetActive (false);

			if (checkIfLoaded ("Dystopia")) {
				SceneManager.UnloadSceneAsync ("Dystopia");
				SceneManager.LoadScene ("Utopia", LoadSceneMode.Additive);
			}
			else if (checkIfLoaded ("Utopia")) {
				SceneManager.UnloadSceneAsync ("Utopia");
				SceneManager.LoadScene ("Dystopia", LoadSceneMode.Additive);
			}
		}
	}

	public bool checkIfLoaded(string sceneName) {
		for(int i=0; i< UnityEditor.SceneManagement.EditorSceneManager.sceneCount; ++i) {
			var scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i);

			if(scene.name == sceneName) {
				return true; //the scene is already loaded
			}
		}
		//scene not currently loaded
		return false;
	}
}