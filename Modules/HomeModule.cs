using Nancy;
using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ =>
      {
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Get["/venues"] = _ =>
      {
      List<Venue> AllVenues = Venue.GetAll();
      return View["venues.cshtml", AllVenues];
      };

      //Create a new band.
      Get["/bands/new"] = _ =>
      {
        return View["bands_form.cshtml"];
      };
      Post["/bands/new"] = _ =>
      {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        return View["success.cshtml"];
      };
      //Create a new venue.
      Get["/venues/new"] = _ =>
      {
        return View["venues_form.cshtml"];
      };
      Post["/venues/new"] = _ =>
      {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["success.cshtml"];
      };

      Get["venues/{id}"] = param =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue SelectedVenue = Venue.Find(param.id);
        List<Band> VenueBands = SelectedVenue.GetBands();
        List<Band> AllBands = Band.GetAll();
        model.Add("venue", SelectedVenue);
        model.Add("venueBands", VenueBands);
        model.Add("allBands", AllBands);
        return View["venue.cshtml", model];
      };

      Get["bands/{id}"] = param =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band SelectedBand = Band.Find(param.id);
        List<Venue> BandVenues = SelectedBand.GetVenues();
        List<Venue> AllVenues = Venue.GetAll();
        model.Add("band", SelectedBand);
        model.Add("bandVenues", BandVenues);
        model.Add("allVenues", AllVenues);
        return View["band.cshtml", model];
      };

      Post["band/add_venue"] = _ =>
      {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        band.AddVenue(venue);
        return View["success.cshtml"];
      };
      Post["venue/add_band"] = _ =>
      {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        venue.AddBand(band);
        return View["success.cshtml"];
      };

      Get["venues/delete/{id}"] = param =>
      {
        Venue SelectedVenue = Venue.Find(param.id);
        return View["venues_delete.cshtml", SelectedVenue];
      };

      Delete["venues/delete/{id}"] = param =>
      {
        Venue SelectedVenue = Venue.Find(param.id);
        SelectedVenue.Delete();
        return View["success.cshtml"];
      };

      Get["/venues/edit/{id}"] = param =>
      {
        Venue SelectedVenue = Venue.Find(param.id);
        return View["venues_edit.cshtml", SelectedVenue];
      };

      Post["/venues/edit/{id}"] = param =>
      {
        Venue SelectedVenue = Venue.Find(param.id);
        return View["success.cshtml", SelectedVenue];
      };

      Patch["/venues/edit/{id}"] = param =>
      {
        Venue SelectedVenue = Venue.Find(param.id);
        SelectedVenue.Update(Request.Form["edit-venue-name"]);
        return View["success.cshtml"];
      };
    }
  }
}
