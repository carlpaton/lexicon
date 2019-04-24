using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.MsSQL;
using Repository.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class EntryRepositoryTest
    {
        [TestMethod]
        public void InsertAndSelect_ShouldEqualInserted()
        {
            // Arrange
            var dbModel = new EntryModel()
            {
                CategoryId = 1,
                SubCategoryId = 1,
                LexiconFunction = "in",
                Recommendation = "et",
                Notes = "ducimus",
            };
            var expectedValue = new EntryRepository(AppState.ConnectionString)
                .Insert(dbModel);

            // Act
            var actualValue = new EntryRepository(AppState.ConnectionString)
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
            var listPoco = new List<EntryModel>()
            {
                new EntryModel()
                {
                    CategoryId = 1,
                    SubCategoryId = 1,
                    LexiconFunction = dummyString,
                    Recommendation = "et",
                    Notes = "nobis",
                },
                new EntryModel()
                {
                    CategoryId = 2,
                    SubCategoryId = 2,
                    LexiconFunction = dummyString,
                    Recommendation = "neque",
                    Notes = "nobis",
                }
            };

            // Act
            new EntryRepository(AppState.ConnectionString).InsertBulk(listPoco);
            var actualValue = new EntryRepository(AppState.ConnectionString)
                .SelectList()
                .Where(x => x.LexiconFunction.Equals(dummyString))
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
            var dbModel = new EntryModel()
            {
                CategoryId = 1,
                SubCategoryId = 1,
                LexiconFunction = "rerum",
                Recommendation = "neque",
                Notes = "nobis",
            };

            // Act
            var newId = new EntryRepository(AppState.ConnectionString).Insert(dbModel);
            new EntryRepository(AppState.ConnectionString).Delete(newId);
            var actualValue = new EntryRepository(AppState.ConnectionString)
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
            var dbModel = new EntryModel()
            {
                CategoryId = 1,
                SubCategoryId = 1,
                LexiconFunction = dummyString,
                Recommendation = "neque",
                Notes = "nobis",
            };

            // Act
            var newId = new EntryRepository(AppState.ConnectionString)
                .Insert(dbModel);
            var dbModel2 = new EntryRepository(AppState.ConnectionString)
                .Select(newId);
            dbModel2.LexiconFunction = expectedValue;

            new EntryRepository(AppState.ConnectionString)
                .Update(dbModel2);
            var actualValue = new EntryRepository(AppState.ConnectionString)
                .Select(newId)
                .LexiconFunction;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
