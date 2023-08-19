using Microsoft.Extensions.Options;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using MovieApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _ProducerRepository;
        readonly Validations v;
        public ProducerService(IProducerRepository producerRepository)
        {
            _ProducerRepository = producerRepository;
            v = new Validations();
        }
        
        public async Task<IEnumerable<ResponseProducer>> GetAllProducersAsync()
        {

            var ProducersList =await _ProducerRepository.GetAllProducersAsync();
            var result = ProducersList.Select(o => new ResponseProducer(o.Id, o.Name, o.Bio, o.DOB, o.Gender));
            return result;
        }

        public async Task<ResponseProducer> GetProducerByIdAsync(int id)
        {

            var producer =await  _ProducerRepository.GetProducerByIdAsync(id);
            if(producer==null)
            {
                return null;
            }
            return new ResponseProducer(id, producer.Name, producer.Bio, producer.DOB, producer.Gender);
        }
        public async Task<int> AddProducerAsync(RequestProducer reqProducer)
        { 
            return await _ProducerRepository.AddProducerAsync(reqProducer);
        }

        public async Task DeleteProducerAsync(int id)
        { 
            await _ProducerRepository.DeleteProducerAsync(id);
        }

      

        public async Task UpdateProducerAsync(int id, RequestProducer reqProducer)
        {
            v.InputValidate(reqProducer.Name);
            v.InputValidate(reqProducer.Bio);
            v.InputValidate(reqProducer.Gender);
           
            await _ProducerRepository.UpdateProducerAsync(id, reqProducer);
        }
    }
}
