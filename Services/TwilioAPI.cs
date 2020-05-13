﻿using PetManager.Contracts;
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
            TwilioClient.Init(API_Key.twilioSID, API_Key.twilioAuthToken);
            string toDoList = "list of tasks here";
            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber($"+{API_Key.twilioPhoneNum}"),
                body: "$Daily Reminder:",
                to: new Twilio.Types.PhoneNumber($"+{petOwner.PhoneNumber}")
            );
        }
    }
}