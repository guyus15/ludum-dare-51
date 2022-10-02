public interface IModifier
{
    public string Name { get; set; }

    /// <summary>
    /// Activate the modifier.
    /// </summary>
    public void Activate();

    /// <summary>
    /// Deactivate the modifier.
    /// </summary>
    public void Deactivate();

}