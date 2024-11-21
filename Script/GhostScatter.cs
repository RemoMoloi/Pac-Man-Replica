using UnityEngine;

public class GhostScatter : GhostBehaviour
{

    private void OnDisable()
    {
        if (this.ghost != null && this.ghost.chase != null)
        {
            this.ghost.chase.Enable();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections.Count > 1 && node.availableDirections[index] == -ghost.movement.direction )
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
