﻿using States.GameResources;

public class CmdResourcesAdd : ICommand
{
    public readonly ResourceType ResourceType;
    public readonly int Amount;

    public CmdResourcesAdd(ResourceType resourceType, int amount)
    {
        ResourceType = resourceType;
        Amount = amount;
    }
}
