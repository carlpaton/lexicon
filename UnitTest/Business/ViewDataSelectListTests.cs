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

        Mock<ICategoryRepository> mockCategoryRepository;
        Mock<IPlatformRepository> mockPlatformRepository;
        Mock<ISubCategoryRepository> mockSubCategoryRepository;

        EntryBusiness mockEntryBusiness;

        Mock<IMapper> mockMapper;

        [TestInitialize]  
        public void TestInitialize()  
        {  
            buCategory = new List<CategoryBusinessModel>() { new CategoryBusinessModel() { Description = "Property", Id = 1 }};
            buSubCategory = new List<SubCategoryBusinessModel>() { new SubCategoryBusinessModel() { Description = "LDP", Id = 1 }};
            buPlatform = new List<PlatformBusinessModel>() { new PlatformBusinessModel() { Description = "iOS", Id = 1 }};

            mockCategoryRepository = new Mock<ICategoryRepository>();
            mockPlatformRepository = new Mock<IPlatformRepository>();
            mockSubCategoryRepository = new Mock<ISubCategoryRepository>();
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

            mockEntryBusiness = new EntryBusiness(
                mockCategoryRepository.Object, 
                mockSubCategoryRepository.Object,
                mockPlatformRepository.Object);
        } 

        [TestMethod]
        public void CategorySelectList_ShouldCreateList()
        {
            // Arrange
            var expected = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Property", Value = "1" }
            };

            var classUnderTest = new ViewDataSelectList(mockEntryBusiness);

            // Act
            var actual = classUnderTest.CategorySelectList(mockMapper.Object);

            // Assert
            // TODO - there must be a better way to compare a list if `SelectListItem`
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

            var classUnderTest = new ViewDataSelectList(mockEntryBusiness);

            // Act
            var actual = classUnderTest.SubCategorySelectList(mockMapper.Object);

            // Assert
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Value, actual[0].Value);
        }

        [TestMethod]
        public void PlatformSelectList_ShouldCreateList()
        {
            // Arrange
            var expected = new List<SelectListItem>
            {
                new SelectListItem() { Text = "iOS", Value = "1" }
            };

            var classUnderTest = new ViewDataSelectList(mockEntryBusiness);

            // Act
            var actual = classUnderTest.PlatformSelectList(mockMapper.Object);

            // Assert
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Value, actual[0].Value);
        }
    }
}
