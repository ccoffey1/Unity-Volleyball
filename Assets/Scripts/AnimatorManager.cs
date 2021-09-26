using System;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
	private Animator animator;
	private int horizontalId;
	private int verticalId;
	private int readyToSetId;
	private int setBallId;
	private int fullBodySetLayerId;
	private int partialBodySetLayerId;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		horizontalId = Animator.StringToHash("Horizontal");
		verticalId = Animator.StringToHash("Vertical");
		readyToSetId = Animator.StringToHash("ReadyToSet");
		setBallId = Animator.StringToHash("SetBall");
		fullBodySetLayerId = animator.GetLayerIndex("FB Set Layer");
		partialBodySetLayerId = animator.GetLayerIndex("PB Set Layer");
	}

	public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
	{
		// animation snapping
		float snappedHorizontal = SnapMovement(horizontalMovement);
		float snappedVertical = SnapMovement(verticalMovement);

		animator.SetFloat(horizontalId, snappedHorizontal, 0.1f, Time.deltaTime);
		animator.SetFloat(verticalId, snappedVertical, 0.1f, Time.deltaTime);
	}

	private float SnapMovement(float movementAmount)
	{
		float snappedMovement;

		if (movementAmount > 0 && movementAmount < 0.55f)
		{
			snappedMovement = 0.5f;
		}
		else if (movementAmount > 0.55f)
		{
			snappedMovement = 1;
		}
		else if (movementAmount < 0 && movementAmount > -0.55f)
		{
			snappedMovement = -0.5f;
		}
		else if (movementAmount < -0.55f)
		{
			snappedMovement = -1;
		}
		else
		{
			snappedMovement = 0;
		}

		return snappedMovement;
	}

	public void PrepareSetAnimation(bool readyToSet)
	{
		if (animator.GetBool(readyToSetId) != readyToSet)
			animator.SetBool(readyToSetId, readyToSet);
	}

	public void PlaySetBallAnimation()
	{
		animator.SetTrigger(setBallId);
	}
}
