public interface ICharacterInput
{
    float HorizontalInput { get;  }
    float VerticalInput { get; }
    float MouseXDirection { get; }
    void SetMovementInputs();
}
