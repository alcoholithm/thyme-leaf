public interface IRenewalObservable
{
    void RegisterObserver(IRenewalObserver o);
    void RemoveObserver(IRenewalObserver o);
    void NotifyRenewalObservers();
    void HasChanged();
    void SetChanged();
}