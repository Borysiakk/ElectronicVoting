using System;
using System.Collections.Generic;

namespace ElectronicVoting.Validator.Consensus
{
    public class Blockchain
    {
        private readonly List<Block>_blocks;
        
        public Blockchain()
        {
            _blocks = new List<Block>();
        }

        public Block CreateBlock()
        {
            Block block = new Block
            {
                Id = _blocks.Count,
                PreviousHash = _blocks.Count != 0 ?_blocks[^1].Hash : null,
            };

            return block;
        }
        
    }
}