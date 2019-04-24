using AutoMapper;
using Business;
using Business.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interface;
using Repository.Schema;
using System.Collections.Generic;

namespace UnitTest.Business
{
    [TestClass]
    public class EntryBusinessTests
    {
        List<CategoryModel> dbCatergory;
        List<SubCategoryModel> dbSubCategory;
        List<PlatformModel> dbPlatform;

        List<CategoryBusinessModel> buCategory;
        List<SubCategoryBusinessModel> buSubCategory;
        List<PlatformBusinessModel> buPlatform;

        Mock<ICategoryRepository> mockCategoryRepository;
        Mock<ISubCategoryRepository> mockSubCategoryRepository;
        Mock<IPlatformRepository> mockPlatformRepository;

        Mock<IMapper> mockMapper;

        [TestInitialize]  
        public void TestInitialize()  
        {  
            dbCatergory = new List<CategoryModel>() { new CategoryModel() { Description = "Property", Id = 1 } };
            dbSubCategory = new List<SubCategoryModel>() { new SubCategoryModel() { Description = "LDP", Id = 1 } };
            dbPlatform = new List<PlatformModel>() { new PlatformModel() { Description = "FrEnd", Id = 1 } };

            buCategory = new List<CategoryBusinessModel>() { new CategoryBusinessModel() { Description = "Property", Id = 1 }};
            buSubCategory = new List<SubCategoryBusinessModel>() { new SubCategoryBusinessModel() { Description = "LDP", Id = 1 }};
            buPlatform = new List<PlatformBusinessModel>() { new PlatformBusinessModel() { Description = "iOS", Id = 1 }};
            
            mockCategoryRepository = new Mock<ICategoryRepository>();
            mockPlatformRepository = new Mock<IPlatformRepository>();
            mockSubCategoryRepository = new Mock<ISubCategoryRepository>();
            mockMapper = new Mock<IMapper>();

            mockCategoryRepository
                .Setup(m => m.SelectList())
                .Returns(dbCatergory);

            mockPlatformRepository
                .Setup(m => m.SelectList())
                .Returns(dbPlatform);

            mockSubCategoryRepository
                .Setup(m => m.SelectList())
                .Returns(dbSubCategory);

            mockMapper
                .Setup(x => x.Map<List<CategoryBusinessModel>>(It.IsAny<List<CategoryModel>>()))
                .Returns(buCategory);

            mockMapper
                .Setup(x => x.Map<List<SubCategoryBusinessModel>>(It.IsAny<List<SubCategoryModel>>()))
                .Returns(buSubCategory);

            mockMapper
                .Setup(x => x.Map<List<PlatformBusinessModel>>(It.IsAny<List<PlatformModel>>()))
                .Returns(buPlatform);
        } 

        [TestCleanup]  
        public void TestCleanup()  
        {  
            Mapper.Reset(); // this is shit :D
        } 

        [TestMethod]
        public void GetModel_ShouldMapRepository_ToLexiconEntryBusinessModel()
        {
            // Arrange
            var expected = new EntryBusinessModel()
            {
                Category = buCategory,
                SubCategory = buSubCategory
            };

            var classUnderTest = new EntryBusiness(
                mockCategoryRepository.Object, 
                mockSubCategoryRepository.Object,
                mockPlatformRepository.Object);

            // Act
            var actual = classUnderTest.GetModel(mockMapper.Object);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
