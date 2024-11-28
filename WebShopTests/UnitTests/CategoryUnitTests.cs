using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Repository.Models;
using Repository.Repositories.Categories;

namespace WebShopTests.UnitTests
{
    public class CategoryUnitTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        public CategoryUnitTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
        }

        [Fact]
        public void GetAllCategories_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" },
                new Category { Id = 3, Name = "Category 3" }
            };
            _mockCategoryRepository.Setup(x => x.GetAll()).Returns(categories);

            // Act
            var result = _mockCategoryRepository.Object.GetAll();

            // Assert
            Assert.Equal(categories, result);
        }

        [Fact]
        public void GetAllCategories_ReturnsNull()
        {
            // Arrange
            _mockCategoryRepository.Setup(x => x.GetAll()).Returns(() => null);

            // Act
            var result = _mockCategoryRepository.Object.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCategoryById_ReturnsCategory()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category 1" };
            _mockCategoryRepository.Setup(x => x.GetById(category.Id)).Returns(category);

            // Act
            var result = _mockCategoryRepository.Object.GetById(category.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(category, result);
        }

        [Fact]
        public void GetCategoryById_ReturnsNull()
        {
            // Arrange
            _mockCategoryRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => null);

            // Act
            var result = _mockCategoryRepository.Object.GetById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddCategory_ReturnsCategory()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category 1" };
            _mockCategoryRepository.Setup(x => x.Add(category));

            // Act
            _mockCategoryRepository.Object.Add(category);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.Add(It.Is<Category>(c => c.Id == category.Id && c.Name == category.Name)), Times.Once);
        }

        [Fact]
        public void AddCategory_ReturnsNull()
        {
            // Arrange
            Category category = null;

            // Assert & Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockCategoryRepository.Object.Add(category));
        }

        [Fact]
        public void UpdateCategory_ReturnsOkResult()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category 1" };
            _mockCategoryRepository.Setup(repo => repo.Update(category));

            // Act
            _mockCategoryRepository.Object.Update(category);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.Update(It.Is<Category>(c => c.Id == category.Id)), Times.Once);
        }

        [Fact]
        public void UpdateCategory_ReturnsNull()
        {
            // Arrange
            Category category = null;

            // Assert & Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockCategoryRepository.Object.Update(category));
        }

        [Fact]
        public void DeleteCategory_ReturnsOkResult()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category 1" };
            _mockCategoryRepository.Setup(repo => repo.Delete(category));

            // Act
            _mockCategoryRepository.Object.Delete(category);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.Delete(It.Is<Category>(c => c.Id == category.Id)), Times.Once);
        }

        [Fact]
        public void DeleteCategory_ReturnsNull()
        {
            // Arrange
            Category category = null;

            // Assert & Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockCategoryRepository.Object.Delete(category));
        }
    }
}
