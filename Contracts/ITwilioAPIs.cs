using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface ITwilioAPIs
    {
        public void SendSMSReminder(PetOwner petOwner, List<ToDoTask> toDoTasks);

    }
}
