using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.MsSQL;
using Repository.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class PlatformRepositoryTest
    {
        [TestMethod]
        public void InsertAndSelect_ShouldEqualInserted()
        {
            // Arrange
            var dbModel = new PlatformModel()
            {
                Description = "ex",
            };
            var expectedValue = new PlatformRepository(AppState.ConnectionString)
                .Insert(dbModel);

            // Act
            var actualValue = new PlatformRepository(AppState.ConnectionString)
                .Select(expectedValue)
                .Id;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void InsertBulkThenSelectList_ShouldEqualTwo()
        {
            // Arrange
            var expectedValue = 2;
            var dummyString = Guid.NewGuid().ToString().Replace("-", "");
            var listPoco = new List<PlatformModel>()
            {
                new PlatformModel()
                {
                    Description = dummyString,
                },
                new PlatformModel()
                {
                    Description = dummyString,
                }
            };

            // Act
            new PlatformRepository(AppState.ConnectionString).InsertBulk(listPoco);
            var actualValue = new PlatformRepository(AppState.ConnectionString)
                .SelectList()
                .Where(x => x.Description.Equals(dummyString))
                .ToList()
                .Count;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void InsertAndDelete_ShouldNoLongerExistAfterDelete()
        {
            // Arrange
            var expectedValue = 0;
            var dbModel = new PlatformModel()
            {
                Description = "ex",
            };

            // Act
            var newId = new PlatformRepository(AppState.ConnectionString).Insert(dbModel);
            new PlatformRepository(AppState.ConnectionString).Delete(newId);
            var actualValue = new PlatformRepository(AppState.ConnectionString)
                .Select(newId)
                .Id;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void InsertThenUpdate_ShouldReflectChanges()
        {
            // Arrange
            var expectedValue = "bar@domain.com";
            var dummyString = Guid.NewGuid().ToString().Replace("-", "");
            var dbModel = new PlatformModel()
            {
                Description = dummyString,
            };

            // Act
            var newId = new PlatformRepository(AppState.ConnectionString)
                .Insert(dbModel);
            var dbModel2 = new PlatformRepository(AppState.ConnectionString)
                .Select(newId);
            dbModel2.Description = expectedValue;

            new PlatformRepository(AppState.ConnectionString)
                .Update(dbModel2);
            var actualValue = new PlatformRepository(AppState.ConnectionString)
                .Select(newId)
                .Description;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
