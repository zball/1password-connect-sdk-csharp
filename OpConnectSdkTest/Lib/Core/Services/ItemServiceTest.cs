using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bogus;
using Moq;
using OpConnectSdk.Lib.Core.Services;
using OpConnectSdk.Model;
using Xunit;

namespace OpConnectSdkTest
{
    public class CreactAsyncTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Item() };
            yield return new object[] { new Item
                {
                    Vault = new Vault()
                } 
            };
            yield return new object[] { new Item
                {
                    Vault = new Vault
                        {
                            Id = ""
                        }
                } 
            };
            yield return new object[] { new Item
                {
                    Vault = new Vault
                        {
                            Id = " "
                        }
                } 
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ItemServiceTest : BaseServiceTest
    {
        
        #region Private Members
        
        private readonly ItemService _sut;
        
        #endregion Private Members

        public ItemServiceTest()
        {
            _sut = new ItemService(opClient: OpClient.Object);
        }

        [Fact]
        public async Task GetListAsync_When_No_Items_Exist_Returns_EmptyList()
        {
            // Arrange
            OpClient.Setup(
                  e => e.GetAsync<List<Item>>( It.IsAny<string>() )
            ).ReturnsAsync(new List<Item>());            

            // Act
            var response = await _sut.GetListAsync(new Faker().Random.String(10));

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public async Task GetListAsync_When_Items_Exist_Returns_Item()
        {
            // Arrange
            var faker = new Faker();
            var items = new List<Item> {
                new Item { Title = faker.Lorem.Words(3).ToString() },
                new Item { Title = faker.Lorem.Words(3).ToString() },
            };

            OpClient.Setup(
                  e => e.GetAsync<List<Item>>( It.IsAny<string>() )
            ).ReturnsAsync(items);            

            // Act
            var response = await _sut.GetListAsync(faker.Random.String(10));

            // Assert
            Assert.Equal(response.First().Title, items.First().Title);
            Assert.Equal(2, response.Count());
        }

        [Fact]
        public async Task GetListAsync_When_Filtered_AppendsFilterToQuery()
        {
            // Arrange
            var faker = new Faker();
            var filter = faker.Lorem.Word();
            var vaultId = "vaultid";
            var capturedValue = string.Empty;
            var captureMatch = new CaptureMatch<string>(s => capturedValue = s);

            OpClient.Setup(
                  e => e.GetAsync<List<Item>>( Capture.With<string>(captureMatch) )
            );            

            // Act
            var response = await _sut.GetListAsync(vaultId, filter);

            // Assert
            Assert.Equal($"{ItemService.BASE_URL.Replace("{vaultUUID}", vaultId)}?filter={filter}", capturedValue);
        }

        [Fact]
        public async Task GetAsync_When_No_Item_Exists_Returns_NotFoundException()
        {
            // Arrange
            var faker = new Faker();
            OpClient.Setup(
                  e => e.GetAsync<Item>( It.IsAny<string>() )
            ).Throws(new HttpRequestException(null, null, statusCode: HttpStatusCode.NotFound));  

            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>( 
                () => _sut.GetAsync(faker.Random.String(12), faker.Random.String(12)) 
            );
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task GetAsync_When_Item_Exists_Returns_Item()
        {
            // Arrange
            var faker = new Faker();
            var item = new Item { 
                Title = faker.Lorem.Words(3).ToString(),
                Id = "itemid"
            };
            var vaultId = "vaultid";
            var capturedValue = string.Empty;
            var captureMatch = new CaptureMatch<string>(s => capturedValue = s);

            OpClient.Setup(
                  e => e.GetAsync<Item>( Capture.With(captureMatch) )
            ).ReturnsAsync(item);  

            // Act
           var response = await _sut.GetAsync(vaultId, item.Id);

            // Assert
            Assert.Equal($"{ItemService.BASE_URL.Replace("{vaultUUID}", vaultId)}/{item.Id}", capturedValue);
            Assert.Equal(response.Title, item.Title);
        }

        [Theory]
        [ClassData(typeof(CreactAsyncTestData))]
        public async Task CreateAsync_When_VaultIncorrectlySet_ThrowsArgumentException(Item item)
        {
            // Arrange, Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>( 
                () => _sut.CreateAsync(item)
            );
            Assert.Equal($"ItemService.CreateAsync: {ItemService.ERROR_NO_VAULT_ID}", exception.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenNotSuccessful_ThrowsException()
        {
            // Arrange
            var faker = new Faker();
            var item = new Item { Vault = new Vault { Id = faker.Random.String(10) } };
            OpClient.Setup(
                  e => e.PostAsync<CreateItemDto, Item>( It.IsAny<string>(), It.IsAny<CreateItemDto>() )
            ).Throws(new HttpRequestException(null, null, statusCode: HttpStatusCode.BadRequest));  

            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>( 
                () => _sut.CreateAsync(item) 
            );
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        public async Task CreateAsync_WhenSuccessful_ReturnsItem()
        {
            // Arrange
            var faker = new Faker();
            var item = new Item { Vault = new Vault { Id = faker.Random.String(10) } };
            var capturedValue = string.Empty;
            var captureMatch = new CaptureMatch<string>(s => capturedValue = s);

            OpClient.Setup(
                  e => e.PostAsync<CreateItemDto, Item>( Capture.With(captureMatch), It.IsAny<CreateItemDto>() )
            ).ReturnsAsync(item);  

            // Act
           var response = await _sut.CreateAsync(item);

            // Assert
            Assert.Equal($"{ItemService.BASE_URL.Replace("{vaultUUID}", item.Vault.Id)}", capturedValue);
            Assert.Equal(response.Title, item.Title);
        }

        [Fact]
        public async Task DeleteAsync_When_No_Item_Exists_Returns_NotFoundException()
        {
            // Arrange
            var faker = new Faker();
            OpClient.Setup(
                  e => e.DeleteAsync( It.IsAny<string>() )
            ).Throws(new HttpRequestException(null, null, statusCode: HttpStatusCode.NotFound));  

            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>( 
                () => _sut.DeleteAsync(faker.Random.String(12), faker.Random.String(12)) 
            );
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_WhenSuccessful_ReturnsTrue()
        {
            // Arrange
            var faker = new Faker();
            var itemId = "itemid";
            var vaultId = "vaultid";
            var capturedValue = string.Empty;
            var captureMatch = new CaptureMatch<string>(s => capturedValue = s);

            OpClient.Setup(
                  e => e.DeleteAsync( Capture.With(captureMatch) )
            ).ReturnsAsync(true);  

            // Act
           var response = await _sut.DeleteAsync(vaultId: vaultId, itemId: itemId);

            // Assert
            Assert.Equal($"{ItemService.BASE_URL.Replace("{vaultUUID}", vaultId)}/{itemId}", capturedValue);
            Assert.True(response);
        }
    }


}