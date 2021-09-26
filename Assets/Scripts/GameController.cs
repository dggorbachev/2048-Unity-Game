using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private readonly Random rnd = new Random();
    public static int timer, curScore;
    public static bool isTileMoved;
    private bool isCreateNewField, isWinGame;

    private XMLSaving _xmlSaving;
    private readonly XMLLoading _xmlLoading = new XMLLoading();

    [SerializeField] private GameObject  _tableScores, _help, _gameOver, _winGame;
    [SerializeField] private Button _helpButton;
    [SerializeField] private TextMeshProUGUI _nickname, _nicknameField;

    [Space(10)] [SerializeField] private TextMeshProUGUI scoreText;

    [Space(10)] [SerializeField] private GameObject _fillPrefab;

    [Space(5)] [SerializeField] private Cell[] _cells;

    [Space(10)] [SerializeField] private TextMeshProUGUI playersRating;
    [SerializeField] private TextMeshProUGUI timeRating, scoreRating;


    public static Action<string> KeyAction;

    private void OnApplicationQuit()
    {
        SaveRating();
    }
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        Application.targetFrameRate = 300;
        _nicknameField.text = "Player: " + _nickname.text;
        LoadRating();
    }

    private void Update()
    {
        WinGameCheck();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_help.activeSelf)
            {
                _help.SetActive(false);
                Timer.isTimerAvailable = true;
            }
        }

        if (!(_winGame.activeSelf || _gameOver.activeSelf || _help.activeSelf))
        {
            GameOverCheck();

            if (!isCreateNewField)
            {
                NewTileGenerate();
                NewTileGenerate();
                isCreateNewField = true;
            }

            isTileMoved = false;
            timer = 0;
            if (Input.GetKeyDown(KeyCode.Tab))
                _tableScores.SetActive(true);

            if (Input.GetKeyUp(KeyCode.Tab))
                _tableScores.SetActive(false);

            if (Input.GetKeyDown(KeyCode.W))
                KeyAction("W");

            if (Input.GetKeyDown(KeyCode.A))
                KeyAction("A");

            if (Input.GetKeyDown(KeyCode.S))
                KeyAction("S");

            if (Input.GetKeyDown(KeyCode.D))
                KeyAction("D");
        }
    }
    public void NewTileGenerate()
    {
        List<Transform> emptyCells = new List<Transform>();

        for (int i = 0; i < 16; i++)
            if (_cells[i].transform.childCount == 0)
                emptyCells.Add(_cells[i].transform);

        if (emptyCells.Count != 0)
        {
            Transform fillCell = emptyCells[rnd.Next(0, emptyCells.Count)];
            GameObject temp = Instantiate(_fillPrefab, fillCell);
            Fill tempFill = temp.GetComponent<Fill>();
            fillCell.GetComponent<Cell>().Fill = tempFill;

            if (rnd.Next(0, 10) == 0)
                tempFill.ChangeValue(2);
            else
                tempFill.ChangeValue(1);

            tempFill.CellEntrance();
        }
    }

    public void TileTwoGenerate()
    {
        List<Transform> emptyCells = new List<Transform>();

        for (int i = 0; i < 16; i++)
            if (_cells[i].transform.childCount == 0)
                emptyCells.Add(_cells[i].transform);

        if (emptyCells.Count != 0)
        {
            Transform fillCell = emptyCells[rnd.Next(0, emptyCells.Count)];
            GameObject temp = Instantiate(_fillPrefab, fillCell);
            Fill tempFill = temp.GetComponent<Fill>();
            fillCell.GetComponent<Cell>().Fill = tempFill;

            tempFill.ChangeValue(1);
            tempFill.CellAppearance();
        }
    }

    public void UpdateScore(int addScore)
    {
        curScore += addScore;
        scoreText.text = curScore.ToString();
    }

    public void OpenHelpPanel()
    {
        _help.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        _help.SetActive(false);
    }

    public void GameOverCheck()
    {
        if (isGameOver())
        {
            _helpButton.interactable = false;
            Timer.isTimerAvailable = false;

            _gameOver.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        _winGame.SetActive(false);
        _helpButton.interactable = true;
        Timer.isTimerAvailable = true;
    }
    public void WinGameCheck()
    {
        if (!isWinGame && _cells.Where(value => value.Fill != null).Count(value => value.Fill.Value == 11) == 1)
        {
            _helpButton.interactable = false;
            Timer.isTimerAvailable = false;

            isWinGame = true;
            _winGame.SetActive(true);
        }
    }
    public void Restart()
    {
        SaveRating();
        SceneManager.LoadScene(0);
        curScore = 0;
    }
    public void Exit()
    {
        SaveRating();
        Application.Quit();
    }

    private void SaveRating()
    {
        bool isGameOver = _gameOver.activeSelf;
        int[] cells = new int[16];

        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].Fill == null)
                cells[i] = 0;
            else
                cells[i] = _cells[i].Fill.Value;
        }

        _xmlSaving = new XMLSaving(_nickname.text, isGameOver, curScore, Timer.time, cells);
    }

    public void NewGame()
    {
        isCreateNewField = false;
    }
    public void ReadInfo()
    {
        isCreateNewField = true;
        string nick = _nickname.text;

        _xmlLoading.XMLLoadingToName(nick, out bool isGameOver,
            out int score, out float time, out int[] cells);

        if (!isGameOver)
        {
            Timer.time = time;

            curScore = score;
            UpdateScore(0);

            for (int i = 0; i < 16; i++)
                if (cells[i] != 0)
                {
                    Transform fillCell = _cells[i].transform;
                    GameObject temp = Instantiate(_fillPrefab, fillCell);
                    Fill tempFill = temp.GetComponent<Fill>();
                    fillCell.GetComponent<Cell>().Fill = tempFill;

                    tempFill.ChangeValue(cells[i]);
                }
        }
    }

    private void LoadRating()
    {
        _xmlLoading.XMLLoadingRating(out List<string[]> data);
        int size = data.Count;

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                Int32.TryParse(data[i][1], out int score);
                Int32.TryParse(data[j][1], out int score1);

                if (score > score1)
                {
                    var tempData = data[i];
                    data[i] = data[j];
                    data[j] = tempData;
                }
                else if (score == score1)
                {
                    Int32.TryParse(data[i][2], out int time);
                    Int32.TryParse(data[j][2], out int time1);

                    if (time > time1)
                    {
                        var tempData = data[i];
                        data[i] = data[j];
                        data[j] = tempData;
                    }
                }
            }

        int start = size - 16 > 0 ? size - 16 : 0;

        for (int i = start; i < size; i++)
        {
            playersRating.text += data[i][0] + "\n";
            scoreRating.text += data[i][1] + "\n";
            timeRating.text += data[i][2] + "\n";
        }
    }

    private bool isGameOver()
    {
        for (int i = 0; i < 16; i++)
        {
            if (_cells[i].Fill == null)
                return false;

            if (_cells[i].Up != null)
            {
                if (_cells[i].Up.Fill == null)
                    return false;
                if (_cells[i].Up.Fill.Value == _cells[i].Fill.Value)
                    return false;
            }

            if (_cells[i].Left != null)
            {
                if (_cells[i].Left.Fill == null)
                    return false;
                if (_cells[i].Left.Fill.Value == _cells[i].Fill.Value)
                    return false;
            }

            if (_cells[i].Down != null)
            {
                if (_cells[i].Down.Fill == null)
                    return false;
                if (_cells[i].Down.Fill.Value == _cells[i].Fill.Value)
                    return false;
            }

            if (_cells[i].Right != null)
            {
                if (_cells[i].Right.Fill == null)
                    return false;
                if (_cells[i].Right.Fill.Value == _cells[i].Fill.Value)
                    return false;
            }
        }

        return true;
    }
}