using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using SimpleDataGenerator.Core.Mapping.Implementations;

namespace SimpleDataGenerator.Json.Tests
{
    public class Vehicle
    {
        public string Name { get; set; }
        public int Kilometers { get; set; }
        public DateTime CreatedOn { get; set; }
    }


    [TestFixture]
    public class SimpleJsonDataGeneratorTests
    {
        [Test]
        public void Should_Generate_Json_For_Single_Entity()
        {
            //ARRANGE

            var vehicleConf = new EntityConfiguration<Vehicle>();
            vehicleConf.For(x => x.Name).WithConstValue("TestName");
            vehicleConf.For(x => x.Kilometers).WithConstValue(100);
            vehicleConf.For(x => x.CreatedOn).WithConstValue(new DateTime(2016, 01, 01, 8, 0, 0));
            var sut = new SimpleJsonDataGenerator();
            sut.WithConfiguration(vehicleConf);

            //ACT

            var result = sut.Generate<Vehicle>();
            
            //ASSERT
            var expectedObject = new Vehicle()
            {
                CreatedOn = new DateTime(2016, 01, 01, 8, 0, 0),
                Kilometers = 100,
                Name = "TestName"
            };
            Assert.That(result, Is.EqualTo(JsonConvert.SerializeObject(expectedObject, Formatting.Indented)));
        }

        [Test]
        public void Should_Generate_Json_For_Many_Entities()
        {
            //ARRANGE

            var vehicleConf = new EntityConfiguration<Vehicle>();
            vehicleConf.For(x => x.Name).WithConstValue("TestName");
            vehicleConf.For(x => x.Kilometers).WithConstValue(100);
            vehicleConf.For(x => x.CreatedOn).WithConstValue(new DateTime(2016, 01, 01, 8, 0, 0));
            var sut = new SimpleJsonDataGenerator();
            sut.WithConfiguration(vehicleConf);

            //ACT

            var result = sut.Generate<Vehicle>(numberOfElements: 2);

            //ASSERT
            var expectedObject = new Vehicle()
            {
                CreatedOn = new DateTime(2016, 01, 01, 8, 0, 0),
                Kilometers = 100,
                Name = "TestName"
            };

            Assert.That(result, Is.EqualTo(JsonConvert.SerializeObject(new List<Vehicle>() { expectedObject, expectedObject }, Formatting.Indented)));
        }
    }
}
