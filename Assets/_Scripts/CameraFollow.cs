using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Drag&Drop your following object.")]
    public Transform target;

    public bool animationMode;

    public float smoothedSpeed = 0.125f;


    private void FixedUpdate()
    {
        if (!animationMode)
        {
            Vector3 smoothedPosition = Vector3.Slerp(transform.position, target.position, smoothedSpeed);

            transform.position = smoothedPosition;

            if (GameManager.Instance.gameState == GameManager.GameState.Win)
            {
                smoothedSpeed = 0.07f;
            }
        }
    }

    private void LateUpdate()
    {
        if (animationMode)
        {
            Vector3 smoothedPosition = Vector3.Slerp(transform.position, target.position, smoothedSpeed);

            transform.position = smoothedPosition;
        }
    }
}
