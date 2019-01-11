using System;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Windows.Data;

namespace Nethereum.Spelunker.Converters
{
    public sealed class WeiToEthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            return Web3.Web3.Convert.FromWei((BigInteger) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}