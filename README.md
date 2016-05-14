# SimpleDataGenerator.Json 
SimpleDataGenerator.Json is an open source library for .NET, expands https://github.com/FilipPaluch/SimpleDataGenerator. Library allows you to generate Json files based on a predefined model.

Nuget: https://www.nuget.org/packages/SimpleDataGenerator.Json/

## Example

#### Entity
~~~
    public class Vehicle
    {
        public string Name { get; set; }
        public int Kilometers { get; set; }
        public DateTime CreatedOn { get; set; }
    }
~~~

#### Generation
~~~
    //ARRANGE

    var vehicleConf = new EntityConfiguration<Vehicle>();

    vehicleConf.For(x => x.Name).WithLength(5);
    vehicleConf.For(x => x.Kilometers).WithConstValue(100);
    vehicleConf.For(x => x.CreatedOn).InRange(new DateTime(2015, 01, 01, 8, 0, 0), new DateTime(2016, 12, 20, 8, 0, 0));

    var sut = new SimpleJsonDataGenerator();
    sut.WithConfiguration(vehicleConf);

    //ACT

    var result = sut.Generate<Vehicle>(numberOfElements: 3);

~~~

#### Result
~~~
[
  {
    "Name": "34c82",
    "Kilometers": 100,
    "CreatedOn": "2016-10-19T21:00:48.2163632"
  },
  {
    "Name": "86b4d",
    "Kilometers": 100,
    "CreatedOn": "2016-04-13T17:27:27.7718064"
  },
  {
    "Name": "83c6d",
    "Kilometers": 100,
    "CreatedOn": "2016-09-17T09:39:16.9868026"
  }
]
~~~

### Saving to file

Library allows to saving result to file. Only need to extend  configuration of

~~~
sut.WithSavingToFile("FilePath");
~~~


Library can be used without entity configuration, then the data will be generated randomly.
