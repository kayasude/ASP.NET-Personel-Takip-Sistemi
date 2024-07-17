using EntitiyLayer.Concrete.Entitiy;
using Newtonsoft.Json;
using PersoenlApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PersoenlApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        public async Task<bool> checkCard(string CardNo)
        {




            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.thingspeak.com/channels/2550812/feeds.json?api_key=VYBAVZGI3BGZLLYX&results=0");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var getApiResponse = JsonConvert.DeserializeObject<ResponseApi>(await response.Content.ReadAsStringAsync());
                personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
                var getDbResponse = db.ApiResponse.Where(i=>i.GetApiId == getApiResponse.channel.last_entry_id).ToList();
                if (getDbResponse.Count == 0 )
                {
                    var dbId = db.ApiResponse.Where(i => true).ToList().OrderByDescending(i=>i.GetApiId).FirstOrDefault().GetApiId;
                    var dbNotEntry = getApiResponse.channel.last_entry_id - dbId;

                    var client1 = new HttpClient();
                    var request1 = new HttpRequestMessage(HttpMethod.Get, "https://api.thingspeak.com/channels/2550812/feeds.json?api_key=VYBAVZGI3BGZLLYX&results="+dbNotEntry);
                    var response1 = await client1.SendAsync(request1);
                    response1.EnsureSuccessStatusCode();
                    var getApiResponse1 = JsonConvert.DeserializeObject<ResponseApi>(await response1.Content.ReadAsStringAsync()); 
                    foreach (var item in getApiResponse1.feeds)
                    {
                        db.ApiResponse.Add(new ApiResponse()
                        {
                            CardNo = item.field1,
                            GetApiId = item.entry_id
                        });
                        db.SaveChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;

            //if (!string.IsNullOrEmpty( CardNo))
            //{
            //    personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            //    var checkCard = db.personel.FirstOrDefault(i => i.kartseriNo == CardNo);
            //    if (checkCard != null)
            //    {
            //        db.girisÇıkısBilgisi.Add(new girisÇıkısBilgisi()
            //        {
            //            personelID = checkCard.personelID,
            //            İslemTarihiSaat = DateTime.Now,
            //            subeID = checkCard.personel_sube.FirstOrDefault(i => i.personelID == checkCard.personelID).subeID

            //        });
            //        db.SaveChanges();
            //        return true;

            //    }
            //    else
            //    {
            //        return false;

            //    }


            //}
            //else
            //{
            //    return false;

            //}
        }
    }
}
