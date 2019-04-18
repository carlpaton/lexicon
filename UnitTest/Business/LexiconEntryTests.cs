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
    public class LexiconEntryTests
    {
        List<CategoryModel> dbCatergory;
        List<PlatformModel> dbPlatform;
        List<SubCategoryModel> dbSubCategory;
        List<LexiconEntryTypeModel> dbLexiconEntryType;

        List<CategoryBusinessModel> buCategory;
        List<PlatformBusinessModel> buPlatform;
        List<SubCategoryBusinessModel> buSubCategory;
        List<LexiconEntryTypeBusinessModel> buLexiconEntryType;

        Mock<ICategoryRepository> mockCategoryRepository;
        Mock<IPlatformRepository> mockPlatformRepository;
        Mock<ISubCategoryRepository> mockSubCategoryRepository;
        Mock<ILexiconEntryTypeRepository> mockLexiconEntryTypeRepository;

        Mock<IMapper> mockMapper;

        [TestInitialize]  
        public void TestInitialize()  
        {  
            dbCatergory = new List<CategoryModel>() { new CategoryModel() { Description = "Property", Id = 1 } };
            dbPlatform = new List<PlatformModel>() { new PlatformModel() { Description = "FrEnd", Id = 1 } };
            dbSubCategory = new List<SubCategoryModel>() { new SubCategoryModel() { Description = "LDP", Id = 1 } };
            dbLexiconEntryType = new List<LexiconEntryTypeModel>() { new LexiconEntryTypeModel() { Description = "function", Id = 1 } };

            buCategory = new List<CategoryBusinessModel>() { new CategoryBusinessModel() { Description = "Property", Id = 1 }};
            buPlatform = new List<PlatformBusinessModel>() { new PlatformBusinessModel() { Description = "FrEnd", Id = 1 }};
            buSubCategory = new List<SubCategoryBusinessModel>() { new SubCategoryBusinessModel() { Description = "LDP", Id = 1 }};
            buLexiconEntryType = new List<LexiconEntryTypeBusinessModel>() { new LexiconEntryTypeBusinessModel() { Description = "LDP", Id = 1 }};

            mockCategoryRepository = new Mock<ICategoryRepository>();
            mockPlatformRepository = new Mock<IPlatformRepository>();
            mockSubCategoryRepository = new Mock<ISubCategoryRepository>();
            mockLexiconEntryTypeRepository = new Mock<ILexiconEntryTypeRepository>();
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

            mockLexiconEntryTypeRepository
                .Setup(m => m.SelectList())
                .Returns(dbLexiconEntryType);

            mockMapper
                .Setup(x => x.Map<List<CategoryBusinessModel>>(It.IsAny<List<CategoryModel>>()))
                .Returns(buCategory);

            mockMapper
                .Setup(x => x.Map<List<PlatformBusinessModel>>(It.IsAny<List<PlatformModel>>()))
                .Returns(buPlatform);

            mockMapper
                .Setup(x => x.Map<List<SubCategoryBusinessModel>>(It.IsAny<List<SubCategoryModel>>()))
                .Returns(buSubCategory);

            mockMapper
                .Setup(x => x.Map<List<LexiconEntryTypeBusinessModel>>(It.IsAny<List<LexiconEntryTypeModel>>()))
                .Returns(buLexiconEntryType);
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
            var expected = new LexiconEntryBusinessModel()
            {
                Category = buCategory,
                Platform = buPlatform,
                SubCategory = buSubCategory,
                LexiconEntryType = buLexiconEntryType,
            };

            var classUnderTest = new LexiconEntryBusiness(
                mockCategoryRepository.Object, 
                mockPlatformRepository.Object, 
                mockSubCategoryRepository.Object, 
                mockLexiconEntryTypeRepository.Object);

            // Act
            var actual = classUnderTest.GetModel(mockMapper.Object);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
