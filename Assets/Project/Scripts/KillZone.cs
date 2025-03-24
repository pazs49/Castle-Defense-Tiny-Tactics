using UnityEngine;

public class KillZone : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Unit"))
    {
      Destroy(collision.gameObject);
    }
  }
}