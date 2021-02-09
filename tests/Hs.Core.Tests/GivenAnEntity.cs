using Hs.Core.Tests.Assets;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Hs.Core.Tests
{
    public class GivenAnEntity
    {
        [Fact]
        public void CallingConstructor_ReturnsATransientInstance()
        {
            var movie = MovieFactory.GetForKids();

            movie.IsTransient().ShouldBeTrue();
        }

        [Fact]
        public void WithCompositeKey_CanCheckEquality()
        {
            var orderId = Guid.NewGuid();

            var item1 = new OrderItem(orderId, 1, "A", 1);
            var item2 = new OrderItem(orderId, 1, "B", 2);

            item1.ShouldBe(item2);
            item1.ShouldNotBeSameAs(item2);
        }

        [Fact]
        public void GetHashCode_ReturnsSame()
        {
            var orderId = Guid.NewGuid();

            var item1 = new OrderItem(orderId, 1, "A", 1);
            var item2 = new OrderItem(orderId, 1, "B", 2);

            item1.GetHashCode().ShouldBe(item2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ReturnsDifferent()
        {
            var orderId = Guid.NewGuid();

            var item1 = new OrderItem(orderId, 1, "A", 1);
            var item2 = new OrderItem(orderId, 2, "A", 1);

            item1.GetHashCode().ShouldNotBe(item2.GetHashCode());
        }

    }
}
