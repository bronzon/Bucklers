using UnityEngine;
using System.Collections;

public interface Predicate {
	bool apply();
}
public class GameStatePredicate : Predicate {
	private GameState gameState;
	private string state;
	private bool acceptedValue;

	public GameStatePredicate (GameState gameState, string state, bool acceptedValue) {
		this.gameState = gameState;
		this.state = state;
		this.acceptedValue = acceptedValue;
	}

	public bool apply () {
		return gameState.GetState (state) == acceptedValue;
	}
}

public class FalsePredicate : Predicate {
	public bool apply () {
		return false;
	}
}

public class TruePredicate : Predicate {
	public bool apply () {
		return true;
	}
}

public class AndPredicate : Predicate {
	private Predicate first;
	private Predicate second;

	public AndPredicate (Predicate first, Predicate second)	{
		this.first = first;
		this.second = second;
	}

	public bool apply () {
		return first.apply() && second.apply();
	}
}

public class OrPredicate : Predicate {
	private Predicate first;
	private Predicate second;

	public OrPredicate (Predicate first, Predicate second)	{
		this.first = first;
		this.second = second;
	}

	public bool apply () {
		return first.apply () || second.apply ();
	}
}

public class InventoryPredicate : Predicate {
	private Inventory inventory;
	private string itemId;
	private bool shouldHave;

	public InventoryPredicate (Inventory inventory, string itemId, bool shouldHave)	{
		this.inventory = inventory;
		this.itemId = itemId;
		this.shouldHave = shouldHave;
	}
	
	public bool apply () {
		return inventory.HasItem (itemId) == shouldHave;
	}
}

public class Predicates {
	public static Predicate GameStatePredicate( GameState gameState, string state, bool acceptedValue) {
		return new GameStatePredicate (gameState, state, acceptedValue);
	}

	public static Predicate InventoryPredicate(Inventory inventory, string itemId, bool shouldHave) {
		return new InventoryPredicate(inventory, itemId, shouldHave);
	}

	public static Predicate TruePredicate() {
		return new TruePredicate ();
	}

	public static Predicate FalsePredicate() {
		return new FalsePredicate ();
	}

	public static Predicate AndPredicate(Predicate first, Predicate second) {
		return new AndPredicate (first, second);
	}
		
	public static Predicate OrPredicate(Predicate first, Predicate second) {
		return new OrPredicate (first, second);
	}
}

