# SimpleDataGenerator.Json

Nuget: https://www.nuget.org/packages/SimpleDataGenerator.Json/

## Example

~~~

    public class Vehicle
    {
        public string Name { get; set; }
        public int Kilometers { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    //ARRANGE

    var vehicleConf = new EntityConfiguration<Vehicle>();

    vehicleConf.For(x => x.Name).WithLength(5);
    vehicleConf.For(x => x.Kilometers).WithConstValue(100);
    vehicleConf.For(x => x.CreatedOn).InRange(new DateTime(2015, 01, 01, 8, 0, 0), new DateTime(2016, 12, 20, 8, 0, 0));

    var sut = new SimpleJsonDataGenerator();
    sut.WithConfiguration(vehicleConf);

    //ACT

    var result = sut.Generate<Vehicle>();

~~~
