using System;
using System.Net.Http;
using Autofac;
using Core;
using Core.Extensions;
using OEDClient;
using Storage.AzureStorage.Extensions;

namespace WordOrganizerService
{
    public class WordOrganizerServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterNamedCloudTable(Settings.Storage.MainStorageAccount, Settings.Storage.Definitions);
            builder.RegisterNamedCloudTable(Settings.Storage.MainStorageAccount, Settings.Storage.WordReferences);
            builder.RegisterInstance(new HttpClient());
            builder.RegisterSingleInstance<OxfordDictionaryClient>();
            builder.RegisterSingleInstance<WordOrganizerService>();
            base.Load(builder);
        }
    }
}
