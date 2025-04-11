public interface ISelectable : IMonobehaviour
{
    InterfaceReference<IPlaceable> Placeable { get; }
    void Select();
    void Deselect();
}
