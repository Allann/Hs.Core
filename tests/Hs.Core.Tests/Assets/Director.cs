using Hs.Core.Domain;

namespace Hs.Core.Tests.Assets
{
    public class Director : Entity
    {
        public string Name { get; }

        public Director(string name)
        {
            Name = name;
        }
    }
}
