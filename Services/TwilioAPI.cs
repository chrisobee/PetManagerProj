using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PetManager.Services
{
    public class TwilioAPI : ITwilioAPIs
    {
        public TwilioAPI()
        {
        }
                
        public void SendSMSReminder(PetOwner petOwner, List<ToDoTask> toDoTasks)
        {
            if (CheckForPhoneNumber(petOwner))
            {
                TwilioClient.Init(API_Key.twilioSID, API_Key.twilioAuthToken);
                string toDoListText = null;
                foreach (ToDoTask task in toDoTasks)
                {
                    toDoListText += $"\n -{task.TaskName}";
                }
                var message = MessageResource.Create(
                    from: new Twilio.Types.PhoneNumber($"{API_Key.twilioPhoneNum}"),
                    body: $"Daily Reminder\nRemaining tasks:\n{toDoListText}",
                    to: new Twilio.Types.PhoneNumber($"{petOwner.PhoneNumber}")
                );
            }
        }

        public bool CheckForPhoneNumber(PetOwner petOwner)
        {
            if(petOwner.PhoneNumber == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
