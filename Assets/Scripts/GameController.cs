using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;

    private Camera _currentCamera;
    private BlackoutScript _currentBlackout;

    void Start()
    {
        GameStart();
    }

    private void GameStart()
    {


        //End of startgame animation
        ChangeCamera(_playerCamera);
    }

    private void ChangeCamera(Camera newCamera)
    {

        _currentBlackout.OnOff(2f);
        _currentCamera = newCamera;
    }
}
