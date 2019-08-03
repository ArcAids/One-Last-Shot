public interface ICharacterInput
{
    float HorizontalInput { get;  }
    float VerticalInput { get; }
    float MouseXPosition { get; }
    void SetInputs();
}
