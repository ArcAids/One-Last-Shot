public interface IWeaponInput {
    float MouseXDirection { get; }
    float MouseYDirection { get; }
    bool Shooting { get; }

    void SetWeaponInputs();
}