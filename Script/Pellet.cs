using UnityEngine;

//[RequireComponent (typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int point = 1;

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }


}
