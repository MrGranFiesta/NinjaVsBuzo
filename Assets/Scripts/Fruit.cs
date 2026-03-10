using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject))
        {
            SoundConst.EatFruit.Play();
            Destroy(gameObject);
        }
    }
}
