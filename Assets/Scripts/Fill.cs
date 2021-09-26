using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fill : MonoBehaviour
{
    [SerializeField] private float scaleSpeed, growSize, speed;
    private Vector3 _vec;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _score;
    private bool isMerged;

    public int Value { get; set; }
    public bool IsTileEmpty => Value == 0;
    public int Score => IsTileEmpty ? 0 : (int)(Math.Pow(2, Value));

    private void Start()
    {
        _vec = new Vector3(growSize, growSize, 0f);
    }

    private void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            isMerged = false;
            transform.localPosition =
                Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }
        else if (isMerged == false)
        {
            if (transform.parent.GetChild(0) != transform)
                Destroy(transform.parent.GetChild(0).gameObject);
            isMerged = true;
        }
    }

    public void IncreaseValueTile()
    {
        Value++;
        UpdateCell();
        GameController.instance.UpdateScore(Score);
    }

    public void ChangeValue(int value)
    {
        Value = value;
        UpdateCell();
    }

    public void UpdateCell()
    {
        _score.text = IsTileEmpty ? "" : Score.ToString();
        _score.color = Value <= 3 || Value >= 9 ? ColorController.Pattern.ScoreDarkColor : ColorController.Pattern.ScoreLightColor;
        _image.color = ColorController.Pattern.Colors[Value];
    }

    public void CellAppearance()
    {
        StartCoroutine("CellApp");
    }
    public void CellEntrance()
    {
        StartCoroutine("CellEnt");
    }

    private IEnumerator CellApp()
    {
        while (transform == null)
            yield return null;

        while (transform.localScale.x < 1f + growSize)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one + _vec, scaleSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.localScale.x > 1f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, scaleSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator CellEnt()
    {
        while (transform == null)
            yield return null;

        transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        while (transform.localScale.x < 1f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, scaleSpeed * Time.deltaTime);
            yield return null;
        }
    }
}