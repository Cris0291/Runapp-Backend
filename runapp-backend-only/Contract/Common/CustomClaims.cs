using System.Collections;

namespace Contracts.Common
{
    public class CustomClaims : IEnumerable<CustomClaim>
    {
        public List<CustomClaim> Claims { get; init; } = new();

        public IEnumerator<CustomClaim> GetEnumerator()
        {
            return new CustomClaimsEnumerator(Claims);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }
    }
}
