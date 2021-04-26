using MapperServices;
using System.Linq;
using Xunit;

namespace MapperTests.OneToManyMapperTests
{
    public class OneToManyMapperTests
    {
        readonly IOneToManyMapper _mapper;

        public OneToManyMapperTests()
        {
            _mapper = new OneToManyMapper();
            _mapper.Add(10, 100);
            _mapper.Add(10, 101);
            _mapper.Add(10, 102);
            _mapper.Add(20, 200);
            _mapper.Add(20, 201);
            _mapper.Add(30, 300);
            _mapper.Add(30, 301);
            _mapper.Add(40, 400);
            _mapper.Add(40, 401);
            _mapper.Add(40, 402);
        }

        [Fact]
        public void ValidateGetChildren()
        {
            //Arrange
            int[] expectedResult = new int[3] { 100, 101, 102 };

            //Act
            var actual = _mapper.GetChildren(10);

            //Assert
            Assert.Equal(expectedResult, actual);
        }

        [Fact]
        public void ValidateGetParent()
        {
            //Arrange
            int expectedResult = 10;

            //Act
            var actual = _mapper.GetParent(100);

            //Assert
            Assert.Equal(expectedResult, actual);
        }

        [Fact]
        public void RemoveChild()
        {
            //Arrange
            int[] expectedChildrens = new int[2] { 100, 102 };
            int expectedParent = 0;
            _mapper.RemoveChild(101);

            //Act
            var childrens = _mapper.GetChildren(10);
            var parent = _mapper.GetParent(101);

            //Assert
            Assert.Equal(expectedChildrens, childrens);
            Assert.Equal(expectedParent, parent);
        }

        [Fact]
        public void RemoveAllChilds()
        {
            //Arrange
            int[] expectedChildrens = new int[0] { };
            var childs = _mapper.GetChildren(10).ToList();
            foreach (var child in childs)
            {
                _mapper.RemoveChild(child);
            }

            //Act
            var childrens = _mapper.GetChildren(10);

            //Assert
            Assert.Equal(expectedChildrens, childrens);
        }

        [Fact]
        public void RemoveParent()
        {
            //Arrange
            int[] expectedChildrens = new int[0] { };
            // Parentless childs have to return 0 for their parents
            int expectedParent = 0;
            _mapper.RemoveParent(40);

            //Act
            var childrens = _mapper.GetChildren(40);
            var parent400 = _mapper.GetParent(400);
            var parent401 = _mapper.GetParent(401);
            var parent402 = _mapper.GetParent(403);

            //Assert
            Assert.Equal(expectedChildrens, childrens);
            Assert.Equal(expectedParent, parent400);
            Assert.Equal(expectedParent, parent401);
            Assert.Equal(expectedParent, parent402);
        }

        [Fact]
        public void UpdateParent()
        {
            //Arrange
            int[] expectedChildrens = new int[2] { 200, 201 };
            int expectedParent = 21;
            _mapper.UpdateParent(20, 21);

            //Act
            var childrens = _mapper.GetChildren(21);
            var parent0 = _mapper.GetParent(200);
            var parent1 = _mapper.GetParent(201);

            //Assert
            Assert.Equal(expectedChildrens, childrens);
            Assert.Equal(expectedParent, parent0);
            Assert.Equal(expectedParent, parent1);
        }

        [Fact]
        public void UpdateChild()
        {
            //Arrange
            int[] expectedChildrens = new int[2] { 301, 302 };
            int expectedParent = 30;
            int expectedParent300 = 0;
            _mapper.UpdateChild(300, 302);

            //Act
            var childrens = _mapper.GetChildren(30);
            var parent300 = _mapper.GetParent(300);
            var parent301 = _mapper.GetParent(301);
            var parent302 = _mapper.GetParent(302);

            //Assert
            Assert.Equal(expectedChildrens, childrens);
            Assert.Equal(expectedParent300, parent300);
            Assert.Equal(expectedParent, parent301);
            Assert.Equal(expectedParent, parent302);
        }
    }
}
