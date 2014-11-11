public interface IScoreObservable
{
    void RegisterObserver(IScoreObserver o);
    void RemoveObserver(IScoreObserver o);
    void NotifyScoreObservers();
    void HasChanged();
    void SetChanged();
}