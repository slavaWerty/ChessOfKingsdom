﻿using States.GameResources;

public class CmdResourcesSpend : ICommand
{
    public readonly ResourceType ResourceType;
    public readonly int Amount;

    public CmdResourcesSpend(ResourceType resourceType, int amount)
    {
        ResourceType = resourceType;
        Amount = amount;
    }
}
