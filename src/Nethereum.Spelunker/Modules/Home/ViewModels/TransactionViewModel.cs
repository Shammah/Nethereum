using System;
using System.Numerics;
using Nethereum.RPC.Eth.DTOs;
using ReactiveUI;

namespace Nethereum.Spelunker.Modules.Home.ViewModels
{
    public class TransactionViewModel : ReactiveObject
    {
        private string _from;
        private DateTime _time;
        private string _to;
        private string _transactionHash;
        private BigInteger _transactionIndex;
        private BigInteger _value;

        public DateTime Time
        {
            get => _time;
            private set => this.RaiseAndSetIfChanged(ref _time, value);
        }

        public string From
        {
            get => _from;
            private set => this.RaiseAndSetIfChanged(ref _from, value);
        }

        public string To
        {
            get => _to;
            private set => this.RaiseAndSetIfChanged(ref _to, value);
        }

        public BigInteger Value
        {
            get => _value;
            private set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        public BigInteger TransactionIndex
        {
            get => _transactionIndex;
            private set => this.RaiseAndSetIfChanged(ref _transactionIndex, value);
        }

        public string TransactionHash
        {
            get => _transactionHash;
            private set => this.RaiseAndSetIfChanged(ref _transactionHash, value);
        }

        public static TransactionViewModel FromTransaction(Transaction transaction, DateTime time) => new TransactionViewModel
        {
            Time = time,
            TransactionHash = transaction.TransactionHash,
            From = transaction.From,
            To = transaction.To,
            Value = transaction.Value,
            TransactionIndex = transaction.TransactionIndex
        };
    }
}