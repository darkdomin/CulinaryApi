﻿using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.DTO.Difficulties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Difficulties
{
    public class DifficultyService : IDifficultyService
    {
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IMapper _mapper;
        public DifficultyService(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            _difficultyRepository = difficultyRepository;
            _mapper = mapper;
        }

        public async Task<DifficultyDto> GetAsync(int id)
        {
            var difficulty = await _difficultyRepository.GetAsync(id);
            var result = _mapper.Map<DifficultyDto>(difficulty);
            return result;
        }

        public async Task<DifficultyDto> GetAsync(string name)
        {
            var difficulty = await _difficultyRepository.GetAsync(name);
            var result = _mapper.Map<DifficultyDto>(difficulty);
            return result;
        }
        public async Task<IEnumerable<DifficultyDto>> BrowseAsync(string name = null)
        {
            var difficulties = await _difficultyRepository.GetAllAsync(name);
            var result = _mapper.Map<IEnumerable<DifficultyDto>>(difficulties);
            return result;
        }

        public async Task<int> CreateAsync(CreateDifficultyDto dto)
        {
            var newDifficulty = _mapper.Map<Difficulty>(dto);
            await _difficultyRepository.AddAsync(newDifficulty);
            return newDifficulty.Id;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var difficulty = await _difficultyRepository.GetAsync(id);
            await _difficultyRepository.DeleteAsync(difficulty);
            await Task.CompletedTask;
        }
    }
}
