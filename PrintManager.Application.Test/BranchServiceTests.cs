using Moq;
using PrintManager.Application.Services;
using PrintManager.Application.Test.Mocks;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Application.Test
{
    public class BranchServiceTests
    {
        [Fact]
        public void GetById_WithValidId_ReturnsBranch()
        {
            // Arrange
            int branchId = 1;

            Branch expectedBranch = BranchMockDataGenerator.GetMockBranches().First();

            Mock<IBranchStore> mockBranchStore = new();

            mockBranchStore.Setup(store => store.GetById(branchId)).Returns(expectedBranch);

            BranchService branchService = new(mockBranchStore.Object);

            // Act
            Branch? result = branchService.GetById(branchId);

            // Assert
            Assert.Equal(expectedBranch, result);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsBranch()
        {
            // Arrange
            int branchId = 1;

            Branch expectedBranch = BranchMockDataGenerator.GetMockBranches().First();

            Mock<IBranchStore> mockBranchStore = new();

            mockBranchStore.Setup(store => store.GetByIdAsync(branchId)).ReturnsAsync(expectedBranch);

            BranchService branchService = new(mockBranchStore.Object);

            // Act
            Branch? result = await branchService.GetByIdAsync(branchId);

            // Assert
            Assert.Equal(expectedBranch, result);
        }

        [Fact]
        public async Task GetByPageAsync_NoPaginationParameters_ReturnsAllBranches()
        {
            // Arrange
            Mock<IBranchStore> mockBranchStore = new();

            List<Branch> branches = BranchMockDataGenerator.GetMockBranches();

            mockBranchStore.Setup(x => x.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(branches);

            BranchService branchService = new(mockBranchStore.Object);

            // Act
            IReadOnlyList<Branch> result = await branchService.GetByPageAsync(null, null);

            // Assert
            Assert.Equal(branches.Count, result.Count);

            foreach (var branch in branches)
            {
                Assert.Contains(branch, result);
            }
        }

        [Fact]
        public async Task GetByPageAsync_WithPaginationParameters_ReturnsRequestedBranches()
        {
            // Arrange
            List<Branch> expectedBranches = BranchMockDataGenerator.GetMockBranches().Take(2).ToList();

            Mock<IBranchStore> mockBranchStore = new();

            mockBranchStore.Setup(store => store.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedBranches);

            BranchService branchService = new(mockBranchStore.Object);

            int page = 1;

            int pageSize = 2;

            // Act
            IReadOnlyList<Branch> result = await branchService.GetByPageAsync(page, pageSize);

            // Assert
            Assert.Equal(pageSize, result.Count);

            foreach (var branch in expectedBranches.GetRange((page - 1) * pageSize, pageSize))
            {
                Assert.Contains(branch, result);
            }
        }

        [Fact]
        public async Task GetByPageAsync_PageGreaterThanTotalPages_ReturnsEmptyList()
        {
            // Arrange
            Mock<IBranchStore> mockBranchStore = new();

            List<Branch> expectedBranches = [];

            mockBranchStore.Setup(x => x.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedBranches);

            BranchService branchService = new(mockBranchStore.Object);

            // Act
            int page = 100;

            int pageSize = 10;

            IReadOnlyList<Branch> result = await branchService.GetByPageAsync(page, pageSize);

            // Assert
            Assert.Empty(result);
        }
    }
}
