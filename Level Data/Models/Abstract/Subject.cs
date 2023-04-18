public abstract class Subject
{
    private List<IObserver> _Observers = new List<IObserver>();

    public void Attach(IObserver Observer)
    {
        _Observers.Add(Observer);
    }

    public void Detach(IObserver observer)
    {
        _Observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver Observer in _Observers)
        {
            Observer.Update();
        }
    }
}
