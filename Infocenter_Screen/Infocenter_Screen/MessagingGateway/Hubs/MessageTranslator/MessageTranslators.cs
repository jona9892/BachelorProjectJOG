using DTOModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Infocenter_Screen.Hubs.MessageTranslator
{
    public class MessageTranslators
    {
        public List<Guest> TranslateGuest(byte[] body)
        {
            List<Guest> guests = new List<Guest>();

            String message = Encoding.UTF8.GetString(body);
            guests = JsonConvert.DeserializeObject<List<Guest>>(message);

            return guests;
        }

        public List<Information> TranslateInformation(byte[] body)
        {
            List<Information> informations = new List<Information>();

            String message = Encoding.UTF8.GetString(body);
            informations = JsonConvert.DeserializeObject<List<Information>>(message);

            return informations;
        }

        public string TranslateString(byte[] body)
        {
            string rssfeed = "";

            String message = Encoding.UTF8.GetString(body);
            rssfeed = JsonConvert.DeserializeObject<string>(message);

            return rssfeed;
        }
    }
}