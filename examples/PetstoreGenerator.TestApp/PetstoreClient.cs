// This file is generated by the PetstoreGenerator program. Do not edit by hand.
// This file is saved into git so that we can track changes between version and to give an overview of what the program does.

namespace PetstoreGenerator.TestApp;

public record Pet(string name, string? tag, long id);
public record NewPet(string name, string? tag);
public record Error(int code, string message);
