using System.Collections.Generic;
public interface IPlaceable : IMonobehaviour
{
	InterfaceReference<ISelectable> Selectable { get; }
	InterfaceReference<IDamagable> Damagable { get; }
	InterfaceReference<IAttacker> Attacker { get; }
    bool IsPlaced { get;}
	void Place();
	void Deplace();
	List<Node> OccupyingNodes { get; }
	void SetOccupyingNodes(List<Node> occupyingNodes);


}
