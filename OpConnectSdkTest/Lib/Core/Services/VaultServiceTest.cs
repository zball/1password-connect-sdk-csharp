using Xunit;
using System.Threading.Tasks;
using OpConnectSdk.Lib.Core.Services;
using Moq;
using System.Net.Http;
using OpConnectSdk.Model;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using System.Net;

namespace OpConnectSdkTest
{
    public class VaultServiceTest : BaseServiceTest
    {

        #region Private Members
        
        private readonly VaultService _sut;
        
        #endregion Private Members

        public VaultServiceTest()
        {
            _sut = new VaultService(opClient: OpClient.Object);
        }

        [Fact]
        public async Task GetListAsync_When_No_Vaults_Exist_Returns_EmptyList()
        {
            // Arrange
            OpClient.Setup(
                  e => e.GetAsync<List<Vault>>( It.IsAny<string>() )
            ).ReturnsAsync(new List<Vault>());            

            // Act
            var response = await _sut.GetListAsync();

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public async Task GetListAsync_When_Vaults_Exist_Returns_Vaults()
        {
            // Arrange
            var faker = new Faker();
            var vaults = new List<Vault> {
                new Vault { Name = faker.Lorem.Words(3).ToString() },
                new Vault { Name = faker.Lorem.Words(3).ToString() },
            };

            OpClient.Setup(
                  e => e.GetAsync<List<Vault>>( It.IsAny<string>() )
            ).ReturnsAsync(vaults);            

            // Act
            var response = await _sut.GetListAsync();

            // Assert
            Assert.Equal(response.First().Name, vaults.First().Name);
            Assert.Equal(2, response.Count());
        }

        [Fact]
        public async Task GetListAsync_When_Filtered_AppendsFilterToQuery()
        {
            // Arrange
            var faker = new Faker();
            var filter = faker.Lorem.Word();
            var capturedValue = string.Empty;
            var captureMatch = new CaptureMatch<string>(s => capturedValue = s);

            OpClient.Setup(
                  e => e.GetAsync<List<Vault>>( Capture.With<string>(captureMatch) )
            );            

            // Act
            var response = await _sut.GetListAsync(filter);

            // Assert
            Assert.Equal($"{VaultService.BASE_URL}?filter={filter}", capturedValue);
        }

        [Fact]
        public async Task GetAsync_When_No_Vault_Exists_Returns_NotFoundException()
        {
            // Arrange
            OpClient.Setup(
                  e => e.GetAsync<Vault>( It.IsAny<string>() )
            ).Throws(new HttpRequestException(null, null, statusCode: HttpStatusCode.NotFound));  

            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>( 
                () => _sut.GetAsync(new Faker().Random.String(12)) 
            );
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task GetAsync_When_Vault_Exists_Returns_Vault()
        {
            // Arrange
            var faker = new Faker();
            var vault = new Vault { 
                Name = faker.Lorem.Words(3).ToString(),
                Id = faker.Random.String(12)
            };
            var capturedValue = string.Empty;
            var captureMatch = new CaptureMatch<string>(s => capturedValue = s);

            OpClient.Setup(
                  e => e.GetAsync<Vault>( Capture.With(captureMatch) )
            ).ReturnsAsync(vault);  

            // Act
           var response = await _sut.GetAsync(vault.Id);

            // Assert
            Assert.Equal($"{VaultService.BASE_URL}/{vault.Id}", capturedValue);
            Assert.Equal(response.Name, vault.Name);
        }
    }
}
