using Moq;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdd.Specs.MockResources
{
    public class ProducerMock
    {
        public static readonly Mock<IProducerRepository> ProducerRepoMock = new Mock<IProducerRepository>();

        private static readonly IEnumerable<Producer> ListOfProducers = new List<Producer>
        {
            new Producer(1,"jerry Christopher","jerryBio",new DateTime(2001, 12, 2),"male"),
            new Producer(2,"Priyanka","PriyankaBio",new DateTime(1998, 06, 5),"female")
        };

        public static void MockGetAllProducersAsync()
        {
            ProducerRepoMock.Setup(x => x.GetAllProducersAsync()).ReturnsAsync(ListOfProducers);
        }

        public static void MockGetProducerByIdAsync()
        {
            ProducerRepoMock.Setup(x => x.GetProducerByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => { return ListOfProducers.FirstOrDefault(Producer => Producer.Id == id); });
        }

        public static void MockAddProducerAsync()
        {
            ProducerRepoMock.Setup(x => x.AddProducerAsync(It.IsAny<RequestProducer>())).ReturnsAsync((RequestProducer Producer) =>
            {
                return ListOfProducers.Count() - 1 ;
            });
        }

        public static void MockDeleteProducerAsync()
        {
            ProducerRepoMock.Setup(x => x.DeleteProducerAsync(It.IsAny<int>())).Returns((int id) =>
            {
                return Task.CompletedTask;
            });
        }

        public static void MockUpdateProducerAsync()
        {
            ProducerRepoMock.Setup(x => x.UpdateProducerAsync(It.IsAny<int>(), It.IsAny<RequestProducer>())).Returns((int id, RequestProducer Producer) =>
            {
                return Task.CompletedTask;
            });
        }
    }
}
