using Smartstore.Core.Common.Configuration;
using Smartstore.Core.Configuration;
using Smartstore.Core.Security;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Data.Migrations
{
    public class SmartDbContextDataSeeder : IDataSeeder<SmartDbContext>
    {
        public DataSeederStage Stage => DataSeederStage.Early;
        public bool AbortOnFailure => false;

        public async Task SeedAsync(SmartDbContext context, CancellationToken cancelToken = default)
        {
            //await context.MigrateLocaleResourcesAsync(MigrateLocaleResources);
            await MigrateSettingsAsync(context, cancelToken);
        }

        public async Task MigrateSettingsAsync(SmartDbContext context, CancellationToken cancelToken = default)
        {
            await SettingFactory.SaveSettingsAsync(context, new PerformanceSettings(), false);
            await SettingFactory.SaveSettingsAsync(context, new ResiliencySettings(), false);
        }

        public void MigrateLocaleResources(LocaleResourcesBuilder builder)
        {
            

            builder.AddOrUpdate("Admin.Common.RestartHint",
                "Changes to the following settings only take effect after the application has been restarted.",
                "�nderungen an den folgenden Einstellungen werden erst nach einem Neustart der Anwendung wirksam.");

            #region Performance settings

            var prefix = "Admin.Configuration.Settings.Performance";

            builder.AddOrUpdate($"{prefix}", "Performance", "Leistung");
            builder.AddOrUpdate($"{prefix}.Resiliency", "Overload protection", "�berlastungsschutz");
            builder.AddOrUpdate($"{prefix}.Cache", "Cache", "Cache");

            builder.AddOrUpdate($"{prefix}.Hint",
                "For technically experienced users only. Pay attention to the CPU and memory usage when changing these settings.",
                "Nur f�r technisch erfahrene Benutzer. Achten Sie auf die CPU- und Speicherauslastung, wenn Sie diese Einstellungen �ndern.");

            builder.AddOrUpdate($"{prefix}.CacheSegmentSize",
                "Cache segment size", 
                "Cache Segment Gr��e",
                "The number of entries in a single cache segment when greedy loading is disabled. The larger the catalog, the smaller this value should be. We recommend segment size of 500 for catalogs with less than 100.000 items.",
                "Die Anzahl der Eintr�ge in einem einzelnen Cache-Segment, wenn Greedy Loading deaktiviert ist. Je gr��er der Katalog ist, desto kleiner sollte dieser Wert sein. Wir empfehlen eine Segmentgr��e von 500 f�r Kataloge mit weniger als 100.000 Eintr�gen.");

            builder.AddOrUpdate($"{prefix}.AlwaysPrefetchTranslations",
                "Always prefetch translations",
                "�bersetzungen immer vorladen (Prefetch)",
                "By default, only Instant Search prefetches translations. All other product listings work against the segmented cache. For very large multilingual catalogs (> 500,000), enabling this can improve query performance and reduce resource usage.",
                "Standardm��ig werden nur bei der Sofortsuche �bersetzungen vorgeladen. Alle anderen Produktauflistungen arbeiten mit dem segmentierten Cache. Bei sehr gro�en mehrsprachigen Katalogen (> 500.000) kann die Aktivierung dieser Option die Abfrageleistung verbessern und die Ressourcennutzung verringern.");

            builder.AddOrUpdate($"{prefix}.AlwaysPrefetchUrlSlugs",
                "Always prefetch URL slugs",
                "URL Slugs immer vorladen  (Prefetch)",
                "By default, only Instant Search prefetches URL slugs. All other product listings work against the segmented cache. For very large multilingual catalogs (> 500,000), enabling this can improve query performance and reduce resource usage.",
                "Standardm��ig werden nur bei der Sofortsuche URL slugs vorgeladen. Alle anderen Produktauflistungen arbeiten mit dem segmentierten Cache. Bei sehr gro�en mehrsprachigen Katalogen (> 500.000) kann die Aktivierung dieser Option die Abfrageleistung verbessern und die Ressourcennutzung verringern.");

            builder.AddOrUpdate($"{prefix}.MaxUnavailableAttributeCombinations",
                "Max. unavailable attribute combinations",
                "Max. nicht verf�gbare Attributkombinationen",
                "Maximum number of attribute combinations that will be loaded and parsed to make them unavailable for selection on the product detail page.",
                "Maximale Anzahl von Attributkombinationen, die geladen und analysiert werden, um nicht verf�gbare Kombinationen zu ermitteln.");

            builder.AddOrUpdate($"{prefix}.MediaDupeDetectorMaxCacheSize",
                "Media Duplicate Detector max. cache size",
                "Max. Cache-Gr��e f�r Medien-Duplikat-Detektor",
                "Maximum number of MediaFile entities to cache for duplicate file detection. If a media folder contains more files, no caching is done for scalability reasons and the MediaFile entities are loaded directly from the database.",
                "Maximale Anzahl der MediaFile-Entit�ten, die f�r die Duplikat-Erkennung zwischengespeichert werden. Enth�lt ein Medienordner mehr Dateien, erfolgt aus Gr�nden der Skalierbarkeit keine Zwischenspeicherung und die MediaFile-Entit�ten werden direkt aus der Datenbank geladen.");

            prefix = "Admin.Configuration.Settings.Resiliency";

            builder.AddOrUpdate($"{prefix}.LongTraffic", "Traffic limit", "Besucherlimit");
            builder.AddOrUpdate($"{prefix}.LongTrafficNotes", "TBD", "TBD");
            builder.AddOrUpdate($"{prefix}.PeakTraffic", "Peak", "Lastspitzen");
            builder.AddOrUpdate($"{prefix}.PeakTrafficNotes", "TBD", "TBD");

            builder.AddOrUpdate($"{prefix}.TrafficTimeWindow",
                "Time window",
                "Zeitfenster",
                "TBD",
                "TBD");

            builder.AddOrUpdate($"{prefix}.TrafficLimitGuest",
                "Guest limit",
                "G�ste-Grenzwert",
                "TBD",
                "TBD");

            builder.AddOrUpdate($"{prefix}.TrafficLimitBot",
                "Bot limit",
                "Bot-Grenzwert",
                "TBD",
                "TBD");

            builder.AddOrUpdate($"{prefix}.TrafficLimitGlobal",
                "Global limit",
                "Globaler Grenzwert",
                "TBD",
                "TBD");

            builder.AddOrUpdate($"{prefix}.EnableOverloadProtection",
                "Enable overload protection",
                "�berlastungsschutz aktivieren",
                "TBD",
                "TBD");

            builder.AddOrUpdate($"{prefix}.ForbidNewGuestsIfSubRequest",
                "If sub request, forbid \"new\" guests",
                "Wenn Sub-Request, \"neue\" G�ste blockieren",
                "TBD",
                "TBD");

            #endregion
        }
    }
}