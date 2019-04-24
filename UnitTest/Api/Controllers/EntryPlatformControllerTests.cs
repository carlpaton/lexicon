using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interface;
using Repository.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UnitTest.Api.Controllers
{
    [TestClass]
    public class EntryPlatformControllerTests
    {
        [TestMethod, Description("Get with no parameters returns a list of all: SelectList")]
        public void Get_WithNoParameters_ShouldReturnList()
        {
            // Arrange
            var mockRepository = new Mock<IEntryPlatformRepository>();
            var expectedValue = new List<EntryPlatformModel>()
            {
                new EntryPlatformModel()
                {
                    Id = 8413,
                    EntryId = 8414,
                    PlatformId = 8415,
                    Description = "blanditiis"
                }
            };

            mockRepository
                .Setup(m => m.SelectList())
                .Returns(expectedValue);

            var controller = new EntryPlatformController(mockRepository.Object);

            // Act
            var result = controller.Get();
            var actualValue = (List<EntryPlatformModel>)result.Value;

            // Assert
            CollectionAssert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod, Description("Get with one int parameter returns a database object: Select")]
        public void Get_WithParameter42_ShouldReturnDbModel()
        {
            // Arrange
            var actualId = 8412;
            var mockRepository = new Mock<IEntryPlatformRepository>();
            var expectedValue = new EntryPlatformModel()
            {
                Id = actualId,
                EntryId = 8414,
                PlatformId = 8415,
                Description = "blanditiis"
            };

            mockRepository
                .Setup(m => m.Select(actualId))
                .Returns(expectedValue);

            var controller = new EntryPlatformController(mockRepository.Object);

            // Act
            var result = controller.Get(actualId) as JsonResult;
            var actualValue = (EntryPlatformModel)result.Value;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod, Description("Post with database object returns the new Id: Insert")]
        public void Post_WithMockedModel_ShouldReturnNewId()
        {
            // Arrange
            var mockRepository = new Mock<IEntryPlatformRepository>();
            var dbModel = new EntryPlatformModel();
            var expectedValue = 8412;

            mockRepository
                .Setup(m => m.Insert(dbModel))
                .Returns(expectedValue);

            var controller = new EntryPlatformController(mockRepository.Object);

            // Act
            var result = controller.Post(dbModel) as JsonResult;
            var actualValue = (int)result.Value;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod, Description("Put with record Id and database object returns void: Update")]
        public void Put_WithMockedModel_ShouldBeCalledOnce()
        {
            // Arrange
            var actualId = 8412;
            var dummyString = Guid.NewGuid().ToString().Replace("-", "");
            var mockRepository = new Mock<IEntryPlatformRepository>();
            var dbModel = new EntryPlatformModel()
            {
                EntryId = 8413,
                PlatformId = 8414,
                Description = dummyString,
            };

            mockRepository
                .Setup(m => m.Update(dbModel));

            var controller = new EntryPlatformController(mockRepository.Object);

            // Act
            controller.Put(actualId, dbModel);

            // Assert
            mockRepository
                .Verify(m => m.Update(
                        It.Is<EntryPlatformModel>(
                            i => i.Id == actualId && i.Description == dummyString)), 
                    Times.Once());
        }

        [TestMethod, Description("Put with record Id returns void: Delete")]
        public void Delete_WithParameter42_ShouldBeCalledOnce()
        {
            // Arrange
            var actualId = 8412;
            var mockRepository = new Mock<IEntryPlatformRepository>();

            mockRepository
                .Setup(m => m.Delete(actualId));

            var controller = new EntryPlatformController(mockRepository.Object);

            // Act
            controller.Delete(actualId);

            // Assert
            mockRepository
                .Verify(m => m.Delete(actualId), Times.Once());
        }
    }
}
