using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.MsSQL;
using Repository.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class EntryPlatformRepositoryTest
    {
        [TestMethod]
        public void InsertAndSelect_ShouldEqualInserted()
        {
            // Arrange
            var dbModel = new EntryPlatformModel()
            {
                EntryId = 6,
                PlatformId = 1,
                Description = "voluptate",
            };
            var expectedValue = new EntryPlatformRepository(AppState.ConnectionString)
                .Insert(dbModel);

            // Act
            var actualValue = new EntryPlatformRepository(AppState.ConnectionString)
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
            var listPoco = new List<EntryPlatformModel>()
            {
                new EntryPlatformModel()
                {
                    EntryId = 6,
                    PlatformId = 1,
                    Description = dummyString,
                },
                new EntryPlatformModel()
                {
                    EntryId = 6,
                    PlatformId = 2,
                    Description = dummyString,
                }
            };

            // Act
            new EntryPlatformRepository(AppState.ConnectionString).InsertBulk(listPoco);
            var actualValue = new EntryPlatformRepository(AppState.ConnectionString)
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
            var dbModel = new EntryPlatformModel()
            {
                EntryId = 6,
                PlatformId = 1,
                Description = "voluptate",
            };

            // Act
            var newId = new EntryPlatformRepository(AppState.ConnectionString).Insert(dbModel);
            new EntryPlatformRepository(AppState.ConnectionString).Delete(newId);
            var actualValue = new EntryPlatformRepository(AppState.ConnectionString)
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
            var dbModel = new EntryPlatformModel()
            {
                EntryId = 6,
                PlatformId = 1,
                Description = dummyString,
            };

            // Act
            var newId = new EntryPlatformRepository(AppState.ConnectionString)
                .Insert(dbModel);
            var dbModel2 = new EntryPlatformRepository(AppState.ConnectionString)
                .Select(newId);
            dbModel2.Description = expectedValue;

            new EntryPlatformRepository(AppState.ConnectionString)
                .Update(dbModel2);
            var actualValue = new EntryPlatformRepository(AppState.ConnectionString)
                .Select(newId)
                .Description;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
