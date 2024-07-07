using AllRoadsLeadToRome.Core.MassTransit.Enums;
using Infrustructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationServiceAPI.Models;

namespace NotificationServiceAPI.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Template> templateCollection;

        private readonly IMongoCollection<ReceiverInfo> receiverCollection;

        public MongoDBService(IOptions<NotificationDatabaseSettings> templateDatabaseSettings)
        {
            var mongoClient = new MongoClient(templateDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(templateDatabaseSettings.Value.DatabaseName);

            templateCollection = mongoDatabase.GetCollection<Template>(templateDatabaseSettings.Value.TemplateCollectionName);

            receiverCollection = mongoDatabase.GetCollection<ReceiverInfo>(templateDatabaseSettings.Value.NotificationCollectionName);
        }

        public async Task<List<Template>> GetTemplatesAsync() =>
            await templateCollection.Find(_ => true).ToListAsync();

        public async Task<Template?> GetTemplateAsync(string name, NotificationType notificationType) =>
            await templateCollection.Find(x => x.Name == name & x.TemplateType == notificationType).FirstOrDefaultAsync();

        public async Task CreateTemplateAsync(Template newTemplate) =>
            await templateCollection.InsertOneAsync(newTemplate);

        public async Task UpdateTemplateAsync(string name, NotificationType notificationType, Template updateTemplate) =>
            await templateCollection.ReplaceOneAsync(x => x.Name == name & x.TemplateType == notificationType, updateTemplate);

        public async Task RemoveTemplateAsync(string name, NotificationType notificationType) =>
            await templateCollection.DeleteOneAsync(x => x.Name == name & x.TemplateType == notificationType);

        public async Task CreateNotificationAsync(ReceiverInfo receiverInfo) =>
            await receiverCollection.InsertOneAsync(receiverInfo);


    }
}
