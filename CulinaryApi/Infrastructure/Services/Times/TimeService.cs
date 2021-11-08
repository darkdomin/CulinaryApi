using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.DTO.Meals;
using CulinaryApi.Infrastructure.DTO.Times;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Times
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IMapper _mapper;
        public TimeService(ITimeRepository timeRepository, IMapper mapper)
        {
            _timeRepository = timeRepository;
            _mapper = mapper;
        }

        public async Task<TimeDto> GetAsync(int id)
        {
            var time = await _timeRepository.GetAsync(id);
            var result = _mapper.Map<TimeDto>(time);
            return result;
        }

        public async Task<TimeDto> GetAsync(string name)
        {
            var time = await _timeRepository.GetAsync(name);
            var result = _mapper.Map<TimeDto>(time);
            return result;
        }
        public async Task<IEnumerable<TimeDto>> BrowseAsync(string name = null)
        {
            var times = await _timeRepository.GetAllAsync(name);
            var result = _mapper.Map<IEnumerable<TimeDto>>(times);
            return result;
        }

        public async Task<int> CreateAsync(CreateTimeDto dto)
        {
            var newTime = _mapper.Map<Time>(dto);
            await _timeRepository.AddAsync(newTime);
            return newTime.Id;
        }

        public async Task UpdateAsync(UpdateTimeDto dto, int id)
        {
            var time = await _timeRepository.GetAsync(id);
            time.SetName(dto.Name);
            await _timeRepository.UpdateAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var time = await _timeRepository.GetAsync(id);
            await _timeRepository.DeleteAsync(time);
            await Task.CompletedTask;
        }
    }
}
