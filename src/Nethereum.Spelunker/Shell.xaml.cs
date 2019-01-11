using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using Nethereum.RPC.Reactive.Polling;
using Nethereum.Spelunker.Modules.Blockchain.ViewModels;
using Nethereum.Spelunker.Modules.Home.ViewModels;
using Nethereum.Spelunker.ViewModels;

namespace Nethereum.Spelunker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Polling.DefaultPoller = Observable.Timer(TimeSpan.FromSeconds(1)).Select(_ => Unit.Default);

            var web3 = new Web3.Web3();

            // Home
            var blockStreamFactory = new Func<BlockStreamViewModel>(() => new BlockStreamViewModel(web3.Eth));
            var transactionStreamFactory = new Func<TransactionStreamViewModel>(() => new TransactionStreamViewModel(web3.Eth));
            var pendingTransactionStreamFactory = new Func<PendingTransactionStreamViewModel>(() => new PendingTransactionStreamViewModel(web3.Eth));

            var home = new HomeViewModel(
                blockStreamFactory,
                transactionStreamFactory,
                pendingTransactionStreamFactory);

            // Blockchain
            var transactions = new TransactionsViewModel();

            var vm = new MainViewModel(home, transactions);

            DataContext = vm;
        }
    }
}