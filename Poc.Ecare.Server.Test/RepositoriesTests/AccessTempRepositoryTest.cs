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
    public class AccessTempRepositoryTest
    {
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        protected readonly Mock<IGenericGetAllQuery<AccessTemp>> _mockGenericGetAllQuery;
        protected readonly Mock<IGenericGetByIdQuery<AccessTemp>> _mockGenericGetByIdQuery;
        protected readonly Mock<IGenericCreateCommand<AccessTemp>> _mockGenericCreateCommand;
        protected readonly Mock<IGenericUpdateCommand<AccessTemp>> _mockGenericUpdateCommand;
        protected readonly Mock<IGenericDeleteQuery<AccessTemp>> _mockGenericDeleteQuery;
        protected readonly IGenericService<AccessTemp>? _genericService;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        protected readonly Mock<IGenericRepository<AccessTemp>> _mockGenericRepository;
        protected readonly Mock<IUnitOfWork<DatabaseContext>> _mockUnitOfWork;
        private IList<AccessTemp> _accessTemps;

        public AccessTempRepositoryTest()
        {
            _mockGenericRepository = new Mock<IGenericRepository<AccessTemp>>();
            _mockUnitOfWork = new Mock<IUnitOfWork<DatabaseContext>>();
            _mockGenericGetAllQuery = new Mock<IGenericGetAllQuery<AccessTemp>>();
            _mockGenericGetByIdQuery = new Mock<IGenericGetByIdQuery<AccessTemp>>();
            _mockGenericCreateCommand = new Mock<IGenericCreateCommand<AccessTemp>>();
            _mockGenericUpdateCommand = new Mock<IGenericUpdateCommand<AccessTemp>>();
            _mockGenericDeleteQuery = new Mock<IGenericDeleteQuery<AccessTemp>>();
            _genericService = new GenericService<AccessTemp>(_mockUnitOfWork.Object, _mockGenericGetAllQuery.Object, _mockGenericGetByIdQuery.Object, _mockGenericCreateCommand.Object, _mockGenericUpdateCommand.Object, _mockGenericDeleteQuery.Object);
            _accessTemps = AccessTempSeed.GetAccessTempsMockUp();

        }

        [Fact]
        public async Task GetAllTEntitys_ShouldReturnListOfAccessTemps_WhenDatabaseConnectionIsSet()
        {
            // Arrange 
            _mockGenericRepository.Setup(pl => pl.GetAll()).ReturnsAsync(_accessTemps);
            _mockGenericGetAllQuery.Setup(s => s.Handle(CancellationToken.None)).ReturnsAsync(_accessTemps);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemps = await _genericService!.GetAllTEntitys()!;

            // Assert
            Assert.NotNull(AccessTemps);
            Assert.Equal(_accessTemps, AccessTemps);

        }

        [Fact]
        public async Task GetAllTEntitys_ShouldReturnNull_WhenDatabaseConnectionIsSet()
        {
            // Arrange 
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            IList<AccessTemp>? listMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.GetAll()).ReturnsAsync(listMock);
            _mockGenericGetAllQuery.Setup(s => s.Handle(CancellationToken.None)).ReturnsAsync(listMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var list = await _genericService!.GetAllTEntitys()!;

            // Assert
            Assert.Null(list);

        }

        [Fact]
        public async Task GetTEntityById_ShouldReturnAccessTemp_WhenIdIsServed()
        {
            // Arrange 
            var AccessTempId = 1;
            var AccessTempMock = new AccessTemp()
            {
                IdAccessTemp = 1,
                CodeClientBscs = "CODE001",
                NumLigneBscs = 1,
                NumGsm = "0661123456"
            };
            _mockGenericRepository.Setup(pl => pl.GetById(AccessTempId)).ReturnsAsync(AccessTempMock);
            _mockGenericGetByIdQuery.Setup(s => s.Handle(AccessTempId, CancellationToken.None)).ReturnsAsync(AccessTempMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemp = await _genericService!.GetTEntityById(AccessTempId)!;

            // Assert
            Assert.Equal(AccessTempMock, AccessTemp);

        }

        [Fact]
        public async Task GetTEntityById_ShouldReturnNull_WhenIdIsServed()
        {
            // Arrange 
            var AccessTempId = 1;
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            AccessTemp? AccessTempMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.GetById(AccessTempId)).ReturnsAsync(AccessTempMock);
            _mockGenericGetByIdQuery.Setup(s => s.Handle(AccessTempId, CancellationToken.None)).ReturnsAsync(AccessTempMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemp = await _genericService!.GetTEntityById(AccessTempId)!;

            // Assert
            Assert.Null(AccessTemp);

        }

        [Fact]
        public async Task GetTEntityById_ShouldReturnNull_WhenIdIsNotServed()
        {
            // Arrange 
            int? AccessTempId = null;
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            AccessTemp? AccessTempMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.GetById(AccessTempId)).ReturnsAsync(AccessTempMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemp = await _genericService!.GetTEntityById(AccessTempId)!;

            // Assert
            Assert.Null(AccessTemp);

        }

        [Fact]
        public async Task InsertTEntity_ShouldAddAndReturnAccessTemp_WhenEntityIsNotNull()
        {
            // Arrange
            var AccessTempMock = new AccessTemp()
            {
                IdAccessTemp = 7,
                CodeClientBscs = "CODE007",
                NumLigneBscs = 7,
                NumGsm = "0661123456"
            };
            _mockGenericRepository.Setup(pl => pl.Add(AccessTempMock)).ReturnsAsync(AccessTempMock);
            _mockGenericCreateCommand.Setup(s => s.Handle(AccessTempMock, CancellationToken.None)).ReturnsAsync(AccessTempMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemp = await _genericService!.InsertTEntity(AccessTempMock)!;

            // Assert
            Assert.Equal(AccessTempMock, AccessTemp);
        }

        [Fact]
        public async Task InsertTEntity_ShouldAddAndReturnNull_WhenEntityIsNull()
        {
            // Arrange
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            AccessTemp? AccessTempMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.Add(AccessTempMock)).ReturnsAsync(AccessTempMock);
            _mockGenericCreateCommand.Setup(s => s.Handle(AccessTempMock, CancellationToken.None)).ReturnsAsync(AccessTempMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemp = await _genericService!.InsertTEntity(AccessTempMock!)!;

            // Assert
            Assert.Null(AccessTemp);
        }

        [Fact]
        public void UpdateTEntity_ShouldUpdateAndReturnAccessTemp_WhenAccessTempIsNotNull()
        {
            // Arrange
            var AccessTempMock = new AccessTemp()
            {
                IdAccessTemp = 6,
                CodeClientBscs = "CODE0066",
                NumLigneBscs = 66,
                NumGsm = "0661123456"
            };
            _mockGenericRepository.Setup(pl => pl.Update(AccessTempMock)).Returns(AccessTempMock);
            _mockGenericUpdateCommand.Setup(s => s.Handle(AccessTempMock, CancellationToken.None)).Returns(AccessTempMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemp = _genericService!.UpdateTEntity(AccessTempMock)!;

            // Assert
            Assert.Equal(AccessTempMock, AccessTemp);
        }

        [Fact]
        public void UpdateTEntity_ShouldReturnNull_WhenAccessTempIsNull()
        {
            // Arrange
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            AccessTemp? AccessTempMock = null;
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
            _mockGenericRepository.Setup(pl => pl.Update(AccessTempMock!)).Returns(AccessTempMock);
            _mockGenericUpdateCommand.Setup(s => s.Handle(AccessTempMock, CancellationToken.None)).Returns(AccessTempMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemp = _genericService!.UpdateTEntity(AccessTempMock)!;

            // Assert
            Assert.Null(AccessTemp);
        }

        [Fact]
        public async Task DeleteTEntity_ShouldDelete_WhenIdIsNotNull()
        {
            // Arrange
            var id = 1;
            var AccessTempMock = new AccessTemp();
            _mockGenericRepository.Setup(pl => pl.Delete(id)).ReturnsAsync(AccessTempMock);
            _mockGenericDeleteQuery.Setup(s => s.Handle(id, CancellationToken.None)).ReturnsAsync(AccessTempMock);
            _mockUnitOfWork.Setup(u => u.GetRepository<AccessTemp>()).Returns(_mockGenericRepository.Object);

            // Act
            var AccessTemp = await _genericService!.DeleteTEntity(id!)!;

            // Assert
            Assert.Equal(AccessTempMock, AccessTemp);
        }
    }
}
