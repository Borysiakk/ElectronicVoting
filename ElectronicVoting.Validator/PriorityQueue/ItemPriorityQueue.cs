namespace ElectronicVoting.PriorityQueue
{
    public abstract class ItemPriorityQueue
    {
        public PriorityMessage Priority { get; set; }
        public ItemPriorityQueue Left { get; set; }
        public ItemPriorityQueue Right { get; set; }
    }
}