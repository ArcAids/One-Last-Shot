public interface IWeaponInput {
    float MouseXPosition { get; }
    float MouseYPosition { get; }
    bool Shooting { get; }

    void SetWeaponInputs();
}