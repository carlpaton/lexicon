using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.MsSQL;
using Repository.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class LexiconEntryTypeRepositoryTest
    {
        [TestMethod]
        public void InsertAndSelect_ShouldEqualInserted()
        {
            // Arrange
            var dbModel = new LexiconEntryTypeModel()
            {
                Description = "labore",
            };
            var expectedValue = new LexiconEntryTypeRepository(AppState.ConnectionString)
                .Insert(dbModel);

            // Act
            var actualValue = new LexiconEntryTypeRepository(AppState.ConnectionString)
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
            var listPoco = new List<LexiconEntryTypeModel>()
            {
                new LexiconEntryTypeModel()
                {
                    Description = dummyString,
                },
                new LexiconEntryTypeModel()
                {
                    Description = dummyString,
                }
            };

            // Act
            new LexiconEntryTypeRepository(AppState.ConnectionString).InsertBulk(listPoco);
            var actualValue = new LexiconEntryTypeRepository(AppState.ConnectionString)
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
            var dbModel = new LexiconEntryTypeModel()
            {
                Description = "labore",
            };

            // Act
            var newId = new LexiconEntryTypeRepository(AppState.ConnectionString).Insert(dbModel);
            new LexiconEntryTypeRepository(AppState.ConnectionString).Delete(newId);
            var actualValue = new LexiconEntryTypeRepository(AppState.ConnectionString)
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
            var dbModel = new LexiconEntryTypeModel()
            {
                Description = dummyString,
            };

            // Act
            var newId = new LexiconEntryTypeRepository(AppState.ConnectionString)
                .Insert(dbModel);
            var dbModel2 = new LexiconEntryTypeRepository(AppState.ConnectionString)
                .Select(newId);
            dbModel2.Description = expectedValue;

            new LexiconEntryTypeRepository(AppState.ConnectionString)
                .Update(dbModel2);
            var actualValue = new LexiconEntryTypeRepository(AppState.ConnectionString)
                .Select(newId)
                .Description;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
