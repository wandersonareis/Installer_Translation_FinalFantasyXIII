using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Installer.Common.Framework;

public sealed class LoadingHandler : INotifyPropertyChanged
{
    private string _title = string.Empty;
    private int _currentStep;
    private int _totalSteps;
    private bool _isLoading;
    private bool _isIndeterminate;

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public int CurrentStep
    {
        get => _currentStep;
        set
        {
            _currentStep = value;
            OnPropertyChanged();
        }
    }

    public int TotalSteps
    {
        get => _totalSteps;
        set
        {
            _totalSteps = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool IsIndeterminate
    {
        get => _isIndeterminate;
        set
        {
            _isIndeterminate = value;
            OnPropertyChanged();
        }
    }

    public int GetPercentage()
    {
        return TotalSteps > 0 ? (int)(CurrentStep / (double)TotalSteps * 100) : 0;
    }

    public void Start()
    {
        IsIndeterminate = false;
        IsLoading = true;
        TotalSteps = 0;
        CurrentStep = 0;
    }
    public void StartIndeterminate()
    {
        IsIndeterminate = true;
        IsLoading = true;
        TotalSteps = 0;
        CurrentStep = 0;
    }
    public void Finish()
    {
        IsIndeterminate = false;
        IsLoading = false;
        Title = string.Empty;
        TotalSteps = 0;
        CurrentStep = 0;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}