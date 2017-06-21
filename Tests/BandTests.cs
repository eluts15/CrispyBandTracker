using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class BandTests : IDisposable
  {
    public BandTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Override_ObjectsAreEqual()
    {
      //Arrange, Act
      Band band1 = new Band("Paramore");
      Band band2 = new Band("Paramore");

      //Assert
      Assert.Equal(band1, band2);
    }

    [Fact]
    public void Save_BandSavesToDatabase_BandList()
    {
      //Arrange
      Band testBand = new Band("John Mayer Trio");
      testBand.Save();

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Find_FindsBandInDatabase_Band()
    {
      //Arrange
      Band testBand = new Band("John Mayer Trio");
      testBand.Save();

      //Act
      Band result = Band.Find(testBand.GetId());

      //Assert
      Assert.Equal(testBand, result);
    }

    [Fact]
    public void Test_Update_UpdateBandInDataBase()
    {
      //Arrange
      Band testBand = new Band("The Mars Volta");
      testBand.Save();
      string newBandName = "Good Tiger";

      //Act
      testBand.Update(newBandName); //Simplistic for MVP:(
      string result = testBand.GetName();

      //Assert
      Assert.Equal(newBandName, result);
    }

    [Fact]
    public void GetVenues_ReturnsAllBandsAtThisVenueAsAList()
    {
      //Arrange
      Band testBand = new Band("B.B. King");
      testBand.Save();

      Venue testVenue = new Venue("Harlem's Apollo Theater, NY");
      testVenue.Save();

      //Act
      testBand.AddVenue(testVenue);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testListOfVenues = new List<Venue> {testVenue};

      //Assert
      Assert.Equal(testListOfVenues, result);
    }

    [Fact]
    public void Delete_DeleteBandAssociationsFromDataBase_ListOfBands()
    {
      //Arrange
      Venue testVenue = new Venue("Red Rocks, Morrison, CO");
      testVenue.Save();

      Band testBand = new Band("Little Hurricane");
      testBand.Save();

      //Act
      testBand.AddVenue(testVenue);
      testBand.Delete();

      List<Band> venueBands = testVenue.GetBands();
      List<Band> testVenueBands = new List<Band>{};

      //Assert
      Assert.Equal(testVenueBands, venueBands);
    }



    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
