using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.MsSQL;
using Repository.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        [TestMethod]
        public void InsertAndSelect_ShouldEqualInserted()
        {
            // Arrange
            var dbModel = new CategoryModel()
            {
                Description = "voluptatum",
            };
            var expectedValue = new CategoryRepository(AppState.ConnectionString)
                .Insert(dbModel);

            // Act
            var actualValue = new CategoryRepository(AppState.ConnectionString)
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
            var listPoco = new List<CategoryModel>()
            {
                new CategoryModel()
                {
                    Description = dummyString,
                },
                new CategoryModel()
                {
                    Description = dummyString,
                }
            };

            // Act
            new CategoryRepository(AppState.ConnectionString).InsertBulk(listPoco);
            var actualValue = new CategoryRepository(AppState.ConnectionString)
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
            var dbModel = new CategoryModel()
            {
                Description = "voluptatum",
            };

            // Act
            var newId = new CategoryRepository(AppState.ConnectionString).Insert(dbModel);
            new CategoryRepository(AppState.ConnectionString).Delete(newId);
            var actualValue = new CategoryRepository(AppState.ConnectionString)
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
            var dbModel = new CategoryModel()
            {
                Description = dummyString,
            };

            // Act
            var newId = new CategoryRepository(AppState.ConnectionString)
                .Insert(dbModel);
            var dbModel2 = new CategoryRepository(AppState.ConnectionString)
                .Select(newId);
            dbModel2.Description = expectedValue;

            new CategoryRepository(AppState.ConnectionString)
                .Update(dbModel2);
            var actualValue = new CategoryRepository(AppState.ConnectionString)
                .Select(newId)
                .Description;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
