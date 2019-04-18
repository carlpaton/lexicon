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
    public class LexiconEntryTypeControllerTests
    {
        [TestMethod, Description("Get with no parameters returns a list of all: SelectList")]
        public void Get_WithNoParameters_ShouldReturnList()
        {
            // Arrange
            var mockRepository = new Mock<ILexiconEntryTypeRepository>();
            var expectedValue = new List<LexiconEntryTypeModel>()
            {
                new LexiconEntryTypeModel()
                {
                    Id = 8462,
                    Description = "aut"
                }
            };

            mockRepository
                .Setup(m => m.SelectList())
                .Returns(expectedValue);

            var controller = new LexiconEntryTypeController(mockRepository.Object);

            // Act
            var result = controller.Get();
            var actualValue = (List<LexiconEntryTypeModel>)result.Value;

            // Assert
            CollectionAssert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod, Description("Get with one int parameter returns a database object: Select")]
        public void Get_WithParameter42_ShouldReturnDbModel()
        {
            // Arrange
            var actualId = 2474;
            var mockRepository = new Mock<ILexiconEntryTypeRepository>();
            var expectedValue = new LexiconEntryTypeModel()
            {
                Id = actualId,
                Description = "magnam"
            };

            mockRepository
                .Setup(m => m.Select(actualId))
                .Returns(expectedValue);

            var controller = new LexiconEntryTypeController(mockRepository.Object);

            // Act
            var result = controller.Get(actualId) as JsonResult;
            var actualValue = (LexiconEntryTypeModel)result.Value;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod, Description("Post with database object returns the new Id: Insert")]
        public void Post_WithMockedModel_ShouldReturnNewId()
        {
            // Arrange
            var mockRepository = new Mock<ILexiconEntryTypeRepository>();
            var dbModel = new LexiconEntryTypeModel();
            var expectedValue = 2474;

            mockRepository
                .Setup(m => m.Insert(dbModel))
                .Returns(expectedValue);

            var controller = new LexiconEntryTypeController(mockRepository.Object);

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
            var actualId = 2474;
            var dummyString = Guid.NewGuid().ToString().Replace("-", "");
            var mockRepository = new Mock<ILexiconEntryTypeRepository>();
            var dbModel = new LexiconEntryTypeModel()
            {
                Description = dummyString,
            };

            mockRepository
                .Setup(m => m.Update(dbModel));

            var controller = new LexiconEntryTypeController(mockRepository.Object);

            // Act
            controller.Put(actualId, dbModel);

            // Assert
            mockRepository
                .Verify(m => m.Update(
                        It.Is<LexiconEntryTypeModel>(
                            i => i.Id == actualId && i.Description == dummyString)), 
                    Times.Once());
        }

        [TestMethod, Description("Put with record Id returns void: Delete")]
        public void Delete_WithParameter42_ShouldBeCalledOnce()
        {
            // Arrange
            var actualId = 2474;
            var mockRepository = new Mock<ILexiconEntryTypeRepository>();

            mockRepository
                .Setup(m => m.Delete(actualId));

            var controller = new LexiconEntryTypeController(mockRepository.Object);

            // Act
            controller.Delete(actualId);

            // Assert
            mockRepository
                .Verify(m => m.Delete(actualId), Times.Once());
        }
    }
}
