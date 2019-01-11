using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Reactive.Polling;
using ReactiveUI;

namespace Nethereum.Spelunker.Modules.Home.ViewModels
{
    public class BlockStreamViewModel : ReactiveObject, IDisposable
    {
        private readonly CompositeDisposable Subscriptions;

        private List<BlockViewModel> _blocks;

        public BlockStreamViewModel(IEthApiService eth)
        {
            Blocks = new List<BlockViewModel>();

            Subscriptions = new CompositeDisposable
            {
                Observable.FromAsync(() => eth.Blocks.GetBlockNumber.SendRequestAsync())
                    .SelectMany(blockNumber =>
                        eth.GetBlocksWithTransactionHashes(new BlockParameter(new HexBigInteger(blockNumber.Value - 10))))
                    .ObserveOnDispatcher()
                    .Select(BlockViewModel.FromBlock)
                    .Subscribe(block =>
                    {
                        Blocks.Add(block);
                        Blocks = Blocks
                            .OrderByDescending(b => b.BlockNumber)
                            .Take(10)
                            .ToList();
                    })
            };
        }

        public List<BlockViewModel> Blocks
        {
            get => _blocks;
            private set => this.RaiseAndSetIfChanged(ref _blocks, value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Subscriptions?.Dispose();
        }
    }
}