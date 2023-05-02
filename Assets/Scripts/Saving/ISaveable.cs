namespace RPG.Saving
{
    //interface to access save functions
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}