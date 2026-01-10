using UnityEngine;

public class CameraController : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private Transform player;

   [Header("Settings")]
   [SerializeField] private Vector3 camOffset;

   private void Awake()
   {
      GetComponents();
   }

   private void GetComponents()
   {
      if (!player)
         player = GameObject.Find("Player").GetComponent<Transform>();
   }

   private void LateUpdate()
   {
      transform.position = player.transform.position + camOffset;
   }
}
