public interface IElementControlInput
{
    bool ActivateFireElement { get; }
    bool ActivateIceElement { get; }
    bool ActivateSlashElement { get; }

    void SetInputs();
}