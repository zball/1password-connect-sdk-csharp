using System;
using OpConnectSdk.Lib.Filter;
using OpConnectSdk.Model;
using Xunit;

namespace OpConnectSdkTest
{
    public class FilterBuilderTest
    {
        # region Private Members

        private FilterBuilder<Item> _sut;

        # endregion Private Members

        public FilterBuilderTest()
        {
            _sut = new FilterBuilder<Item>();
        }

        [Fact]
        public void FilterBuilder_Field_Gets_Field_Name_From_Lambda_With_Lowercased_First_Letter()
        {
            // Act
            _sut.Field(item => item.CreatedAt);

            // Assert
            Assert.Equal("createdAt", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_Throws_Error_When_Lambda_Doesnt_Return_A_Property()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Field(item => 5));
        }

        [Fact]
        public void FilterBuilder_Group_Adds_Open_Paren() {
            // Act
            _sut.Group();

            // Assert
            Assert.Equal("(", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_GroupEnd_Adds_Close_Paren() {
            // Act
            _sut.GroupEnd();

            // Assert
            Assert.Equal(")", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_And_Adds_Scim_And()
        {
            // Act
            _sut.And();

            // Assert
            Assert.Equal(ScimConstants.AND, _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_Or_Adds_Scim_Or()
        {
            // Act
            _sut.Or();

            // Assert
            Assert.Equal(ScimConstants.OR, _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_Eq_String_Adds_Scim_Eq_Value()
        {
            // Act
            _sut.Eq("test");

            // Assert
            Assert.Equal($"{ScimConstants.EQUALS} test", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_Eq_Long_Adds_Scim_Eq_Value_ToString()
        {
            // Act
            _sut.Eq(5);

            // Assert
            Assert.Equal($"{ScimConstants.EQUALS} 5", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_Contains_Adds_Scim_Co_Value()
        {
            // Act
            _sut.Contains("test");

            // Assert
            Assert.Equal($"{ScimConstants.CONTAINS} test", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_StartsWith_Adds_Scim_Sw()
        {
            // Act
            _sut.StartsWith("test");

            // Assert
            Assert.Equal($"{ScimConstants.STARTS_WITH} test", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_IsPresent_Adds_Scim_Pr()
        {
            // Act
            _sut.Field(item => item.Title).IsPresent();

            // Assert
            Assert.Equal($"title {ScimConstants.PRESENT}", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_GreaterThan_Adds_Scim_Gt()
        {
            // Act
            _sut.GreaterThan(5);

            // Assert
            Assert.Equal($"{ScimConstants.GREATER_THAN} 5", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_GreaterThanOrEqual_Adds_Scim_Ge()
        {
            // Act
            _sut.GreaterThanOrEqual(5);

            // Assert
            Assert.Equal($"{ScimConstants.GREATER_THAN_OR_EQUAL} 5", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_LessThan_Adds_Scim_Lt()
        {
            // Act
            _sut.LessThan(5);

            // Assert
            Assert.Equal($"{ScimConstants.LESS_THAN} 5", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_LessThanOrEqual_Adds_Scim_Le()
        {
            // Act
            _sut.LessThanOrEqual(5);

            // Assert
            Assert.Equal($"{ScimConstants.LESS_THAN_OR_EQUAL} 5", _sut.ToString());
        }

        [Fact]
        public void FilterBuilder_ToString_Does_Not_Add_Space_Between_Parenthesis_And_Inner_Terms()
        {
            // Arrange
            var expectedFilter = "(title eq test or title eq test2) and (id eq test3 or id eq test4)";

            // Act
            _sut.Group()
                .Field(item => item.Title)
                .Eq("test")
                .Or()
                .Field(item => item.Title)
                .Eq("test2")
                .GroupEnd()
                .And()
                .Group()
                .Field(item => item.Id)
                .Eq("test3")
                .Or()
                .Field(item => item.Id)
                .Eq("test4")
                .GroupEnd();

            // Assert
            Assert.Equal(expectedFilter, _sut.ToString());
        }
    }
}
