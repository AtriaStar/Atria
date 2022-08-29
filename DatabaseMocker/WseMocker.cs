using Backend.AspPlugins;
using Models;

namespace DatabaseMocker;

public class WseMocker {
    public static async Task<int> AddWse(AtriaContext context, long uId) {
        WebserviceEntry entry = new WebserviceEntry {
            Name = "Google",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://google.de",
            ViewCount = 1,
            ContactPersonId = uId
        };

        WebserviceEntry entry2 = new WebserviceEntry {
            Name = "Wikipedia",
            ShortDescription = "Encyclopedia",
            FullDescription = "Big Encyclopedia",
            Link = "https://www.wikipedia.de/",
            ViewCount = 10,
            ContactPersonId = uId
        };

        WebserviceEntry entry3 = new WebserviceEntry {
            Name = "LeoOrg",
            ShortDescription = "Translator",
            FullDescription = "Die LEO GmbH bietet Ihnen die bekannten und beliebten Wörterbücher in den " +
                              "Sprachen Englisch ⇔ Deutsch, Französisch ⇔ Deutsch, Spanisch ⇔ Deutsch, Italienisch ⇔ Deutsch, " +
                              "Chinesisch ⇔ Deutsch, Russisch ⇔ Deutsch, Portugiesisch ⇔ Deutsch, Polnisch ⇔ Deutsch, Englisch ⇔ Spanisch, " +
                              "Englisch ⇔ Französisch, Englisch ⇔ Russisch, sowieSpanisch ⇔ Portugiesisch. Des Weiteren können Sie in unseren " +
                              "Foren unsere weltweite Community um Rat fragen, im Trainer Ihre Vokabelkenntnisse auffrischen oder mit unseren " +
                              "Sprachkursen entspannt eine neue Sprache lernen oder verfeinern.",
            Link = "https://www.leo.org/englisch-deutsch",
            ViewCount = 10,
            ContactPersonId = uId
        };

        WebserviceEntry entry4 = new WebserviceEntry {
            Name = "Netflix",
            ShortDescription = "Serien und Filme online ansehen",
            FullDescription = "Netflix, Inc. (von Net, kurz für Internet und flicks als ein im Englischen " +
                              "umgangssprachlicher Ausdruck für ‚Filme‘) ist ein US-amerikanisches Medienunternehmen, das sich " +
                              "mit dem kostenpflichtigen Streaming und der Produktion von Filmen und Serien beschäftigt.",
            Link = "https://www.netflix.com/",
            ViewCount = 10000,
            ContactPersonId = uId
        };

        WebserviceEntry entry5 = new WebserviceEntry {
            Name = "NetflixAgain",
            ShortDescription = "Serien und Filme online ansehen",
            FullDescription = "Netflix, Inc. (von Net, kurz für Internet und flicks als ein im Englischen " +
                              "umgangssprachlicher Ausdruck für ‚Filme‘) ist ein US-amerikanisches Medienunternehmen, das sich " +
                              "mit dem kostenpflichtigen Streaming und der Produktion von Filmen und Serien beschäftigt.",
            Link = "https://www.netflix.com/",
            ViewCount = 10000,
            ContactPersonId = uId
        };


        WebserviceEntry entry6 = new WebserviceEntry {
            Name = "Bing",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://www.bing.com/",
            ViewCount = 1,
            ContactPersonId = uId
        };

        WebserviceEntry entry7 = new WebserviceEntry {
            Name = "ZBing",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://www.bing.com/",
            ViewCount = 1,
            ContactPersonId = uId
        };

        WebserviceEntry entry8 = new WebserviceEntry {
            Name = "DuckDuckGo",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://duckduckgo.com/",
            ViewCount = 1,
            ContactPersonId = uId
        };

        WebserviceEntry entry9 = new WebserviceEntry {
            Name = "Urban Dictionary",
            ShortDescription = "Dictonary",
            FullDescription = "Dictionary for english slang",
            Link = "https://www.urbandictionary.com/",
            ViewCount = 16523,
            ContactPersonId = uId
        };

        WebserviceEntry entry10 = new WebserviceEntry {
            Name = "NewDuckDuckGo",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://duckduckgo.com/",
            ViewCount = 15675,
            ContactPersonId = uId
        };

        WebserviceEntry entry11 = new WebserviceEntry {
            Name = "TwoDuckDuckGo",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://duckduckgo.com/",
            ViewCount = 1145145,
            ContactPersonId = uId
        };

        WebserviceEntry entry12 = new WebserviceEntry {
            Name = "YouTube",
            ShortDescription = "Watch videos",
            FullDescription =
                "Watch videos provided by many users, pay if u don't want to only watch advertisment instead of videos.",
            Link = "https://www.youtube.com/",
            ViewCount = 1145,
            ContactPersonId = uId
        };

        WebserviceEntry entry13 = new WebserviceEntry {
            Name = "Overleaf",
            ShortDescription = "Write LaTeX documents.",
            FullDescription =
                "Write documents for free using LaTeX. Work together with other people on one project at the same time.",
            Link = "https://de.overleaf.com/",
            ViewCount = 1114514,
            ContactPersonId = uId
        };

        WebserviceEntry entry14 = new WebserviceEntry {
            Name = "TwoDuckDuckGo",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://duckduckgo.com/",
            ViewCount = 32441,
            ContactPersonId = uId
        };

        WebserviceEntry entry15 = new WebserviceEntry {
            Name = "ThreeDuckDuckGo",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://duckduckgo.com/",
            ViewCount = 1333,
            ContactPersonId = uId
        };

        WebserviceEntry entry16 = new WebserviceEntry {
            Name = "AnotherDuckDuckGo",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://duckduckgo.com/",
            ViewCount = 33331,
            ContactPersonId = uId
        };
        
        await context.WebserviceEntries.AddAsync(entry);
        await context.WebserviceEntries.AddAsync(entry2);
        await context.WebserviceEntries.AddAsync(entry3);
        await context.WebserviceEntries.AddAsync(entry4);
        await context.WebserviceEntries.AddAsync(entry5);
        await context.WebserviceEntries.AddAsync(entry6);
        await context.WebserviceEntries.AddAsync(entry7);
        await context.WebserviceEntries.AddAsync(entry8);
        await context.WebserviceEntries.AddAsync(entry9);
        await context.WebserviceEntries.AddAsync(entry10);
        await context.WebserviceEntries.AddAsync(entry11);
        await context.WebserviceEntries.AddAsync(entry12);
        await context.WebserviceEntries.AddAsync(entry13);
        await context.WebserviceEntries.AddAsync(entry14);
        await context.WebserviceEntries.AddAsync(entry15);
        await context.WebserviceEntries.AddAsync(entry16);


        return await context.SaveChangesAsync();
    }
}