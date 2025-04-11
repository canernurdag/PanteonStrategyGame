using System.Collections.Generic;
public interface IPlaceable : IMonobehaviour
{
	InterfaceReference<ISelectable> Selectable { get; }
    bool IsPlaced { get;}
	void SetAsPlaced();
	List<Node> OccupyingNodes { get; }
	void SetOccupyingNodes(List<Node> occupyingNodes);


}
