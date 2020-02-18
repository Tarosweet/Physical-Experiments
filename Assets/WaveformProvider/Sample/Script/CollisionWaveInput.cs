using System;
using UnityEngine;

namespace Es.WaveformProvider.Sample
{
	/// <summary>
	/// Enter the waveform with a collision.
	/// </summary>
	[RequireComponent(typeof(Collider), typeof(Rigidbody))]
	public class CollisionWaveInput : MonoBehaviour
	{
		[SerializeField]
		private Texture2D waveform;

		[SerializeField, Range(0f, 1f)]
		private float inputScaleFitter = 0.01f;

		[SerializeField, Range(0f, 1f)]
		private float strength = 1f;

		private new Rigidbody rigidbody;

		private void Awake()
		{
			rigidbody = GetComponent<Rigidbody>();
		}

		private void OnTriggerEnter(Collider collision)
		{
			WaveInput(collision);
		}

		public void OnTriggerStay(Collider collision)
		{
			WaveInput(collision);
		}

		private void WaveInput(Collider collision)
		{
			var canvas = collision.GetComponent<WaveConductor>();
			
			if (canvas == null) 
				return;
			var p = collision.ClosestPoint(transform.position);
			canvas.Input(waveform, p, rigidbody.velocity.magnitude * rigidbody.mass * inputScaleFitter, strength);

		}
	}
}