using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace PocoToStringer
{
    public interface IPocoFormatter
    {
        void Add(string toAdd, string parameterName);
        string ToString();
    }

    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class DefaultPocoFormatter : IPocoFormatter
    {
        private bool _isFirstPropertyAdd;
        private StringBuilder _stringBuilder;
        public DefaultPocoFormatter()
        {
            _stringBuilder = new StringBuilder(200);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(string toAdd, string parameterName)
        {
            if (string.IsNullOrEmpty(toAdd)) return;
            if (_isFirstPropertyAdd)
                _stringBuilder.Append(", ");
            _stringBuilder.Append(parameterName);
            _stringBuilder.Append(": ");
            _stringBuilder.Append(toAdd);
            _isFirstPropertyAdd = true;
        }

        public override string ToString() => _stringBuilder.ToString();

    }
}