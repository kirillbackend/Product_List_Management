using ProductListManagement.Service;
using ProductListManagement.Service.Dtos;

namespace ProductListManagement.Test
{
    public class ValidationServiceTests
    {
        private readonly ValidationService _validationService;

        public ValidationServiceTests()
        {
            _validationService = new ValidationService();
        }

        [Fact]
        public async Task ValidateAdminLogin_CorrectedData_ReturnArgumentNullException()
        {
            //Arrange
            var userDto = new UserDto()
            {
                Login = "dwer",
                Password = "dde434"
            };

            var adminStrings = new AdminStrings()
            {
                Login = "dwer",
                Password = "dde434"
            };

            //Act
            var exception = await Record.ExceptionAsync(() => _validationService.ValidateAdminLogin(userDto.Login, adminStrings.Login));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task ValidateAdminPassvord_CorrectedData_ReturnArgumentNullException()
        {
            //Arrange
            var userDto = new UserDto()
            {
                Login = "dwer",
                Password = "dde434"
            };

            var adminStrings = new AdminStrings()
            {
                Login = "dwer",
                Password = "dde434"
            };

            //Act
            var exception = await Record.ExceptionAsync(() => _validationService.ValidateAdminPassvord(userDto.Password, adminStrings.Password));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task ValidateAdminPassvord_WrongPassword_ReturnsArgumentException()
        {
            //Arrange
            var userDto = new UserDto()
            {
                Password = "IIVi"
            };

            var adminStrings = new AdminStrings() 
            {
                Password = "dde434"
            };

            //Act
            var exception = await Record.ExceptionAsync(() => _validationService.ValidateAdminPassvord(userDto.Password, adminStrings.Password));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task ValidateAdminPassvord_WrongPassword_ReturnsArgumentExceptionWrongPassword()
        {
            //Arrange
            var expected = "Wrong password";
            var userDto = new UserDto()
            {
                Password = "IIVi"
            };

            var adminStrings = new AdminStrings()
            {
                Password = "dde434"
            };

            //Act
            var exception = await Record.ExceptionAsync(() => _validationService.ValidateAdminPassvord(userDto.Password, adminStrings.Password));

            //Assert
            var argumentNullEx = Assert.IsType<ArgumentException>(exception);
            Assert.Equal(expected, argumentNullEx.Message);
        }

        [Fact]
        public async Task ValidateAdminLogin_WrongLogin_ReturnsArgumentException()
        {
            //Arrange
            var userDto = new UserDto()
            {
                Login = "IIVi"
            };

            var adminStrings = new AdminStrings()
            {
                Login = "dde434"
            };

            //Act
            var exception = await Record.ExceptionAsync(() => _validationService.ValidateAdminLogin(userDto.Login, adminStrings.Login));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task ValidateAdminLogin_WrongLogin_ReturnsArgumentExceptionWrongLogin()
        {
            //Arrange
            var expected = "Wrong login";
            var userDto = new UserDto()
            {
                Login = "IIVi"
            };

            var adminStrings = new AdminStrings()
            {
                Login = "dde434"
            };

            //Act
            var exception = await Record.ExceptionAsync(() => _validationService.ValidateAdminLogin(userDto.Login, adminStrings.Login));

            //Assert
            var argumentNullEx = Assert.IsType<ArgumentException>(exception);
            Assert.Equal(expected, argumentNullEx.Message);
        }
    }
}
