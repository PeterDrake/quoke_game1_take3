using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    protected bool locked;
    protected bool pauseOnOpen = false;
    protected bool forceOpen = false;
    public abstract void Open();
    public abstract void Close();

    public virtual bool IsLocked()
    {
        return locked;
    }

    public virtual bool Force()
    {
        return forceOpen;
    }
    
    public virtual bool PauseOnOpen()
    {
        return pauseOnOpen;
    }
}
