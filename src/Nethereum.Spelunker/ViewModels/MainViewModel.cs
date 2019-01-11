using System.Reactive;
using Nethereum.Spelunker.Modules.Blockchain.ViewModels;
using Nethereum.Spelunker.Modules.Home.ViewModels;
using ReactiveUI;

namespace Nethereum.Spelunker.ViewModels
{
    public sealed class MainViewModel : ReactiveObject
    {
        private readonly HomeViewModel Home;
        private readonly TransactionsViewModel Transactions;

        private INavigationAware _activePage;

        public MainViewModel(
            HomeViewModel home,
            TransactionsViewModel transactions)
        {
            Home = home;
            Transactions = transactions;

            ActivePage = Home;

            GoToHomeCommand = ReactiveCommand.Create(GoToHome);
            GoToTransactionsCommand = ReactiveCommand.Create(GoToTransactions);
        }

        public ReactiveCommand<Unit, Unit> GoToHomeCommand { get; }
        public ReactiveCommand<Unit, Unit> GoToTransactionsCommand { get; }

        public INavigationAware ActivePage
        {
            get => _activePage;
            private set
            {
                if (_activePage == value) return;

                _activePage?.OnNavigatedFrom();
                this.RaiseAndSetIfChanged(ref _activePage, value);

                _activePage?.OnNavigatedTo();
            }
        }

        private void GoToTransactions() => ActivePage = Transactions;

        private void GoToHome() => ActivePage = Home;
    }
}