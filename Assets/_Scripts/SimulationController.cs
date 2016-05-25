using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script to control some of the propierties of the game. 
public class SimulationController : MonoBehaviour
{
    public GameObject controlsPanel;
	public GameObject difficultyPanel;
	public Text actualDifficulty;

	public static int difficulty;

	public void Start() {
        actualDifficulty.text = "Actual Difficulty: " + GetDefaultDifficulty();
    }

	public void Update() {
        actualDifficulty.text = "Actual Difficulty: " + getDifficulty();
    }

    //Function called when we want to enter the game.
    public void StartGame()
    {
        SceneManager.LoadScene("Universe", LoadSceneMode.Single);
    }

    public void ShowControls()
    {
		controlsPanel.SetActive(!controlsPanel.activeSelf);
		difficultyPanel.SetActive (false);
		if (!controlsPanel.activeSelf) {
			GetComponent<AudioSource>().Play ();
		}
    }

	public void ShowDifficulty()
	{
		difficultyPanel.SetActive(!difficultyPanel.activeSelf);
		controlsPanel.SetActive (false);
		if (!difficultyPanel.activeSelf) {
			GetComponent<AudioSource>().Play ();
		}
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    // Implemented method but since onClick() can pass only 0 or 1 parameters, let it stay here
    public void SetDifficulty(Button difficultyButton, int difficultyLevelInt)
    {
        GetComponent<AudioSource>().Play();
        Text difficultyLevel = difficultyButton.transform.FindChild("Text").GetComponent<Text>();
        difficulty = difficultyLevelInt;
        actualDifficulty.text = "Actual Difficulty: " + difficultyLevel.text;
    }

    // For the future improvements, e.g. if we have a data about our players and their prefered game settings
    // Currently hard-coded to the value "Normal"
    public string GetDefaultDifficulty()
    {
        difficulty = 3;
        string defaultDifficulty = "Normal";
        return defaultDifficulty;
    }

    public string getDifficulty (){

		string d = "";

		switch (difficulty) {
		case 1:
			d = "Very Easy";
			break;
		case 2:
			d = "Easy";
			break;
		case 3:
			d = "Normal";
			break;
		case 4: 
			d = "Hard";
			break;
		case 5:
			d = "Very Hard";
			break;
		default:
			d = "No Specified";
			break;
		}

		return d;
	}

	public void setVeryEasy() {GetComponent<AudioSource>().Play ();difficulty = 1;}
	public void setEasy() {GetComponent<AudioSource>().Play ();difficulty = 2;}
	public void setNormal() {GetComponent<AudioSource>().Play ();difficulty = 3;}
	public void setHard() {GetComponent<AudioSource>().Play ();difficulty = 4;}
	public void setVeryHard() {GetComponent<AudioSource>().Play ();difficulty = 5;}

}
