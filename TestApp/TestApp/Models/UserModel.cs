
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Models
{
    public class UserModel
    {
        public string pk { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool has_business { get; set; }
        public int business_id { get; set; }
        public string social_token { get; set; }
        public string full_name { get { return $"{first_name} {last_name}";} }
    }


    public class QRResult
    {
        public int bookingStatus { get; set; }
        public string message { get; set; }
        public string booking_id { get; set; }
        public string forename { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public object total { get; set; }
        public List<BookingItem> booking_items { get; set; }

        public string QrUserName { get { return $"{forename} {surname}"; } }
    }

    public class BookingItem
    {
        public int pk { get; set; }
        public string name { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }

    public class BusinessDetail
    {
        public int id { get; set; }
        public string url { get; set; }
        public int user { get; set; }
        public string absolute_url { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string addressGmap { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
        public string business_email { get; set; }
        public string facebookUrl { get; set; }
        public List<Category> categories { get; set; }
        public List<object> activity_set { get; set; }
        public string ImageFullUrl { get { return $"https://dev.spotimist.com{image}"; } }
    }
}
