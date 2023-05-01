using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button startGameButton;

    public void StartGameButtonFalse()
    {
        startGameButton.gameObject.SetActive(false);
    }

}
