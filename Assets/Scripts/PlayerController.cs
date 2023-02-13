using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
	[Header("Input Actions")]
	[SerializeField] InputAction movementAction;
	[SerializeField] InputAction fireAction;
	[SerializeField] InputAction pauseAction;

	[Header("Movement Settings")]
	[Tooltip("How fast player moves on x and y axis based on input")]
	[SerializeField] float speedControl = 30f;
	[Tooltip("Input sensivity (determines how fast inputs go from 0 to 1/-1)")]
	[SerializeField] float smoothInputSpeed = .1f; // Input sensivity
	[Tooltip("If input is in between this positive and negative value, input equals 0")]
	[SerializeField] float inputDeadZone = .001f;

	[Header("Movement Range")]
	[Tooltip("How far player moves horizontally")][SerializeField] float xRange = 10f;
	[Tooltip("How far player moves vertically")][SerializeField] float yRange = 7f;

	[Header("Laser Particles")]
	[SerializeField] GameObject[] lasers;

	[Header("Screen position based rotation")]
	[Tooltip("Rotation in x modifier calculated with player's position")]
	[SerializeField] float positionPitchFactor = 2f;
	[Tooltip("Rotation in y modifier calculated with player's position")]
	[SerializeField] float positionYawFactor = 2f;

	[Header("Input based rotation")]
	[Tooltip("Rotation in x modifier calculated with player's input")]
	[SerializeField] float controlPitchFactor = -20f;
	[Tooltip("Rotation in z modifier calculated with player's input")]
	[SerializeField] float controlRollFactor = -10f;

	[Header("Pause Settings")]
	[SerializeField] PlayableDirector playableDirector;
	[SerializeField] GameObject pauseGameObject;

	float xThrow, yThrow; // Input values	
	Vector2 currentInputVector;
	Vector2 smoothInputVelocity; // Just used to reference the currentInputVector smooth velocity

	AudioSource playerAudio;
	List<ParticleSystem> particleSystems = new List<ParticleSystem>();

	// Start is called before the first frame update
	void Start()
	{
		playerAudio = GetComponent<AudioSource>();

		GetParticleSystems();
	}


	void OnEnable()
	{
		movementAction.Enable();
		fireAction.Enable();
		pauseAction.Enable();
	}

	void OnDisable()
	{
		movementAction.Disable();
		fireAction.Disable();
		pauseAction.Disable();
	}

	// Update is called once per frame
	void Update()
	{
		SmoothController();
		ProcessTranslation();
		ProcessRotation();
		ProcessFiring();
		ProcessPause();
	}

	private void GetParticleSystems()
	{
		foreach (var laser in lasers)
		{
			particleSystems.Add(laser.GetComponent<ParticleSystem>());
		}
	}

	void ProcessTranslation()
	{
		float xOffset = xThrow * Time.deltaTime * speedControl;
		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

		float yOffset = yThrow * Time.deltaTime * speedControl;
		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}

	void SmoothController()
	{
		Vector2 throw_ = movementAction.ReadValue<Vector2>();
		currentInputVector = Vector2.SmoothDamp(currentInputVector, throw_, ref smoothInputVelocity, smoothInputSpeed);

		ProcessInputDeadZone();

		xThrow = currentInputVector.x;
		yThrow = currentInputVector.y;
	}

	void ProcessInputDeadZone()
	{
		if (currentInputVector.x < inputDeadZone && currentInputVector.x > -inputDeadZone)
			currentInputVector.x = 0;

		if (currentInputVector.y < inputDeadZone && currentInputVector.y > -inputDeadZone)
			currentInputVector.y = 0;
	}

	void ProcessRotation()
	{
		float pitchDueToPosition = transform.localPosition.y * -positionPitchFactor;
		float pitchDueToControlThrow = yThrow * controlPitchFactor;
		float pitch = pitchDueToPosition + pitchDueToControlThrow;

		float yawDueToPosition = transform.localPosition.x * positionYawFactor;
		float yaw = yawDueToPosition;

		float rollDueToControlThrow = xThrow * controlRollFactor;
		float roll = rollDueToControlThrow;

		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	void ProcessFiring()
	{
		bool fire = fireAction.ReadValue<float>() > 0.1f;

		if (fire)
		{
			SetLasersActive(true);

			if (!playerAudio.isPlaying) playerAudio.Play();
		}
		else
		{
			SetLasersActive(false);

			if (playerAudio.isPlaying) playerAudio.Stop();
		}
	}

	void SetLasersActive(bool isActive)
	{
		foreach (var laser in particleSystems)
		{
			var laserEmission = laser.emission;
			laserEmission.enabled = isActive;
		}
	}


	void ProcessPause()
	{
		bool pause = pauseAction.ReadValue<float>() > 0.1f;

		if (pause)
		{
			PauseGame();
		}
	}

	void PauseGame()
	{
		playableDirector.Pause();
		pauseGameObject.SetActive(true);
	}

	public void UnpauseGame()
	{
		playableDirector.Play();
		pauseGameObject.SetActive(false);
	}
}
