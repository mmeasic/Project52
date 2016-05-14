using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimulationUIController : MonoBehaviour
{

    //Player UI
    public Text info_energy;
    public Text info_lifes;
    public Text info_speed;
    public Text score_number;
    public Text timer_number;
    public Slider energy_slider;

    //Game Over UI
    public GameObject game_over_panel;

    //Pause UI
    public GameObject pause_panel;

    public void refreshPlayerInfo(float current_energy, float current_number_of_lifes, float current_speed)
    {
        info_energy.text = "Energy: " + ((int)current_energy).ToString() + "%";
        info_lifes.text = "Lifes: " + current_number_of_lifes.ToString();
        info_speed.text = "Speed: " + ((int)current_speed).ToString();
        energy_slider.normalizedValue = (((int)current_energy) / 100.0f);
    }

    public void refreshScore(int score)
    {
        score_number.text = score.ToString();
    }

    public void refreshTimer(int timer)
    {
        timer_number.text = timer.ToString();
    }

    public void showPausePanel()
    {
        pause_panel.SetActive(true);
    }

    public void hidePausePanel()
    {
        pause_panel.SetActive(false);
    }

    public void showGameOverPanel()
    {
        game_over_panel.SetActive(true);
    }

    public void hideGameOverPanel()
    {
        game_over_panel.SetActive(false);
    }
}
