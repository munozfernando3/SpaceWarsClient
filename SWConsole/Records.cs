namespace SpaceWarsServices;

public record JoinGameResponse(string Token, Location StartingLocation, string GameState, int Heading, int BoardWidth, int BoardHeight, IEnumerable<PurchasableItem> Shop);
public record Location(int X, int Y);
public record GameStateResponse(string GameState, IEnumerable<Location> PlayerLocations);
public record PlayerMessageResponse(string Type, string Message);
public record QueueActionRequest(string Type, string? Request);
public record QueueActionResponse(string Message);
public record ShopResponse(int Cost, string Name, IEnumerable<string> Prerequisites);
public record PurchasableItem(int Cost, string Name, IEnumerable<string> Prerequisites);
public record GameMessage(string Type, string Message);