using AutoMapper;
using SimpleStore.Application.Mapper;
using SimpleStore.Application.Models;
using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStore.Application.Services
{
    public interface ICharacteristicService
    {
        List<CharacteristicModel> GetCharacteristicList();
        List<CharacteristicModel> GetCharacteristicListByName(string name);
        CharacteristicModel GetCharacteristicById(int characteristicId);
        CharacteristicModel AddCharacteristic(CharacteristicModel characteristicModel);
        void AssignCharacteristic(int characteristicId, int itemId);
        CharacteristicModel AssignCharacteristic(CharacteristicModel characteristicModel, int itemId);
        void UnassignCharacteristic(int characteristicId, int itemId);
    }

    public class CharacteristicService : ICharacteristicService
    {
        private readonly ICharacteristicRepository characteristicRepository;
        private readonly IItemCharacteristicRepository itemCharacteristicRepository;

        public CharacteristicService(
            ICharacteristicRepository characteristicRepository,
            IItemCharacteristicRepository itemCharacteristicRepository)
        {
            this.characteristicRepository = characteristicRepository;
            this.itemCharacteristicRepository = itemCharacteristicRepository;
        }

        private readonly IMapper mapper = ObjectMapper.Mapper;

        public CharacteristicModel AddCharacteristic(CharacteristicModel characteristicModel)
        {
            var characteristic = mapper.Map<Characteristic>(characteristicModel);
            characteristic = characteristicRepository.Add(characteristic);
            return mapper.Map<CharacteristicModel>(characteristic);
        }

        public List<CharacteristicModel> GetCharacteristicList()
        {
            var characteristicList = characteristicRepository.Entities.ToList();
            return mapper.Map<List<CharacteristicModel>>(characteristicList);
        }

        public CharacteristicModel GetCharacteristicById(int characteristicId)
        {
            var characteristic = characteristicRepository.Entities
                .SingleOrDefault(e => e.CharacteristicId == characteristicId);
            return mapper.Map<CharacteristicModel>(characteristic);
        }

        public List<CharacteristicModel> GetCharacteristicListByName(string name)
        {
            var characteristicList = characteristicRepository.Entities
                .Where(e => e.Name == name).ToList();
            return mapper.Map<List<CharacteristicModel>>(characteristicList);
        }

        public void AssignCharacteristic(int characteristicId, int itemId)
        {
            itemCharacteristicRepository.Add(itemId, characteristicId);
        }

        public CharacteristicModel AssignCharacteristic(CharacteristicModel characteristicModel, int itemId)
        {
            var characteristic = characteristicRepository.Entities
                .SingleOrDefault(e => e.CharacteristicId == characteristicModel.CharacteristicId);

            if (characteristic is null)
            {
                characteristic = mapper.Map<Characteristic>(characteristicModel);
                characteristic = characteristicRepository.Add(characteristic);
            }

            AssignCharacteristic(characteristic.CharacteristicId, itemId);

            return mapper.Map<CharacteristicModel>(characteristic);
        }

        public void UnassignCharacteristic(int characteristicId, int itemId)
        {
            itemCharacteristicRepository.Delete(itemId, characteristicId);
        }
    }
}
