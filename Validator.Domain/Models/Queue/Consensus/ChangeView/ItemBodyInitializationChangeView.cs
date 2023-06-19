﻿
using Validator.Domain.Queue.Consensus;

namespace Validator.Domain.Models.Queue.Consensus.ChangeView;
public class ItemBodyInitializationChangeView : ItemBody
{
    public int Round { get; set; }
    public bool Decision { get; set; }
}