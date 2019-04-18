using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.MsSQL;
using Repository.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class LexiconEntryRepositoryTest
    {
        [TestMethod]
        public void InsertAndSelect_ShouldEqualInserted()
        {
            // Arrange
            var dbModel = new LexiconEntryModel()
            {
                CategoryId = 1,
                PlatformId = 1,
                SubCategoryId = 1,
                LexiconEntryTypeId = 1,
                Description = "voluptas",
            };
            var expectedValue = new LexiconEntryRepository(AppState.ConnectionString)
                .Insert(dbModel);

            // Act
            var actualValue = new LexiconEntryRepository(AppState.ConnectionString)
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
            var listPoco = new List<LexiconEntryModel>()
            {
                new LexiconEntryModel()
                {
                    CategoryId = 1,
                    PlatformId = 1,
                    SubCategoryId = 1,
                    LexiconEntryTypeId = 1,
                    Description = dummyString,
                },
                new LexiconEntryModel()
                {
                    CategoryId = 1,
                    PlatformId = 1,
                    SubCategoryId = 1,
                    LexiconEntryTypeId = 1,
                    Description = dummyString,
                }
            };

            // Act
            new LexiconEntryRepository(AppState.ConnectionString).InsertBulk(listPoco);
            var actualValue = new LexiconEntryRepository(AppState.ConnectionString)
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
            var dbModel = new LexiconEntryModel()
            {
                CategoryId = 1,
                PlatformId = 1,
                SubCategoryId = 1,
                LexiconEntryTypeId = 1,
                Description = "voluptas",
            };

            // Act
            var newId = new LexiconEntryRepository(AppState.ConnectionString).Insert(dbModel);
            new LexiconEntryRepository(AppState.ConnectionString).Delete(newId);
            var actualValue = new LexiconEntryRepository(AppState.ConnectionString)
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
            var dbModel = new LexiconEntryModel()
            {
                CategoryId = 1,
                PlatformId = 1,
                SubCategoryId = 1,
                LexiconEntryTypeId = 1,
                Description = dummyString,
            };

            // Act
            var newId = new LexiconEntryRepository(AppState.ConnectionString)
                .Insert(dbModel);
            var dbModel2 = new LexiconEntryRepository(AppState.ConnectionString)
                .Select(newId);
            dbModel2.Description = expectedValue;

            new LexiconEntryRepository(AppState.ConnectionString)
                .Update(dbModel2);
            var actualValue = new LexiconEntryRepository(AppState.ConnectionString)
                .Select(newId)
                .Description;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
