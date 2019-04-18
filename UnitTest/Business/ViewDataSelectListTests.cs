using AutoMapper;
using Business;
using Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interface;
using Repository.Schema;
using System.Collections.Generic;

namespace UnitTest.Business
{
    [TestClass]
    public class ViewDataSelectListTests
    {
        List<CategoryBusinessModel> buCategory;
        List<PlatformBusinessModel> buPlatform;
        List<SubCategoryBusinessModel> buSubCategory;
        List<LexiconEntryTypeBusinessModel> buLexiconEntryType;

        Mock<ICategoryRepository> mockCategoryRepository;
        Mock<IPlatformRepository> mockPlatformRepository;
        Mock<ISubCategoryRepository> mockSubCategoryRepository;
        Mock<ILexiconEntryTypeRepository> mockLexiconEntryTypeRepository;

        LexiconEntryBusiness mockLexiconEntryBusiness;

        Mock<IMapper> mockMapper;

        [TestInitialize]  
        public void TestInitialize()  
        {  
            buCategory = new List<CategoryBusinessModel>() { new CategoryBusinessModel() { Description = "Property", Id = 1 }};
            buPlatform = new List<PlatformBusinessModel>() { new PlatformBusinessModel() { Description = "FrEnd", Id = 1 }};
            buSubCategory = new List<SubCategoryBusinessModel>() { new SubCategoryBusinessModel() { Description = "LDP", Id = 1 }};
            buLexiconEntryType = new List<LexiconEntryTypeBusinessModel>() { new LexiconEntryTypeBusinessModel() { Description = "function", Id = 1 }};

            mockCategoryRepository = new Mock<ICategoryRepository>();
            mockPlatformRepository = new Mock<IPlatformRepository>();
            mockSubCategoryRepository = new Mock<ISubCategoryRepository>();
            mockLexiconEntryTypeRepository = new Mock<ILexiconEntryTypeRepository>();
            mockMapper = new Mock<IMapper>();

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

            mockLexiconEntryBusiness = new LexiconEntryBusiness(
                mockCategoryRepository.Object, 
                mockPlatformRepository.Object, 
                mockSubCategoryRepository.Object, 
                mockLexiconEntryTypeRepository.Object);
        } 

        [TestMethod]
        public void CategorySelectList_ShouldCreateList()
        {
            // Arrange
            var expected = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Property", Value = "1" }
            };

            var classUnderTest = new ViewDataSelectList(mockLexiconEntryBusiness);

            // Act
            var actual = classUnderTest.CategorySelectList(mockMapper.Object);

            // Assert
            // TODO - there must be a better way to compare a list if `SelectListItem`
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Value, actual[0].Value);
        }

        [TestMethod]
        public void PlatformSelectList_ShouldCreateList()
        {
            // Arrange
            var expected = new List<SelectListItem>
            {
                new SelectListItem() { Text = "FrEnd", Value = "1" }
            };

            var classUnderTest = new ViewDataSelectList(mockLexiconEntryBusiness);

            // Act
            var actual = classUnderTest.PlatformSelectList(mockMapper.Object);

            // Assert
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Value, actual[0].Value);
        }

        [TestMethod]
        public void SubCategorySelectList_ShouldCreateList()
        {
            // Arrange
            var expected = new List<SelectListItem>
            {
                new SelectListItem() { Text = "LDP", Value = "1" }
            };

            var classUnderTest = new ViewDataSelectList(mockLexiconEntryBusiness);

            // Act
            var actual = classUnderTest.SubCategorySelectList(mockMapper.Object);

            // Assert
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Value, actual[0].Value);
        }

        [TestMethod]
        public void LexiconEntryTypeSelectList_ShouldCreateList()
        {
            // Arrange
            var expected = new List<SelectListItem>
            {
                new SelectListItem() { Text = "function", Value = "1" }
            };

            var classUnderTest = new ViewDataSelectList(mockLexiconEntryBusiness);

            // Act
            var actual = classUnderTest.LexiconEntryTypeSelectList(mockMapper.Object);

            // Assert
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Value, actual[0].Value);
        }
    }
}
