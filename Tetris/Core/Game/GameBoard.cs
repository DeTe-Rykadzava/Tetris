using Tetris.Core.Game.Figures;

namespace Tetris.Core.Game;

public class GameBoard
{
    private const int BoardHeight = 32;
    private const int BoardWidth  = 16;
    private const int FigureSpawnPointX = BoardWidth / 2;  
    private const int FigureSpawnPointY = 2;  
    
    private Cell[,] _board;

    private Figure? _currentFigure = null;

    private Vector? _figurePivotPoint = null;
    
    public GameBoard()
    {
        _board = new Cell[BoardWidth, BoardHeight];
        InitBoard();
    }

    private void SetFigure(Figure figure)
    {
        _currentFigure = figure;
        _figurePivotPoint = new Vector(FigureSpawnPointX, FigureSpawnPointY);
    }

    public bool SpawnFigure(Figure figure)
    {
        if (!CanSpawnFigure()) return false;
        SetFigure(figure);
        return true;
    }

    private bool CanSpawnFigure()
    {
        return _board[FigureSpawnPointX, FigureSpawnPointY].Type != CellType.Figure && _board[FigureSpawnPointX - 1, FigureSpawnPointY].Type != CellType.Figure &&
               _board[FigureSpawnPointX + 1, FigureSpawnPointY].Type != CellType.Figure && _board[FigureSpawnPointX - 1, FigureSpawnPointY - 1].Type != CellType.Figure && 
               _board[FigureSpawnPointX, FigureSpawnPointY - 1].Type != CellType.Figure && _board[FigureSpawnPointX + 1, FigureSpawnPointY - 1].Type != CellType.Figure &&
               _board[FigureSpawnPointX - 1, FigureSpawnPointY + 1].Type != CellType.Figure && _board[FigureSpawnPointX, FigureSpawnPointY + 1].Type != CellType.Figure &&
               _board[FigureSpawnPointX + 1, FigureSpawnPointY + 1].Type != CellType.Figure;
    }

    private void InitBoard()
    {
        for (int y = 0; y < BoardHeight; y++)
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                if (x == 0 || x == 15 || y == 31)
                {
                    _board[x, y] = new Cell(new Vector(x,y), CellType.Border);
                    continue;
                }

                _board[x, y] = new Cell(new Vector(x,y), CellType.Void);;
            }
        }
    }

    public void DrawBoard()
    {
        for (int y = 0; y < BoardHeight; y++)
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                if (x == BoardWidth - 1)
                {
                    Console.Write(_board[x,y]+"\n");
                    continue;
                }

                Console.Write(_board[x,y]+" ");
            }
        }
    }

    public bool Move(Vector direction, bool isVerifiedMove = false)
    {
        if (_currentFigure == null || _figurePivotPoint == null)
            return false;
        
        if (!isVerifiedMove)
        {
            if (!CanMove(direction))
            {
                return false;
            }

            Move(direction);
            return true;
        }
        Move(direction);
        return true;
    }

    private void Move(Vector direction)
    {
        if(_currentFigure == null || _figurePivotPoint == null)
            return;
        
        ClearOldFigurePointsOnBoard();
        var pivotTemp = _figurePivotPoint.Value;
        pivotTemp.X += direction.X;
        pivotTemp.Y -= direction.Y;
        _figurePivotPoint = pivotTemp;
        // if(direction == Vector.Down)
        //     _figurePivotPoint -= Vector.Down;
        // else if (direction == Vector.Left)
        //     _figurePivotPoint += Vector.Left;
        // else if (direction == Vector.Right)
        //     _figurePivotPoint += Vector.Right;
        ShowNewFigurePointsOnBoard();
    }

    private void ShowNewFigurePointsOnBoard()
    {
        if(_currentFigure == null || _figurePivotPoint == null)
            return;
        foreach (var positions in _currentFigure.CellsPosition)
        {
            var dotInBoard = _figurePivotPoint!.Value + positions;
            _board[(int)dotInBoard.X,(int)dotInBoard.Y].ChangeType(CellType.Figure);
        }
    }

    private void ClearOldFigurePointsOnBoard()
    {
        if(_currentFigure == null || _figurePivotPoint == null)
            return;
        
        foreach (var positions in _currentFigure.CellsPosition)
        {
            var dotInBoard = _figurePivotPoint!.Value + positions;
            _board[(int)dotInBoard.X,(int)dotInBoard.Y].ChangeType(CellType.Void);
        }
    }

    public bool CanMove(Vector direction)
    {
        if (_currentFigure == null || _figurePivotPoint == null)
            return false;

        if (direction.Y < 0)
        {
            return CanMoveDown();
        }
        if (direction.X > 0)
        {
            return CanMoveRight();
        }
        if (direction.X < 0)
        {
            return CanMoveLeft();
        }
        return false;
    }

    public bool RotateFigure()
    {
        if (!CanRotateFigure())
            return false;
        return true;
    }

    private bool CanRotateFigure()
    {
        if (_currentFigure == null || _figurePivotPoint == null)
            return false;
        
        ClearOldFigurePointsOnBoard();
        _currentFigure.Rotate();
        var isSuccessRotate = true;
        foreach (var point in _currentFigure.CellsPosition)
        {
            var boardPoint = _figurePivotPoint.Value + point;
            var cell = _board[(int)boardPoint.X, (int)boardPoint.Y ];
            if (cell.Type == CellType.Border || cell.Type == CellType.Figure)
            {
                isSuccessRotate = false;
                break;
            }
        }

        if (!isSuccessRotate)
        {
            _currentFigure.RotateBack();
        }

        ShowNewFigurePointsOnBoard();
        return isSuccessRotate;
    }

    private bool CanMoveDown()
    {
        if (_currentFigure == null || _figurePivotPoint == null)
            return false;
        
        var downPoints = _currentFigure.GetDownPoints();
        foreach (var point in downPoints)
        {
            var boardPoint = _figurePivotPoint.Value + point;
            var cell = _board[(int)boardPoint.X, (int)boardPoint.Y + 1];
            if (cell.Type == CellType.Border || cell.Type == CellType.Figure)
                return false;
        }
        return true;
    }

    private bool CanMoveRight()
    {
        if (_currentFigure == null || _figurePivotPoint == null)
            return false;
        
        var rightPoints = _currentFigure.GetRightPoints();
        foreach (var point in rightPoints)
        {
            var boardPoint = _figurePivotPoint.Value + point;
            var cell = _board[(int)boardPoint.X + 1, (int)boardPoint.Y];
            if (cell.Type == CellType.Border || cell.Type == CellType.Figure)
                return false;
        }
        return true;
    }

    private bool CanMoveLeft()
    {
        if (_currentFigure == null || _figurePivotPoint == null)
            return false;
        
        var leftPoints = _currentFigure.GetLeftPoints();
        foreach (var point in leftPoints)
        {
            var boardPoint = _figurePivotPoint.Value + point;
            var cell = _board[(int)boardPoint.X - 1, (int)boardPoint.Y];
            if (cell.Type == CellType.Border || cell.Type == CellType.Figure)
                return false;
        }
        return true;
    }

    public int? ClearIfExistLine()
    {
        var countClearLine = 0;
        for (int y = 0; y < BoardHeight - 1; y++)
        {
            for (int x = 1; x <= BoardWidth - 1; x++)
            {
                if (x == BoardWidth - 1)
                {
                    countClearLine = 1;
                    ClearLine(y);
                }

                if (_board[x, y].Type == CellType.Figure)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        return countClearLine;
    }

    private void ClearLine(int y)
    {
        for (int x = 1; x < BoardWidth - 1; x++)
        {
            _board[x, y].ChangeType(CellType.Void);
        }

        for (int bY = y; bY > 0 ; bY--)
        {
            for (int bX = 1; bX < BoardWidth - 1; bX++)
            {
                if(bY - 1 < 0)
                    break;
                if(_board[bX, bY - 1].Type == CellType.Void)
                    continue;
                else
                {
                    if (_board[bX, bY].Type == _board[bX, bY - 1].Type)
                        continue;

                    var tempType = _board[bX, bY].Type;
                    _board[bX, bY].ChangeType(_board[bX, bY - 1].Type);
                    _board[bX, bY - 1].ChangeType(tempType);
                }
            }
        }
    }
}