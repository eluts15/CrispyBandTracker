using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    //Getters
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public void SetName(string setName)
    {
      this._name = setName;  
    }

    //override Method
    public override bool Equals(System.Object otherBand)
    {
      if(!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
    //    bool idEquality = (this.GetId() == otherBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());
        return (nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    //GetAll Method
    public static List<Band> GetAll()
    {
      List<Band> AllBands = new List<Band>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Band newBand = new Band(name, id);
        AllBands.Add(newBand);
      }
      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
       conn.Close();
      }

      return AllBands;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@Name);", conn);


      SqlParameter nameParam = new SqlParameter("@Name", this.GetName());
      cmd.Parameters.Add(nameParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @Id;", conn);

      SqlParameter idParam = new SqlParameter("@Id", id.ToString());
      cmd.Parameters.Add(idParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string name = null;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      //After locating a specific band on the database side, instantiate an object based off of what is found.
      Band foundBand = new Band(name, foundId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundBand;
    }

    public void Update(string name)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @Name WHERE id = @Id;", conn);

      SqlParameter nameParam = new SqlParameter("@Name", name);
      SqlParameter idParam = new SqlParameter("@Id", this.GetId());
      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(idParam);

      this._name = name;
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    //Act on join table.
    public List<Venue> GetVenues()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN venues_bands ON (bands.id = venues_bands.band_id) JOIN venues ON (venues_bands.venue_id = venues.id) WHERE bands.id = @BandId;", conn);
      SqlParameter bandIdParam = new SqlParameter("@BandId", this.GetId().ToString());
      cmd.Parameters.Add(bandIdParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> venues = new List<Venue>{};

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue newVenue = new Venue(name, venueId);
        venues.Add(newVenue);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return venues;
    }

    //Add band's id and venues id to venues_bands join table.
    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (band_id, venue_id) VALUES (@BandIds, @VenueId);", conn);
      SqlParameter bandIdParam = new SqlParameter("@BandIds", this.GetId());
      SqlParameter venueIdParam = new SqlParameter("@VenueId", newVenue.GetId());
      cmd.Parameters.Add(bandIdParam);
      cmd.Parameters.Add(venueIdParam);
      cmd.ExecuteNonQuery();
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @BandId; DELETE FROM venues_bands WHERE band_id = @BandId;", conn);
      SqlParameter bandIdParam = new SqlParameter("@bandId", this.GetId());
      cmd.Parameters.Add(bandIdParam);
      cmd.ExecuteNonQuery();
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
     cmd.ExecuteNonQuery();
     conn.Close();
    }
  }
}
