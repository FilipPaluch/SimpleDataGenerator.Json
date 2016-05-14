using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core;
using SimpleDataGenerator.Core.Mapping.Implementations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Json.Extensions;
using SimpleDataGenerator.Json.Logic;

namespace SimpleDataGenerator.Json
{
    public class SimpleJsonDataGenerator
    {
        private readonly List<EntityConfiguration> _entityConfigurations;
        private readonly List<ISpecimenBuilder> _specimens; 
        private bool _saveToFile;
        private string _filePath;
        public SimpleJsonDataGenerator()
        {
            _entityConfigurations = new List<EntityConfiguration>();
            _specimens = new List<ISpecimenBuilder>();
            _saveToFile = false;
        }

        public SimpleJsonDataGenerator WithConfiguration(EntityConfiguration configuration)
        {
            _entityConfigurations.Add(configuration);
            return this;
        }

        public SimpleJsonDataGenerator WithConfiguration(ISpecimenBuilder configuration)
        {
            _specimens.Add(configuration);
            return this;
        }

        public SimpleJsonDataGenerator WithConfiguration(IEnumerable<EntityConfiguration> configuration)
        {
            _entityConfigurations.AddRange(configuration);
            return this;
        }

        public SimpleJsonDataGenerator WithSavingToFile(string filePath)
        {
            _saveToFile = true;
            _filePath = filePath;
            return this;
        }

        public string Generate<TEntity>(int numberOfElements)
        {
            var autoDataGenerator = CreateAutoDataGenerator();
            var entities = new List<TEntity>();
            for (var i = 0; i < numberOfElements; i++)
            {
                entities.Add(autoDataGenerator.Fixture.Create<TEntity>());
            }
            var result = entities.DumpAsJson();
            if (_saveToFile) SaveToFile(result);
            return result;
        }

        public string Generate<TEntity>()
        {
            var autoDataGenerator = CreateAutoDataGenerator();
            var json = autoDataGenerator.Fixture.Create<TEntity>().DumpAsJson();
            if (_saveToFile) SaveToFile(json);
            return json;
        }

        private SimpleAutoDataGenerator CreateAutoDataGenerator()
        {
            var autoDataGenerator = new SimpleAutoDataGenerator();
            autoDataGenerator.WithConfiguration(_entityConfigurations);
            foreach (var specimenBuilder in _specimens)
            {
                autoDataGenerator.Fixture.Customizations.Add(specimenBuilder);
            } 
            return autoDataGenerator;
        }

        private void SaveToFile(string json)
        {
            var fileWriter = new FileWriter();
            fileWriter.Save(_filePath, json);
        }
    }
}
