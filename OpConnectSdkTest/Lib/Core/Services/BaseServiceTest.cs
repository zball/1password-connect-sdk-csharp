using Xunit;
using System.Threading.Tasks;
using OpConnectSdk.Lib.Core.Services;
using Moq;
using System.Net.Http;
using OpConnectSdk.Lib.Core;
using OpConnectSdk.Model;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using System.Net;

namespace OpConnectSdkTest
{
    public class BaseServiceTest
    {
        protected Mock<OpClient> OpClient;

        public BaseServiceTest()
        {
            OpClient = new Mock<OpClient>(
                new HttpClient(),
                new OpConnectOptions { 
                    BaseUrl = new Faker().Internet.Url(), 
                    Token = new Faker().Random.String(12) 
                }
            );
        }
    }
}
