using Infrustructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationServiceAPI.Models;

namespace NotificationServiceAPI.Services
{
    public class TemplatesService
    {
        private readonly IMongoCollection<Template> templateCollection;

        public TemplatesService(IOptions<NotificationDatabaseSettings> templateDatabaseSettings)
        {
            var mongoClient = new MongoClient(templateDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(templateDatabaseSettings.Value.DatabaseName);

            templateCollection = mongoDatabase.GetCollection<Template>(templateDatabaseSettings.Value.TemplateCollectionName);
        }

        public async Task<List<Template>> GetTemplatesAsync() =>
            await templateCollection.Find(_ => true).ToListAsync();

        public async Task<Template?> GetTemplateAsync(string name) =>
            await templateCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task CreateTemplateAsync(Template newTemplate) =>
            await templateCollection.InsertOneAsync(newTemplate);

        public async Task UpdateTemplateAsync(string name, Template updateTemplate) =>
            await templateCollection.ReplaceOneAsync(x => x.Name == name, updateTemplate);

        public async Task RemoveTemplateAsync(string name) =>
            await templateCollection.DeleteOneAsync(x => x.Name == name);
    }
}
