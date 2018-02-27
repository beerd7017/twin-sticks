using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public Transform Enemy;
	[Header("Wave Properties")]
	public float TimeBeforeSpawning = 1.5f;
	public float TimeBetweenEnemies = 0.25f;
	public float TimeBeforeWaves = 2.0f;

	public int EnemiesPerWave = 10;
	private int _currentNumberOfEnemies;

	[Header("User Interface")]
	private int _score;
	private int _waveNumber;

	public Text ScoreText;
	public Text WaveText;

	private void Start ()
	{
		StartCoroutine(SpawnEnemies());
	}

	private IEnumerator SpawnEnemies()
	{
		yield return new WaitForSeconds(TimeBeforeSpawning);

		while (true)
		{
			if (_currentNumberOfEnemies <= 0)
			{
				_waveNumber++;
				WaveText.text = "Wave: " + _waveNumber;
				for (int i = 0; i < EnemiesPerWave; i++)
				{
					float randDistance = Random.Range(10, 25);
					Vector2 randDirection = Random.insideUnitCircle;
					Vector3 enemyPos = transform.position;

					enemyPos.x += randDirection.x * randDistance;
					enemyPos.y += randDirection.y * randDistance;

					Instantiate(Enemy, enemyPos, transform.rotation);
					_currentNumberOfEnemies++;
					yield return new WaitForSeconds(TimeBetweenEnemies);
				}
			}
			yield return new WaitForSeconds(TimeBeforeWaves);
		}
	}

	public void KilledEnemy()
	{
		_currentNumberOfEnemies--;
	}

	public void IncreaseScore(int increase)
	{
		_score += increase;
		ScoreText.text = "Score: " + _score;
	}
}
