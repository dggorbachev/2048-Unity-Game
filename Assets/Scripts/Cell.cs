using UnityEngine;
public class Cell : MonoBehaviour
{
    public Cell Up, Down, Left, Right;

    public Fill Fill;
    private void OnEnable()
    {
        GameController.KeyAction += OnKey;
    }

    private void OnDisable()
    {
        GameController.KeyAction -= OnKey;
    }
    private void OnKey(string key)
    {
        if (key == "W")
        {
            if (Up != null)
                return;
            Cell cell = this;
            StepUp(cell);
        }

        if (key == "A")
        {
            if (Left != null)
                return;
            Cell cell = this;
            StepLeft(cell);
        }

        if (key == "S")
        {
            if (Down != null)
                return;
            Cell cell = this;
            StepDown(cell);
        }

        if (key == "D")
        {
            if (Right != null)
                return;
            Cell cell = this;
            StepRight(cell);
        }

        GameController.timer++;
        if (GameController.timer == 4 && GameController.isTileMoved)
            GameController.instance.TileTwoGenerate();
    }

    private bool StepUp(Cell cell)
    {
        if (cell.Down == null)
            return GameController.isTileMoved;

        if (cell.Fill != null)
        {
            Cell next = cell.Down;
            while (next.Down != null && next.Fill == null)
                next = next.Down;

            if (next.Fill != null)
            {
                if (cell.Fill.Value == next.Fill.Value)
                {
                    GameController.isTileMoved = true;
                    next.Fill.IncreaseValueTile();
                    next.Fill.transform.parent = cell.transform;
                    cell.Fill = next.Fill;
                    next.Fill = null;
                }
                else if (cell.Down.Fill != next.Fill)
                {
                    GameController.isTileMoved = true;
                    next.Fill.transform.parent = cell.Down.transform;
                    cell.Down.Fill = next.Fill;
                    next.Fill = null;
                }
            }
        }
        else
        {
            Cell next = cell.Down;
            while (next.Down != null && next.Fill == null)
                next = next.Down;

            if (next.Fill != null)
            {
                GameController.isTileMoved = true;
                next.Fill.transform.parent = cell.transform;
                cell.Fill = next.Fill;
                next.Fill = null;
                StepUp(cell);
            }
        }

        if (cell.Down == null)
            return GameController.isTileMoved;

        GameController.isTileMoved = StepUp(cell.Down);
        return GameController.isTileMoved;
    }

    private bool StepDown(Cell cell)
    {
        if (cell.Up == null)
            return GameController.isTileMoved;

        if (cell.Fill != null)
        {
            Cell next = cell.Up;
            while (next.Up != null && next.Fill == null)
                next = next.Up;

            if (next.Fill != null)
            {
                if (cell.Fill.Value == next.Fill.Value)
                {
                    GameController.isTileMoved = true;
                    next.Fill.IncreaseValueTile();
                    next.Fill.transform.parent = cell.transform;
                    cell.Fill = next.Fill;
                    next.Fill = null;
                }
                else if (cell.Up.Fill != next.Fill)
                {
                    GameController.isTileMoved = true;
                    next.Fill.transform.parent = cell.Up.transform;
                    cell.Up.Fill = next.Fill;
                    next.Fill = null;
                }
            }
        }
        else
        {
            Cell next = cell.Up;
            while (next.Up != null && next.Fill == null)
                next = next.Up;

            if (next.Fill != null)
            {
                GameController.isTileMoved = true;
                next.Fill.transform.parent = cell.transform;
                cell.Fill = next.Fill;
                next.Fill = null;
                StepDown(cell);
            }
        }

        if (cell.Up == null)
            return GameController.isTileMoved;

        GameController.isTileMoved = StepDown(cell.Up);
        return GameController.isTileMoved;
    }

    private bool StepLeft(Cell cell)
    {
        if (cell.Right == null)
            return GameController.isTileMoved;

        if (cell.Fill != null)
        {
            Cell next = cell.Right;
            while (next.Right != null && next.Fill == null)
                next = next.Right;

            if (next.Fill != null)
            {
                if (cell.Fill.Value == next.Fill.Value)
                {
                    GameController.isTileMoved = true;
                    next.Fill.IncreaseValueTile();
                    next.Fill.transform.parent = cell.transform;
                    cell.Fill = next.Fill;
                    next.Fill = null;
                }
                else if (cell.Right.Fill != next.Fill)
                {
                    GameController.isTileMoved = true;
                    next.Fill.transform.parent = cell.Right.transform;
                    cell.Right.Fill = next.Fill;
                    next.Fill = null;
                }
            }
        }
        else
        {
            Cell next = cell.Right;
            while (next.Right != null && next.Fill == null)
                next = next.Right;

            if (next.Fill != null)
            {
                GameController.isTileMoved = true;
                next.Fill.transform.parent = cell.transform;
                cell.Fill = next.Fill;
                next.Fill = null;
                StepLeft(cell);
            }
        }

        if (cell.Right == null)
            return GameController.isTileMoved;

        GameController.isTileMoved = StepLeft(cell.Right);
        return GameController.isTileMoved;
    }

    private bool StepRight(Cell cell)
    {
        if (cell.Left == null)
            return GameController.isTileMoved;

        if (cell.Fill != null)
        {
            Cell next = cell.Left;
            while (next.Left != null && next.Fill == null)
                next = next.Left;

            if (next.Fill != null)
            {
                if (cell.Fill.Value == next.Fill.Value)
                {
                    GameController.isTileMoved = true;
                    next.Fill.IncreaseValueTile();
                    next.Fill.transform.parent = cell.transform;
                    cell.Fill = next.Fill;
                    next.Fill = null;
                }
                else if (cell.Left.Fill != next.Fill)
                {
                    GameController.isTileMoved = true;
                    next.Fill.transform.parent = cell.Left.transform;
                    cell.Left.Fill = next.Fill;
                    next.Fill = null;
                }
            }
        }
        else
        {
            Cell next = cell.Left;
            while (next.Left != null && next.Fill == null)
                next = next.Left;

            if (next.Fill != null)
            {
                GameController.isTileMoved = true;
                next.Fill.transform.parent = cell.transform;
                cell.Fill = next.Fill;
                next.Fill = null;
                StepRight(cell);
            }
        }

        if (cell.Left == null)
            return GameController.isTileMoved;

        GameController.isTileMoved = StepRight(cell.Left);
        return GameController.isTileMoved;
    }
}