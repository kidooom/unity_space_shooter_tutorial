using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
		public Vector3 fixPosition;
		public float speed;
		public GameObject player;
		public GameObject explosion;
		public GameObject playerExplosion;
		public GameObject hitBulletExplosion;
		private int maxHitpoint;
		public int hitpoint;
		public int hitScoreValue;
		public int destoryScoreValue;
		private GameController gameController;
		public GameObject shot;
		public GameObject headShotSpawn;
		public float headShotFireRate;
		private float nextFire;

		void Start()
		{
				GameObject gameControllerObject = GameObject.FindWithTag("GameController");
				if (gameControllerObject != null)
				{
						gameController = gameControllerObject.GetComponent <GameController>();
				}
				if (gameController == null)
				{
						Debug.Log("Cannot find GameController script");
				}
				maxHitpoint = hitpoint;
		}

		void OnTriggerEnter(Collider other)
		{
				if (other.tag == "Boundary" || other.tag == "Asteroid")
				{
						return;
				}

				if (other.tag == "Player")
				{
						Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
						gameController.GameOver();
						Destroy(other.gameObject);
				}

				if (other.tag == "Bullet")
				{
						Destroy(other.gameObject);
						gameController.AddScore(hitScoreValue);
						hitpoint = hitpoint - 1;

						if (hitpoint <= 0)
						{
								gameController.AddScore(destoryScoreValue);

								Instantiate(explosion, transform.position, transform.rotation);
								Destroy(gameObject);
								gameController.GameClear();
						}
						else
						{
								Instantiate(hitBulletExplosion, other.transform.position, other.transform.rotation);
						}
				}

		}

		void Update()
		{
				if (transform.position.z > fixPosition.z)
				{
						transform.position -= new Vector3(0, 0, 1) * speed * Time.deltaTime;
				}

				transform.Rotate(0, 1, 0);

				if (Time.time > nextFire)
				{

						if (hitpoint <= maxHitpoint / 4)
						{
								nextFire = Time.time + headShotFireRate / 4;
								// random 9WAY shot
								int randomRotate = Random.Range(-40, 40);
								for (float i = -80f; i <= 80f; i += 20f)
								{
										Instantiate(shot, headShotSpawn.transform.position, Quaternion.Euler(0, i + randomRotate, 0));
								}
								audio.Play();
						}
						else if (hitpoint <= maxHitpoint / 2)
						{
								nextFire = Time.time + headShotFireRate / 2;
								// random 7WAY shot
								int randomRotate = Random.Range(-40, 40);
								for (float i = -60f; i <= 60f; i += 20f)
								{
										Instantiate(shot, headShotSpawn.transform.position, Quaternion.Euler(0, i + randomRotate, 0));
								}
								audio.Play();
						}
						else
						{
								nextFire = Time.time + headShotFireRate;
								// random 3WAY shot
								int randomRotate = Random.Range(-20, 20);
								for (float i = -20f; i <= 20f; i += 20f)
								{
										Instantiate(shot, headShotSpawn.transform.position, Quaternion.Euler(0, i + randomRotate, 0));
								}
								audio.Play();
						}
				}
		}
}
