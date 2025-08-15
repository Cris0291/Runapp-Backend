
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestsUtilities")]
namespace RunApp.Domain.ProductAggregate.ValueType
{
    public class About
    {
        internal About() { }
        // Constructor use for unit testing
        internal About(string bulletpoint)
        {
            BulletPoint = bulletpoint;
        }
        public string BulletPoint { get; internal set; }

    }
}
