using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Services.DTOs;
using Services.Interfaces;
using Services.Interfaces.Implementations;

namespace UnitTests
{
    public class TestsBase
    {
        protected LendingService LendingService;
        protected List<LendingDto> Lendings = new List<LendingDto>();
        protected Mock<ILendingEntityService> MockLendingEntityService;
        protected Mock<IBookEntityService> MockBookEntityService;
        protected Mock<IPersonEntityService> MockPersonEntityService;

        [SetUp]
        public void SetUp()
        {
            MockLendingEntityService = new Mock<ILendingEntityService>();
            MockLendingEntityService.Setup(s => s.GetAll()).Returns(() => Lendings.ToArray());
            MockLendingEntityService.Setup(s => s.Add(It.IsAny<LendingDto>()))
                .Callback<LendingDto>(l => Lendings.Add(l))
                .Returns((LendingDto parameter) => parameter);
            MockLendingEntityService.Setup(s => s.Update(It.IsAny<LendingDto>()))
                .Callback<LendingDto>(parameter =>
                {
                    Lendings.RemoveAll(l => l.Id == parameter.Id);
                    Lendings.Add(parameter);
                })
                .Returns<LendingDto>(parameter => Lendings.First(l => l.Id == parameter.Id));

            MockBookEntityService = new Mock<IBookEntityService>();
            MockBookEntityService.Setup(s => s.GetOne(It.IsInRange(1, 5, Range.Inclusive)))
                .Returns((int parameter) => new BookDto { Id = parameter });
            MockBookEntityService.Setup(s => s.GetOne(It.IsInRange(6, 10, Range.Inclusive)))
                .Returns<BookDto>(null);

            MockPersonEntityService = new Mock<IPersonEntityService>();
            MockPersonEntityService.Setup(s => s.GetOne(It.IsInRange(1, 5, Range.Inclusive)))
                .Returns((int parameter) => new PersonDto { Id = parameter });
            MockPersonEntityService.Setup(s => s.GetOne(It.IsInRange(6, 10, Range.Inclusive)))
                .Returns<PersonDto>(null);

            LendingService = new LendingService(MockLendingEntityService.Object, MockBookEntityService.Object,
                MockPersonEntityService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            LendingService = null;
            MockLendingEntityService = null;
            MockBookEntityService = null;
            MockPersonEntityService = null;
            Lendings = new List<LendingDto>();
        }
    }
}