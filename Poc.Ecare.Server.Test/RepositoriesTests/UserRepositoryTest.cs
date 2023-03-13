using Xunit;
using Moq;
using Poc.Ecare.Domain.GenericRepository.Interface;
using Poc.Ecare.Infrastructure.Models.Classes;
using Poc.Ecare.Infrastructure.Models.DatabaseContext;
using Poc.Ecare.Infrastructure.Models.Seeds;
using Pro.Ecare.Business.Services.Classes;
using Pro.Ecare.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork.Interface;
using Pro.Ecare.Business.Cqrs.Commands.Interfaces;
using Pro.Ecare.Business.Cqrs.Queries.Interfaces;
using System.Threading;

namespace Poc.Ecare.Server.Test.RepositoriesTests
{
    public class UserRepositoryTest
    {
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        protected readonly Mock<IGenericGetAllQuery<User>> _mockGenericGetAllQuery;
        protected readonly Mock<IGenericGetByIdQuery<User>> _mockGenericGetByIdQuery;
        protected readonly Mock<IGenericCreateCommand<User>> _mockGenericCreateCommand;
        protected readonly Mock<IGenericUpdateCommand<User>> _mockGenericUpdateCommand;
        protected readonly Mock<IGenericDeleteQuery<User>> _mockGenericDeleteQuery;
        protected readonly IGenericService<User>? _genericService;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        protected readonly Mock<IGenericRepository<User>> _mockGenericRepository;
        protected readonly Mock<IUnitOfWork<DatabaseContext>> _mockUnitOfWork;
        private IList<User> _Users;

        public UserRepositoryTest()
        {
            _mockGenericRepository = new Mock<IGenericRepository<User>>();
            _mockUnitOfWork = new Mock<IUnitOfWork<DatabaseContext>>();
            _mockGenericGetAllQuery = new Mock<IGenericGetAllQuery<User>>();
            _mockGenericGetByIdQuery = new Mock<IGenericGetByIdQuery<User>>();
            _mockGenericCreateCommand = new Mock<IGenericCreateCommand<User>>();
            _mockGenericUpdateCommand = new Mock<IGenericUpdateCommand<User>>();
            _mockGenericDeleteQuery = new Mock<IGenericDeleteQuery<User>>();
            _genericService = new GenericService<User>(_mockUnitOfWork.Object, _mockGenericGetAllQuery.Object, _mockGenericGetByIdQuery.Object, _mockGenericCreateCommand.Object, _mockGenericUpdateCommand.Object, _mockGenericDeleteQuery.Object);
            _Users = UserSeed.GetUsersMockUp();

        }

        [Fact]
        public async Task GetAllTEntitys_ShouldReturnListOfUsers_WhenDatabaseConnectionIsSet()
        {
            // Arrange 
            _mockGenericRepository.Setup(pl => pl.GetAll()).ReturnsAsync(_Users);
            _mockGenericGetAllQuery.Setup(s => s.Handle(CancellationToken.None)).ReturnsAsync(_Users);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var Users = await _genericService!.GetAllTEntitys()!;

            // Assert
            Assert.NotNull(Users);
            Assert.Equal(_Users, Users);

        }

        [Fact]
        public async Task GetAllTEntitys_ShouldReturnNull_WhenDatabaseConnectionIsSet()
        {
            // Arrange 
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            IList<User>? listMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.GetAll()).ReturnsAsync(listMock);
            _mockGenericGetAllQuery.Setup(s => s.Handle(CancellationToken.None)).ReturnsAsync(listMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var list = await _genericService!.GetAllTEntitys()!;

            // Assert
            Assert.Null(list);

        }

        [Fact]
        public async Task GetTEntityById_ShouldReturnUser_WhenIdIsServed()
        {
            // Arrange 
            var UserId = 1;
            var UserMock = new User()
            {
                UserId = 1,
                FirstName = "FirstName1",
                LastName = "LastName 1",
                Email = "user1@mail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                IsConnected = 0
            };
            _mockGenericRepository.Setup(pl => pl.GetById(UserId)).ReturnsAsync(UserMock);
            _mockGenericGetByIdQuery.Setup(s => s.Handle(UserId, CancellationToken.None)).ReturnsAsync(UserMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var User = await _genericService!.GetTEntityById(UserId)!;

            // Assert
            Assert.Equal(UserMock, User);

        }

        [Fact]
        public async Task GetTEntityById_ShouldReturnNull_WhenIdIsServed()
        {
            // Arrange 
            var UserId = 1;
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            User? UserMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.GetById(UserId)).ReturnsAsync(UserMock);
            _mockGenericGetByIdQuery.Setup(s => s.Handle(UserId, CancellationToken.None)).ReturnsAsync(UserMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var User = await _genericService!.GetTEntityById(UserId)!;

            // Assert
            Assert.Null(User);

        }

        [Fact]
        public async Task GetTEntityById_ShouldReturnNull_WhenIdIsNotServed()
        {
            // Arrange 
            int? UserId = null;
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            User? UserMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.GetById(UserId)).ReturnsAsync(UserMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var User = await _genericService!.GetTEntityById(UserId)!;

            // Assert
            Assert.Null(User);

        }

        [Fact]
        public async Task InsertTEntity_ShouldAddAndReturnUser_WhenEntityIsNotNull()
        {
            // Arrange
            var UserMock = new User()
            {
                UserId = 5,
                FirstName = "FirstName5",
                LastName = "LastName 5",
                Email = "user5@mail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                IsConnected = 0
            };
            _mockGenericRepository.Setup(pl => pl.Add(UserMock)).ReturnsAsync(UserMock);
            _mockGenericCreateCommand.Setup(s => s.Handle(UserMock, CancellationToken.None)).ReturnsAsync(UserMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var User = await _genericService!.InsertTEntity(UserMock)!;

            // Assert
            Assert.Equal(UserMock, User);
        }

        [Fact]
        public async Task InsertTEntity_ShouldAddAndReturnNull_WhenEntityIsNull()
        {
            // Arrange
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            User? UserMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.Add(UserMock)).ReturnsAsync(UserMock);
            _mockGenericCreateCommand.Setup(s => s.Handle(UserMock, CancellationToken.None)).ReturnsAsync(UserMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var User = await _genericService!.InsertTEntity(UserMock!)!;

            // Assert
            Assert.Null(User);
        }

        [Fact]
        public void UpdateTEntity_ShouldUpdateAndReturnUser_WhenUserIsNotNull()
        {
            // Arrange
            var UserMock = new User()
            {
                UserId = 1,
                FirstName = "FirstName11",
                LastName = "LastName 11",
                Email = "user11@mail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                IsConnected = 0
            };
            _mockGenericRepository.Setup(pl => pl.Update(UserMock)).Returns(UserMock);
            _mockGenericUpdateCommand.Setup(s => s.Handle(UserMock, CancellationToken.None)).Returns(UserMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var User = _genericService!.UpdateTEntity(UserMock)!;

            // Assert
            Assert.Equal(UserMock, User);
        }

        [Fact]
        public void UpdateTEntity_ShouldReturnNull_WhenUserIsNull()
        {
            // Arrange
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            User? UserMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.Update(UserMock!)).Returns(UserMock);
            _mockGenericUpdateCommand.Setup(s => s.Handle(UserMock, CancellationToken.None)).Returns(UserMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var User = _genericService!.UpdateTEntity(UserMock)!;

            // Assert
            Assert.Null(User);
        }

        [Fact]
        public async Task DeleteTEntity_ShouldDelete_WhenIdIsNotNull()
        {
            // Arrange
            var id = 1;
            var UserMock = new User();
            _mockGenericRepository.Setup(pl => pl.Delete(id)).ReturnsAsync(UserMock);
            _mockGenericDeleteQuery.Setup(s => s.Handle(id, CancellationToken.None)).ReturnsAsync(UserMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<User>()).Returns(_mockGenericRepository.Object);

            // Act
            var User = await _genericService!.DeleteTEntity(id!)!;

            // Assert
            Assert.Equal(UserMock, User);
        }
    }
}
