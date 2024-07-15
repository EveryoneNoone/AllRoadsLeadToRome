namespace Application
{
    public static class PrepareWorker
    {
        public static string PrepareMessage(string template, Dictionary<string, string> parameters)
        {
            string result = template;
            foreach (var parameter in parameters)
            {
                result = result.Replace("{" + parameter.Key + "}", parameter.Value);
            }

            return result;
        }
    }
}
