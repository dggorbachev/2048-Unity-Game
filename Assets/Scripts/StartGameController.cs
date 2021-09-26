using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameController : MonoBehaviour
{
    [SerializeField] private GameObject _startGame, _nicknameEx, _game;
    [SerializeField] private TextMeshProUGUI _nickname;
    private XMLLoading xmlLoading = new XMLLoading();

    public void ContinueGame()
    {
        int spaces = 0;
        for (int i = 0; i < _nickname.text.Length; i++)
            if (_nickname.text[i] == ' ')
                spaces++;

        if (spaces + 1 == _nickname.text.Length)
            _nicknameEx.SetActive(true);
        else
        {
            string searchingNickname = _nickname.text;

            if (xmlLoading.XMLSearchToName(searchingNickname))
            {
                _startGame.SetActive(false);
                _game.SetActive(true);
                
            }
            else
                _nicknameEx.SetActive(true);
        }
    }

    public void StartNewGame()
    {
        int spaces = 0;
        for (int i = 0; i < _nickname.text.Length; i++)
            if (_nickname.text[i] == ' ')
                spaces++;

        if (spaces + 1 == _nickname.text.Length)
            _nicknameEx.SetActive(true);
        else
        {
            _startGame.SetActive(false);
            _game.SetActive(true);
        }
    }

    public void OpenExWindow()
    {
        _nicknameEx.SetActive(true);
    }
    public void CloseExWindow()
    {
        _nicknameEx.SetActive(false);
    }
}
