using Infrustructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationServiceAPI.Models;

namespace NotificationServiceAPI.Services
{
    public class NotificationsService
    {
        private readonly IMongoCollection<Template> templateCollection;

        public NotificationsService(IOptions<NotificationDatabaseSettings> templateDatabaseSettings)
        {
            var mongoClient = new MongoClient(templateDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(templateDatabaseSettings.Value.DatabaseName);

            templateCollection = mongoDatabase.GetCollection<Template>(templateDatabaseSettings.Value.NotificationCollectionName);
        }

        public async Task<Template> GetTemplateAsync(string id) =>
            await templateCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateTemplateAsync(Template newTemplate) =>
            await templateCollection.InsertOneAsync(newTemplate);

        public async Task UpdateTemplateAsync(string id, Template updateTemplate) =>
            await templateCollection.ReplaceOneAsync(x => x.Id == id, updateTemplate);

        public async Task RemoveTemplateAsync(string id) =>
            await templateCollection.DeleteOneAsync(x => x.Id == id);
    }
}
