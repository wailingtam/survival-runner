using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayManager : MonoBehaviour {
	
	public static int score;
	public static int stars;
	public static int teddies;
	public static float distance;
	public Text scoreText;
	public Text starsText;
	public Text teddiesText;
	public Text distanceText;

	void Awake ()
	{
		score = 0;
		stars = 0;
		teddies = 0;
		distance = 0;
	}
	
	
	void Update ()
	{
		scoreText.text = score.ToString();
		starsText.text = stars.ToString();
		teddiesText.text = teddies.ToString();
		distanceText.text = ((int)distance).ToString() + " m";
	}
}
