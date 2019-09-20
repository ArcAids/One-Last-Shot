public interface IDashInput
{
    bool Dash { get; }
    float HorizontalInput { get; }
    float VerticalInput { get; }
    void SetDashInputs();
}
