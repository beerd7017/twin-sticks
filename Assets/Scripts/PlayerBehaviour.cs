using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public float PlayerSpeed = 4.0f;
	private float _currentSpeed;
	private Vector3 _lastMovement;
	public Transform Laser;
	public float LaserDistance = 0.2f;
	public float TimeBetweenFires = 0.3f;
	private float _timeTilNextFire;
	public List<KeyCode> ShootButton;
	public AudioClip ShootSound;
	private AudioSource _audioSource;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update ()
	{
		foreach (KeyCode element in ShootButton)
		{
			if (!Input.GetKey(element) || !(_timeTilNextFire < 0)) continue;
			Debug.Log("Should Fire the laser.");
			_timeTilNextFire = TimeBetweenFires;
			ShootLaser();
			break;
		}

		_timeTilNextFire -= Time.deltaTime;
		Rotation();
		Movement();
	}

	private void Rotation()
	{
		Vector3 worldPos = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint(worldPos);

		float dx = transform.position.x - worldPos.x;
		float dy = transform.position.y - worldPos.y;
		float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

		Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));

		transform.rotation = rot;
	}

	private void Movement()
	{
		Vector3 movement = new Vector3();

		movement.x += Input.GetAxis("Horizontal");
		movement.y += Input.GetAxis("Vertical");

		movement.Normalize();

		if (movement.magnitude > 0)
		{
			_currentSpeed = PlayerSpeed;
			transform.Translate(movement * Time.deltaTime * PlayerSpeed, Space.World);
			_lastMovement = movement;
		}
		else
		{
			transform.Translate(_lastMovement * Time.deltaTime * _currentSpeed, Space.World);
			_currentSpeed *= 0.9f;
		}
	}

	private void ShootLaser()
	{
		_audioSource.PlayOneShot(ShootSound);
		Vector3 laserPos = transform.position;
		float rotationAngle = transform.localEulerAngles.z - 90;

		laserPos.x += Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * -LaserDistance;
		laserPos.y += Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * -LaserDistance;

		Instantiate(Laser, laserPos, transform.rotation);
	}
}
