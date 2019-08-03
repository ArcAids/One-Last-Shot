public interface IWeaponInput {
    float MouseXPosition { get; }
    float MouseYPosition { get; }
    bool shooting { get; }

    void SetInputs();
}