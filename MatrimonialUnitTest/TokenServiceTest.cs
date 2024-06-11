using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonialUnitTest
{
    public class TokenServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateTokenPassTest()
        {
            //Arrange
            var jwtSection = new Mock<IConfigurationSection>();
            jwtSection.Setup(x => x["Issuer"]).Returns("*");
            jwtSection.Setup(x => x["Audience"]).Returns("*");
            jwtSection.Setup(x => x["Key"]).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");

            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("Jwt")).Returns(jwtSection.Object);

            ITokenService tokenService = new TokenService(configuration.Object);

            // Act
            var token = tokenService.GenerateToken(new User { UserId = 103 });

            // Assert
            Assert.IsNotNull(token);
        }
    }
}
