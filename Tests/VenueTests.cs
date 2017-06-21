using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class VenueTests : IDisposable
  {
    public VenueTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=band_tracker_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_VenueEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Override_ObjectsAreEqual()
    {
      //Arrange, Act
      Venue venue1 = new Venue("The House Of Blues");
      Venue venue2 = new Venue("The House Of Blues");

      //Assert
      Assert.Equal(venue1, venue2);
    }

    [Fact]
    public void Test_Save_SaveVenueToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("The Hollywood Bowl");
      testVenue.Save();

      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> testListOfVenues = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testListOfVenues, result);
    }

    [Fact]
    public void Test_Find_FindsVenueInDataBAse()
    {
      //Arrange
      Venue testVenue = new Venue("The Hollywood Bowl, Los Angeles, CA");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Test_Update_UpdatesVenueInDataBase()
    {
      //Arrange
      Venue testVenue = new Venue("The Hollywood Bowl, Los Angeles, CA");
      testVenue.Save();

      //Act
      string newVenue = "The Troubadour, West London, EU";
      testVenue.Update(newVenue);
      string result = testVenue.GetName();

      //Assert
      Assert.Equal(newVenue, result);
    }

    //At this point, it will be necessary to write two tests, and two methods in order for these to pass.
    [Fact]
    public void Test_GetBands_ReturnsAllVenuesBands_ListOfBands()
    {
      //Arrange
      Venue testVenue = new Venue("The Wiltern, Los Angeles, CA");
      testVenue.Save();

      Band testBand1 = new Band("Oh Wonder");
      testBand1.Save();
      Band testBand2 = new Band("Paramore");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand1);
      List<Band> savedBand = testVenue.GetBands();
      List<Band> testListOfBands = new List<Band>{testBand1};

      //Assert
      Assert.Equal(testListOfBands, savedBand);
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {
      //Arrange
      Venue testVenue = new Venue("The Wiltern, Los Angeles, CA");
      testVenue.Save();

      Band testBand1 = new Band("Paramore");
      testBand1.Save();

      Band testBand2 = new Band("Oh Wonder");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand1);
      testVenue.AddBand(testBand2);

      List<Band> listOfBandsResults = testVenue.GetBands();
      List<Band> testListOfBands = new List<Band>{testBand1, testBand2};

      //Assert
      Assert.Equal(testListOfBands, listOfBandsResults);
    }

    [Fact]
    public void Test_DeleteVenueAssocationsFromDataBase_ListOfVenues()
    {
      //Arrange
      Band testBand = new Band("Rage Against The Machine");
      testBand.Save();

      Venue testVenue = new Venue("Madison Square Garden, NY");
      testVenue.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> resultBandVenue = testBand.GetVenues(); //Return a list of Venues and their associated bands.
      List<Venue> testBandVenue = new List<Venue>{};

      //Assert
      Assert.Equal(testBandVenue, resultBandVenue);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
