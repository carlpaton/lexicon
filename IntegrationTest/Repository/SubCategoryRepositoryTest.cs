using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.MsSQL;
using Repository.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class SubCategoryRepositoryTest
    {
        [TestMethod]
        public void InsertAndSelect_ShouldEqualInserted()
        {
            // Arrange
            var dbModel = new SubCategoryModel()
            {
                Description = "voluptatem",
            };
            var expectedValue = new SubCategoryRepository(AppState.ConnectionString)
                .Insert(dbModel);

            // Act
            var actualValue = new SubCategoryRepository(AppState.ConnectionString)
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
            var listPoco = new List<SubCategoryModel>()
            {
                new SubCategoryModel()
                {
                    Description = dummyString,
                },
                new SubCategoryModel()
                {
                    Description = dummyString,
                }
            };

            // Act
            new SubCategoryRepository(AppState.ConnectionString).InsertBulk(listPoco);
            var actualValue = new SubCategoryRepository(AppState.ConnectionString)
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
            var dbModel = new SubCategoryModel()
            {
                Description = "quibusdam",
            };

            // Act
            var newId = new SubCategoryRepository(AppState.ConnectionString).Insert(dbModel);
            new SubCategoryRepository(AppState.ConnectionString).Delete(newId);
            var actualValue = new SubCategoryRepository(AppState.ConnectionString)
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
            var dbModel = new SubCategoryModel()
            {
                Description = dummyString,
            };

            // Act
            var newId = new SubCategoryRepository(AppState.ConnectionString)
                .Insert(dbModel);
            var dbModel2 = new SubCategoryRepository(AppState.ConnectionString)
                .Select(newId);
            dbModel2.Description = expectedValue;

            new SubCategoryRepository(AppState.ConnectionString)
                .Update(dbModel2);
            var actualValue = new SubCategoryRepository(AppState.ConnectionString)
                .Select(newId)
                .Description;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
