using System.Collections;

namespace Contracts.Common
{
    public class CustomClaimsEnumerator(List<CustomClaim> iterator) : IEnumerator<CustomClaim>
    {
        private List<CustomClaim> _iterator = iterator;
        private int _index = -1;
        public CustomClaim Current => _iterator[_index];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            _index++;
           return _index < _iterator.Count;
        }

        public void Reset()
        {
            _index = 0;
        }
    }
}
