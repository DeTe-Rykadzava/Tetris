using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tetris.Core.InputManager;

public class ConsoleInputManager : INotifyPropertyChanged
{
    // buttons for events
    private static ConsoleKey DownKey = ConsoleKey.DownArrow;
    
    private static ConsoleKey LeftKey = ConsoleKey.LeftArrow;
    
    private static ConsoleKey RightKey = ConsoleKey.RightArrow;
    
    private static ConsoleKey SpaceKey = ConsoleKey.Spacebar;
    
    private static ConsoleKey QuitKey = ConsoleKey.Q;
    // buttons for events
    
    // events
    public event Action LeftKeyDown;
    
    public event Action RightKeyDown;
    
    public event Action DownKeyDown;

    public event Action SpaceKeyDown;
    
    public event Action QuitKeyDown;
    
    public event Action LeftKeyUp;
    
    public event Action RightKeyUp;
    
    public event Action DownKeyUp;
    
    // events

    private bool _isHandle = false;

    public bool IsHandle 
    { 
        get => _isHandle;
        private set => SetField(ref _isHandle, value);
    }

    public ConsoleInputManager()
    {
        // check "Is Handle"
        PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(IsHandle))
            {
                if(IsHandle)
                    HandleUserInput();
            }
        };
    }

    public void StopHandleUserInput()
    {
        IsHandle = false;
    }

    public void StartHandleUserInput()
    {
        IsHandle = true;
    }

    private void HandleUserInput()
    {
        while (IsHandle) 
        {
            if (!Console.KeyAvailable)
            {
                continue;
            }

            SendKeyDown();
            Thread.Sleep(10);
        }
    }

    private void SendKeyDown()
    {
        var inputKey = Console.ReadKey().Key;

        if (inputKey == DownKey)
        {
            DownKeyDown?.Invoke();
        }
        if(inputKey == RightKey)
        {
            RightKeyDown?.Invoke();
        }
        if(inputKey == LeftKey)
        {
            LeftKeyDown?.Invoke();
        }
            
        if(inputKey == SpaceKey)
        {
            SpaceKeyDown?.Invoke();
        }

        if (inputKey == QuitKey)
        {
            QuitKeyDown?.Invoke();
        }

        // Console.WriteLine(inputKey + "\tDown");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}