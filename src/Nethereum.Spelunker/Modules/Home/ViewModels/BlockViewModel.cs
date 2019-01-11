using System.Numerics;
using Nethereum.RPC.Eth.DTOs;
using ReactiveUI;

namespace Nethereum.Spelunker.Modules.Home.ViewModels
{
    public class BlockViewModel : ReactiveObject
    {
        private BigInteger _blockNumber;
        private string _miner;
        private BigInteger _reward;
        private string _timeElapsed;
        private int _transactions;

        public BigInteger BlockNumber
        {
            get => _blockNumber;
            private set => this.RaiseAndSetIfChanged(ref _blockNumber, value);
        }

        public string TimeElapsed
        {
            get => _timeElapsed;
            private set => this.RaiseAndSetIfChanged(ref _timeElapsed, value);
        }

        public string Miner
        {
            get => _miner;
            private set => this.RaiseAndSetIfChanged(ref _miner, value);
        }

        public int Transactions
        {
            get => _transactions;
            private set => this.RaiseAndSetIfChanged(ref _transactions, value);
        }

        public BigInteger Reward
        {
            get => _reward;
            private set => this.RaiseAndSetIfChanged(ref _reward, value);
        }

        public static BlockViewModel FromBlock(BlockWithTransactionHashes block) => new BlockViewModel
        {
            BlockNumber = block.Number,
            TimeElapsed = "??",
            Miner = block.Miner,
            Transactions = block.TransactionHashes.Length,
            Reward = block.GasUsed
        };
    }
}