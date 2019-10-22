using NUnit.Framework;

namespace Tests.UnitTest
{
    public class Product
    {
        [Test]
        public void Test1()
        {
            var product = WishList.Domain.Entities.Product.Create("ssss");

            Assert.IsTrue(product.Success);
        }

        [Test]
        public void Test2()
        {
            var product = WishList.Domain.Entities.Product.Create(-1,"ssss");

            Assert.IsTrue(product.Success);
        }
    }
}