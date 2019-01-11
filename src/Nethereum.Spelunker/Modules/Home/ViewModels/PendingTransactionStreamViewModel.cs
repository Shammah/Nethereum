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
    public class PendingTransactionStreamViewModel : ReactiveObject, IDisposable
    {
        private readonly CompositeDisposable Subscriptions;

        private List<TransactionViewModel> _pendingTransactions;

        public PendingTransactionStreamViewModel(IEthApiService eth)
        {
            PendingTransactions = new List<TransactionViewModel>();

            Subscriptions = new CompositeDisposable
            {
                eth.GetPendingTransactions()
                    .ObserveOnDispatcher()
                    .Select(t => TransactionViewModel.FromTransaction(t, DateTime.Now))
                    .Subscribe(transaction =>
                    {
                        PendingTransactions.Add(transaction);
                        PendingTransactions = PendingTransactions
                            .OrderByDescending(t => t.Time)
                            .ThenBy(t => t.TransactionIndex)
                            .Take(10)
                            .ToList();
                    })
            };
        }

        public List<TransactionViewModel> PendingTransactions
        {
            get => _pendingTransactions;
            private set => this.RaiseAndSetIfChanged(ref _pendingTransactions, value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Subscriptions?.Dispose();
        }
    }
}