using EntitiyLayer.Concrete.Entitiy;
using Newtonsoft.Json;
using proje.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace proje
{
    public class RunJobs
    {
        public async void checkCard()
        {




            while (true)
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://api.thingspeak.com/channels/2550812/feeds.json?api_key=VYBAVZGI3BGZLLYX&results=0");
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var getApiResponse = JsonConvert.DeserializeObject<ResponseApi>(await response.Content.ReadAsStringAsync());
                    personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
                    var getDbResponse = db.ApiResponse.Where(i => i.GetApiId == getApiResponse.channel.last_entry_id).ToList();
                    if (getDbResponse.Count == 0)
                    {
                        var dbId = db.ApiResponse.Where(i => true).ToList().OrderByDescending(i => i.GetApiId).FirstOrDefault().GetApiId;
                        var dbNotEntry = getApiResponse.channel.last_entry_id - dbId;

                        var client1 = new HttpClient();
                        var request1 = new HttpRequestMessage(HttpMethod.Get, "https://api.thingspeak.com/channels/2550812/feeds.json?api_key=VYBAVZGI3BGZLLYX&results=" + dbNotEntry);
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

                            var checkCard = db.personel.FirstOrDefault(i => i.kartseriNo == item.field1);
                            db.girisÇıkısBilgisi.Add(new girisÇıkısBilgisi()
                            {
                                personelID = checkCard.personelID,
                                İslemTarihiSaat = DateTime.Now,
                                subeID = checkCard.personel_sube.FirstOrDefault(i => i.personelID == checkCard.personelID).subeID

                            });
                            db.SaveChanges();

                            var personel = db.personel.FirstOrDefault(i => i.kartseriNo == item.field1);
                            if(personel!=null)
                            {

                           
                            var today = DateTime.Today;
                            var transactionsToday = db.girisÇıkısBilgisi.Where(t => t.İslemTarihiSaat.Date == today).OrderBy(t => t.İslemTarihiSaat).ToList();

                            if (transactionsToday.Any())
                            {
                                // İlk ve son kayıtları alır
                                var firstTransaction = transactionsToday.First();
                                var lastTransaction = transactionsToday.Last();

                                // Giriş ve çıkış işlemleri
                                var giris = firstTransaction.İslemTarihiSaat;
                                var cikis = lastTransaction.İslemTarihiSaat;

                                var checkCard2 = db.personel.FirstOrDefault(i => i.kartseriNo == item.field1);
                                db.personelgiriscikis.Add(new personelgiriscikis()
                                {
                                    personelID = checkCard2.personelID,
                                    giris=giris,
                                    cikis=cikis,
                                    subeID = checkCard2.personel_sube.FirstOrDefault(i => i.personelID == checkCard.personelID).subeID

                                });
                                db.SaveChanges();

                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                } 
                Thread.Sleep(1000);
            } 
        }
    }
}