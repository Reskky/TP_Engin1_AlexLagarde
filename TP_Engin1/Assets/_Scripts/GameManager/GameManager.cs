using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   [SerializeField] private PlayerController _playerController;

   private void Awake()
   {
      InitGameManager();
   }

   private void InitGameManager()
   {
      if (Instance && Instance != this)
         Destroy(gameObject);
      Instance = this;
      DontDestroyOnLoad(gameObject);
   }

   public int GetPlayerPoints()
   {
      return _playerController.PlayerPoints;
   }


}
