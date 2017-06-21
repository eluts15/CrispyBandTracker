using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string Name, int Id = 0)
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
    //Override Methods
    public override bool Equals(System.Object otherVenue)
    {
      if(!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    //GetAll Method
    public static List<Venue> GetAll()
    {
      List<Venue> AllVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue newVenue = new Venue(name, id);
        AllVenues.Add(newVenue);
      }
      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
       conn.Close();
      }

      return AllVenues;
    }

    //Act on join Table.
    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN venues_bands ON (venues.id = venues_bands.venue_id) JOIN bands ON (venues_bands.band_id = bands.id) WHERE venues.id = @VenueId;", conn);
      SqlParameter venueIdParam = new SqlParameter("@VenueId", this.GetId().ToString());
      cmd.Parameters.Add(venueIdParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> bands = new List<Band>{};

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Band newBand = new Band(name, bandId);
        bands.Add(newBand);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return bands;
    }

    //Adds band's unique id and venues unique id to the join table that was created in the database known as venues_bands.
    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
      SqlParameter bandIdParam = new SqlParameter("@BandId", newBand.GetId());
      SqlParameter venueIdParam = new SqlParameter("@VenueId", this.GetId());
      cmd.Parameters.Add(bandIdParam);
      cmd.Parameters.Add(venueIdParam);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES(@Name);", conn);

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

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @Id;", conn);

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
      Venue foundVenue = new Venue(name, foundId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundVenue;
    }

    public void Update(string updateName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @Name OUTPUT INSERTED.name WHERE id = @VenueId;", conn);

      SqlParameter nameParam = new SqlParameter("@Name", updateName); //Reference to the name column.
      SqlParameter venueIdParam = new SqlParameter("@VenueId", this.GetId());
      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(venueIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM venues_bands WHERE venue_id = @VenueId;", conn);
      SqlParameter venueIdParam = new SqlParameter("@VenueId", this.GetId());
      cmd.Parameters.Add(venueIdParam);
      cmd.ExecuteNonQuery();
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection  conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
