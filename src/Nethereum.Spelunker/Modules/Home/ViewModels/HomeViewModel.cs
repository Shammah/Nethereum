using System;
using ReactiveUI;

namespace Nethereum.Spelunker.Modules.Home.ViewModels
{
    public sealed class HomeViewModel : ReactiveObject, INavigationAware
    {
        private readonly Func<BlockStreamViewModel> BlockStreamFactory;
        private readonly Func<PendingTransactionStreamViewModel> PendingTransactionStreamFactory;
        private readonly Func<TransactionStreamViewModel> TransactionStreamFactory;

        private BlockStreamViewModel _blockStream;
        private PendingTransactionStreamViewModel _pendingTransactionStream;
        private TransactionStreamViewModel _transactionStream;

        public HomeViewModel(
            Func<BlockStreamViewModel> blockStreamFactory,
            Func<TransactionStreamViewModel> transactionStreamFactory,
            Func<PendingTransactionStreamViewModel> pendingTransactionStreamFactory)
        {
            BlockStreamFactory = blockStreamFactory;
            TransactionStreamFactory = transactionStreamFactory;
            PendingTransactionStreamFactory = pendingTransactionStreamFactory;
        }

        public BlockStreamViewModel BlockStream
        {
            get => _blockStream;
            private set => this.RaiseAndSetIfChanged(ref _blockStream, value);
        }

        public TransactionStreamViewModel TransactionStream
        {
            get => _transactionStream;
            private set => this.RaiseAndSetIfChanged(ref _transactionStream, value);
        }

        public PendingTransactionStreamViewModel PendingTransactionStream
        {
            get => _pendingTransactionStream;
            private set => this.RaiseAndSetIfChanged(ref _pendingTransactionStream, value);
        }

        public void OnNavigatedTo()
        {
            BlockStream = BlockStreamFactory();
            TransactionStream = TransactionStreamFactory();
            PendingTransactionStream = PendingTransactionStreamFactory();
        }

        public void OnNavigatedFrom()
        {
            BlockStream.Dispose();
            TransactionStream.Dispose();
            PendingTransactionStream.Dispose();
        }
    }
}