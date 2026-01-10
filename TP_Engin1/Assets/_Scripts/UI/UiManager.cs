using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager _gameManager;
    
    [Header("UI Stuff")]
    [SerializeField] private TMP_Text playerPointsText;
    private void Update()
    {
        UpdatePlayerPoints();
    }

    private void UpdatePlayerPoints()
    {
        playerPointsText.text = GameManager.Instance.GetPlayerPoints().ToString();
    }
}
