using Contoso.Events.Data;
using Contoso.Events.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Contoso.Events.Worker
{
    public static class ProcessDocuments
    {
        private static ConnectionManager _connection = new ConnectionManager();
        private static RegistrationContext _registrationsContext = _connection.GetCosmosContext();

        [FunctionName("ProcessDocuments")]
        public static async Task Run(Stream input, string name, Stream output, TraceWriter log)
        {
            log.Info($"Request received to generate sign-in sheet for event: {name}");

            


            log.Info($"Request received to generate sign-in sheet for event: {name}");
        }

        private static async Task<MemoryStream> ProcessStorageMessage(string eventKey)
        {
            SignInDocumentGenerator documentGenerator = new SignInDocumentGenerator();

            using (EventsContext eventsContext = _connection.GetSqlContext())
            {
                await eventsContext.Database.EnsureCreatedAsync();
                await _registrationsContext.ConfigureConnectionAsync();

                Event eventEntry = await eventsContext.Events.SingleOrDefaultAsync(e => e.EventKey == eventKey);

                List<string> registrants = await _registrationsContext.GetRegistrantsForEvent(eventKey);

                MemoryStream stream = documentGenerator.CreateSignInDocument(eventEntry.Title, registrants);
                stream.Seek(0, SeekOrigin.Begin);

                eventEntry.SignInDocumentUrl = $"{eventKey}.docx";
                await eventsContext.SaveChangesAsync();

                return stream;
            }
        }
    }
}