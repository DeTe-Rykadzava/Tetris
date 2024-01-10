using Tetris.Core.Game.Figures;
using Tetris.Core.InputManager;

namespace Tetris.Core.Game;

public class Game
{
    private ConsoleInputManager _inputManager;

    private GameBoard _board;

    private Vector _direction = Vector.Down;
    
    private const int TickInterval = 375;
    
    private const int DrawBoardInterval = 125;

    private CancellationTokenSource _isGameTs = new CancellationTokenSource();

    public event Action GameOver;

    public int Score { get; private set; }

    public Game()
    {
        _inputManager = new ConsoleInputManager();
        _board = new GameBoard();
        GameOver += () =>
        {
            // stop handle
            _inputManager.StopHandleUserInput();
            _inputManager.DownKeyDown -= Down;

            _inputManager.RightKeyDown -= Right;

            _inputManager.LeftKeyDown -= Left;

            _inputManager.SpaceKeyDown -= Rotate;
        };
    }

    private void Down()
    {
        _board.Move(Vector.Down);
    }
    
    private void Right()
    {
        _board.Move(Vector.Right);
    }

    private void Left()
    {
        _board.Move(Vector.Left);
    }

    private void Rotate()
    {
        _board.RotateFigure();
    }

    public void StartGame()
    {
        Score = 0; 
        _isGameTs = new CancellationTokenSource();
         // subscribe on events
        _inputManager.DownKeyDown += Down;

        _inputManager.RightKeyDown += Right;

        _inputManager.LeftKeyDown += Left;

        _inputManager.SpaceKeyDown += Rotate;

        // tick, change pos down, add score and spawn
        Task.Run(() =>
        {
            _board.SpawnFigure(FigureFactory.CreateL());
            while (!_isGameTs.Token.IsCancellationRequested)
            {
                Thread.Sleep(TickInterval);
        
                if (!_board.Move(Vector.Down))
                {
                    if (_board.ClearIfExistLine() is { } countClearedLine)
                    {
                        Score += countClearedLine * 10;
                    }

                    if(!_board.SpawnFigure(FigureFactory.GetRandomFigure()))
                    {
                        _isGameTs.Cancel();
                        //Console.WriteLine("Game Over");
                        GameOver?.Invoke();
                    }
                }
            }
        });

        // drawing board
        Task.Run(() =>
        {
            while (!_isGameTs.Token.IsCancellationRequested)
            {
                Console.Clear();
                _board.DrawBoard();
                Thread.Sleep(DrawBoardInterval);
            }
        });
        
        // handle user input
        _inputManager.StartHandleUserInput();
    }

}