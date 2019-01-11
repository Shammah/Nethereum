using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Nethereum.RPC;
using Nethereum.RPC.Reactive.Polling;
using ReactiveUI;

namespace Nethereum.Spelunker.Modules.Home.ViewModels
{
    public class TransactionStreamViewModel : ReactiveObject, IDisposable
    {
        private readonly CompositeDisposable Subscriptions;

        private List<TransactionViewModel> _transactions;
        public TransactionStreamViewModel(IEthApiService eth)
        {
            Transactions = new List<TransactionViewModel>();

            Subscriptions = new CompositeDisposable
            {
                eth.GetTransactions()
                    .ObserveOnDispatcher()
                    .Select(t => TransactionViewModel.FromTransaction(t, DateTime.Now))
                    .Subscribe(transaction =>
                    {
                        Transactions.Add(transaction);
                        Transactions = Transactions
                            .OrderByDescending(t => t.Time)
                            .ThenBy(t => t.TransactionIndex)
                            .Take(10)
                            .ToList();
                    })
            };
        }

        public List<TransactionViewModel> Transactions
        {
            get => _transactions;
            private set => this.RaiseAndSetIfChanged(ref _transactions, value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Subscriptions?.Dispose();
        }
    }
}