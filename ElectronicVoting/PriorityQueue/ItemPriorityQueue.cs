namespace KolejkaPriorytetowa
{
    public abstract class ItemPriorityQueue
    {
        public Priority Priority { get; set; }
        public ItemPriorityQueue Left { get; set; }
        public ItemPriorityQueue Right { get; set; }
    }
}