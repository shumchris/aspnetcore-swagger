using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        /// <summary>
        /// Return a list of all the cities and their points of interests.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        /// <summary>
        /// Returns a single city and the points of interests.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

       
    }
}
