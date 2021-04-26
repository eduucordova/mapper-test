using MapperServices;
using System;
using Xunit;

namespace MapperTests.OneToManyMapperTests
{
    public class OneToManyMapperConstraints
    {
        readonly IOneToManyMapper _mapper;

        public OneToManyMapperConstraints()
        {
            _mapper = new OneToManyMapper();
            _mapper.Add(10, 100);
            _mapper.Add(10, 101);
            _mapper.Add(10, 102);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(131073, 1)]
        [InlineData(1, 131073)]
        public void ValidateAddConstraints(int parent, int child)
        {
            Assert.Throws<ArgumentException>(() => _mapper.Add(parent, child));
        }

        [Theory]
        [InlineData(100, 0)]
        [InlineData(100, 131073)]
        public void ValidateUpdateChildConstraints(int oldChild, int newChild)
        {
            Assert.Throws<ArgumentException>(() => _mapper.UpdateChild(oldChild, newChild));
        }

        [Fact]
        public void UpdateInexistentChild()
        {
            var inexistentChild = 200;

            Assert.Throws<Exception>(() => _mapper.UpdateChild(inexistentChild, 100));
        }

        [Fact]
        public void UpdateChildToAnExistentOne()
        {
            var newChild = 103;

            _mapper.Add(10, newChild);

            Assert.Throws<ArgumentException>(() => _mapper.UpdateChild(100, newChild));
        }

        [Fact]
        public void RemoveInexistentChild()
        {
            Assert.Throws<Exception>(() => _mapper.RemoveChild(500));
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(10, 131073)]
        public void ValidateUpdateParentConstraints(int oldParent, int newParent)
        {
            Assert.Throws<ArgumentException>(() => _mapper.UpdateParent(oldParent, newParent));
        }

        [Fact]
        public void UpdateInexistentParent()
        {
            var inexistentParent = 20;

            Assert.Throws<Exception>(() => _mapper.UpdateParent(inexistentParent, 20));
        }

        [Fact]
        public void UpdateParentToAnExistentOne()
        {
            var newParent = 30;

            _mapper.Add(30, 300);

            Assert.Throws<ArgumentException>(() => _mapper.UpdateParent(10, newParent));
        }

        [Fact]
        public void RemoveInexistentParent()
        {
            Assert.Throws<Exception>(() => _mapper.RemoveParent(50));
        }
    }
}
