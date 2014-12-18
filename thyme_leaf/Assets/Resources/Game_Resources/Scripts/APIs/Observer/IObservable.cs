/// <summary>
/// for observer pattern
/// </summary>

public interface IObservable
{
    void RegisterObserver(IObserver o, ObserverTypes field);
    void RemoveObserver(IObserver o, ObserverTypes field);
    void NotifyObservers(ObserverTypes field);
    void HasChanged();
    void SetChanged();
}