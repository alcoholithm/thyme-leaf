/// <summary>
/// for observer pattern
/// </summary>
public interface IObservable<TObserver>
{
    void RegisterObserver(TObserver o);
    void RemoveObserver(TObserver o);
    void NotifyObservers<TObserver>();
    void HasChanged();
    void SetChanged();
}