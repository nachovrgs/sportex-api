using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/playerReview")]
    public class PlayerReviewController : BaseController
    {
        [HttpGet]
        [Route("GetPlayerReviewsSent/{idProfile}")]
        public IActionResult GetPlayerReviewsSent(int idProfile)
        {
            try
            {
                PlayerReviewManager prm = new PlayerReviewManager();
                List<PlayerReview> listReviews = prm.GetReviewsSent(idProfile);
                List<PlayerReviewDTO> listDTOs = new List<PlayerReviewDTO>();
                foreach (PlayerReview rev in listReviews)
                {
                    listDTOs.Add(new PlayerReviewDTO(rev));
                }
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetPlayerReviewsReceived/{idProfile}")]
        public IActionResult GetPlayerReviewsReceived(int idProfile)
        {
            try
            {
                PlayerReviewManager prm = new PlayerReviewManager();
                List<PlayerReview> listReviews = prm.GetReviewsReceived(idProfile);
                List<PlayerReviewDTO> listDTOs = new List<PlayerReviewDTO>();
                foreach (PlayerReview rev in listReviews)
                {
                    listDTOs.Add(new PlayerReviewDTO(rev));
                }
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                PlayerReviewManager prm = new PlayerReviewManager();
                PlayerReview review = prm.GetPlayerReviewById(id);
                return Ok(new PlayerReviewDTO(review));
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("reviewexists")]
        public IActionResult ReviewExists(ReviewAux review)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = false;
                    PlayerReviewManager prm = new PlayerReviewManager();
                    result = prm.ReviewExistsFromProfile(review.IdEvent, review.IdProfileReviews);
                    return Ok(result);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // POST: api/PlayerReview
        [HttpPost]
        public IActionResult Post([FromBody]PlayerReviewDTO reviewDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (reviewDTO != null)
                    {
                        PlayerReview rev = reviewDTO.MapFromDTO();
                        PlayerReviewManager prm = new PlayerReviewManager();
                        prm.InsertPlayerReview(rev);
                        return StatusCode(200);
                    }
                    return StatusCode(400);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("ReviewAllEventParticipants")]
        public IActionResult ReviewAllEventParticipants([FromBody]PlayerReviewDTO reviewDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PlayerReviewManager prm = new PlayerReviewManager();
                    prm.ReviewAllEventParticipants(reviewDTO.EventID, reviewDTO.Rate, reviewDTO.Message, reviewDTO.IdProfileReviews);
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetProfileAverageRating/{idProfile}")]
        public IActionResult GetProfileAverageRating(int idProfile)
        {
            try
            {
                PlayerReviewManager prm = new PlayerReviewManager();
                return Ok(prm.GetProfileAverageRating(idProfile));
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }


        // PUT: api/PlayerReview/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

           
    }

    public class ReviewAux
    {
        public int IdEvent { get; set; }
        public int IdProfileReviews { get; set; }
    }
}
