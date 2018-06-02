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
    [Route("api/PlayerReview")]
    public class PlayerReviewController : BaseController
    {
        [HttpGet]
        [Route("GetPlayerReviewsSent/{idProfile}")]
        public IEnumerable<PlayerReviewDTO> GetPlayerReviewsSent(int idProfile)
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
                return listDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetPlayerReviewsReceived/{idProfile}")]
        public IEnumerable<PlayerReviewDTO> GetPlayerReviewsReceived(int idProfile)
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
                return listDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public PlayerReviewDTO Get(int id)
        {
            try
            {
                PlayerReviewManager prm = new PlayerReviewManager();
                PlayerReview review = prm.GetPlayerReviewById(id);
                return new PlayerReviewDTO(review);
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
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
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetProfileAverageRating/{idProfile}")]
        public double GetProfileAverageRating(int idProfile)
        {
            try
            {
                PlayerReviewManager prm = new PlayerReviewManager();
                return prm.GetProfileAverageRating(idProfile);
            }
            catch (Exception ex)
            {
                throw ex;
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
}
