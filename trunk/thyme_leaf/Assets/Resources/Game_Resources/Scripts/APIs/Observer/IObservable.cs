/// <summary>
/// for observer pattern
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IObservable
{
    void RegisterObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObservers();
    void HasChanged();
    void SetChanged();
}