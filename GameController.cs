using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
		public GameObject hazard;
		public Vector3 spawnValues;
		public GameObject boss;
		public int hazardCount;
		public float startWait;
		public float spawnWait;
		public float waveWait;
		public int waveCount;
		public GUIText scoreText;
		public GUIText highScoreText;
		public GUIText restartText;
		public GUIText gameOverText;
		public GUIText gameClearText;
		public GameObject clearEffect;
		private bool gameOver;
		private bool restart;
		private int score;

		void Start()
		{
				score = 0;
				UpdateScore();

				highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
				gameOverText.text = "";
				restartText.text = "";
				gameClearText.text = "";

				gameOver = false;
				restart = false;
				StartCoroutine(SpawnWaves());
		}

		void Update()
		{
				if (restart)
				{
						if (Input.GetKeyDown(KeyCode.R))
						{
								Application.LoadLevel(Application.loadedLevel);
						}
				}
		}

		IEnumerator SpawnWaves()
		{
				yield return new WaitForSeconds(startWait);
				for (int j = 0; j < waveCount; j++)
				{
						for (int i = 0; i < hazardCount; i++)
						{
								Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
								Quaternion spawnRotation = Quaternion.identity;
								Instantiate(hazard, spawnPosition, spawnRotation);
								yield return new WaitForSeconds(spawnWait);
						}
						if (gameOver)
						{
								break;
						}
						yield return new WaitForSeconds(waveWait);
				}

				Instantiate(boss);

		}

		public void AddScore(int addScoreValue)
		{
				score += addScoreValue;
				UpdateScore();
		}

		void UpdateScore()
		{
				scoreText.text = "Score: " + score;
				int pastHighScore = PlayerPrefs.GetInt("HighScore");
				if (score > pastHighScore)
				{
						PlayerPrefs.SetInt("HighScore", score);
						highScoreText.text = "HighScore: " + score;
				}
		}

		public void GameOver()
		{
				gameOver = true;
				gameOverText.text = "Game Over";
				restart = true;
				restartText.text = "Press 'R' is restart";
		
				int pastHighScore = PlayerPrefs.GetInt("HighScore");
				if (score > pastHighScore)
				{
						PlayerPrefs.SetInt("HighScore", score);
				}
		}

		public void GameClear()
		{
				gameClearText.text = "The End";
				Instantiate(clearEffect, new Vector3(0, 0, 5), new Quaternion(0, 0, 0, 0));
				gameOver = true;
				restart = true;
				restartText.text = "Press 'R' is restart";
		}
}
