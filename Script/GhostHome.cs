using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    public Transform outside;
    public Transform inside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("obstacle"))
        {
            if (ghost != null && ghost.movement != null)
            {
                ghost.movement.SetDirection(-ghost.movement.direction);
            }
            else
            {
                Debug.LogWarning("Ghost or its movement component is null in OnCollisionEnter2D.");
            }
        }
    }

    private IEnumerator ExitTransition()
    {
        if (ghost != null && ghost.movement != null)
        {
            ghost.movement.SetDirection(Vector2.up, true);
            ghost.movement.rigidbody.isKinematic = true;
            ghost.movement.enabled = false;

            //...
            Vector3 position = transform.position;

            float duration = 0.5f;
            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                Vector3 newPosition = Vector3.Lerp(position, inside.position, elapsed / duration);
                newPosition.z = position.z;
                ghost.transform.position = newPosition;
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0.0f;

            while (elapsed < duration)
            {
                Vector3 newPosition = Vector3.Lerp(inside.position, outside.position, elapsed / duration);
                newPosition.z = position.z;
                ghost.transform.position = newPosition;
                elapsed += Time.deltaTime;
                yield return null;
            }

            ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
            ghost.movement.rigidbody.isKinematic = false;
            ghost.movement.enabled = true;
        }
        else
        {
            Debug.LogWarning("Ghost or its movement component is null in ExitTransition coroutine.");
        }
    }
}
