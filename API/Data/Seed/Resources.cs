using API.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing;
using System;

namespace API.Data.Seed
{
    public class Resources
    {
        public IEnumerable<Game> GameList { get;} = new List<Game>()
        {
            new Game { Id = 1, Price = 25.0m, DateAdded = new DateTime(2022,10,10), Name = "Call Of Duty: Modern Warfare II", ReleaseDate = Convert.ToDateTime("28/10/2022"), PublisherId = 1, Description = "Call of Duty®: Modern Warfare® II drops players into an unprecedented global conflict that features the return of the iconic Operators of Task Force 141."},
            new Game { Id = 2, Price = 23.0m, DateAdded = new DateTime(2022,10,15), Name = "RimWorld", ReleaseDate = Convert.ToDateTime("17/10/2018"), PublisherId = 2, Description = "A sci-fi colony sim driven by an intelligent AI storyteller. Generates stories by simulating psychology, ecology, gunplay, melee combat, climate, biomes, diplomacy, interpersonal relationships, art, medicine, trade, and more."},
            new Game { Id = 3, Price = 27.0m, DateAdded = new DateTime(2022,10,25), Name = "Persona 5 Royal", ReleaseDate = Convert.ToDateTime("21/10/2022"), PublisherId = 3, Description = "Don the mask and join the Phantom Thieves of Hearts as they stage grand heists, infiltrate the minds of the corrupt, and make them change their ways!"},
            new Game { Id = 4, Price = 33.0m, DateAdded = new DateTime(2022,10,28), Name = "Gotham Knights", ReleaseDate = Convert.ToDateTime("21/10/2022"), PublisherId = 4, Description = "Batman is dead. It is now up to the Batman Family - Batgirl, Nightwing, Red Hood, and Robin - to protect Gotham City."},
            new Game { Id = 5, Price = 19.0m, DateAdded = new DateTime(2022,10,11), Name = "FIFA 23", ReleaseDate = Convert.ToDateTime("30/09/2022"), PublisherId = 5, Description = "FIFA 23 brings The World’s Game to the pitch, with HyperMotion2 Technology that delivers even more gameplay realism, men’s and women’s FIFA World Cup™ coming during the season, women’s club teams, cross-play features*, and more."},
            new Game { Id = 6, Price = 21.0m, DateAdded = new DateTime(2022,10,9), Name = "Football Manager 2023", ReleaseDate = Convert.ToDateTime("08/11/2022"), PublisherId = 3, Description = "Upgrade your managerial game with new levels of authenticity and immersion in FM23. Your journey towards footballing glory awaits. Pre-order for 20% off and Early Access*."},
            new Game { Id = 7, Price = 23.35m,DateAdded = new DateTime(2022,10,5), Name = "Red Dead Redeption 2", ReleaseDate = Convert.ToDateTime("05/12/2019"), PublisherId = 6, Description = "Winner of over 175 Game of the Year Awards and recipient of over 250 perfect scores, RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online."},
            new Game { Id = 8, Price = 19.99m, DateAdded = new DateTime(2022,10,4), Name = "Victoria 3", ReleaseDate = Convert.ToDateTime("28/10/2022"), PublisherId = 7, Description = "Paradox Development Studio invites you to build your ideal society in the tumult of the exciting and transformative 19th century. Balance the competing interests in your society and earn your place in the sun in Victoria 3, one of the most anticipated games in Paradox’s history."},
            new Game { Id = 9, Price = 26.5m, DateAdded = new DateTime(2022,10,17), Name = "Rust", ReleaseDate = Convert.ToDateTime("08/02/2018"), PublisherId = 8, Description = "The only aim in Rust is to survive. Everything wants you to die - the island’s wildlife and other inhabitants, the environment, other survivors. Do whatever it takes to last another night."},
            new Game { Id = 10, Price = 23.0m, DateAdded = new DateTime(2022,10,16), Name = "Dead by Daylight", ReleaseDate = Convert.ToDateTime("14/06/2016"), PublisherId = 9, Description = "Dead by Daylight is a multiplayer (4vs1) horror game where one player takes on the role of the savage Killer, and the other four players play as Survivors, trying to escape the Killer and avoid being caught and killed."},
            new Game { Id = 11, Price = 28.0m, DateAdded = new DateTime(2022,10,27), Name = "Forza Horizon 5", ReleaseDate = Convert.ToDateTime("09/11/2021"), PublisherId = 10, Description = "Your Ultimate Horizon Adventure awaits! Explore the vibrant open world landscapes of Mexico with limitless, fun driving action in the world’s greatest cars. Blast off to Hot Wheels Park and experience the most extreme tracks ever devised. Requires Forza Horizon 5 game, expansion sold separately."},
            new Game { Id = 12, Price = 33.0m, DateAdded = new DateTime(2022,10,12), Name = "Cyberpunk 2077", ReleaseDate = Convert.ToDateTime("10/12/2020"), PublisherId = 11, Description = "Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City — a dangerous megalopolis obsessed with power, glamor, and ceaseless body modification."},
            new Game { Id = 13, Price = 35.5m, DateAdded = new DateTime(2022,10,13), Name = "Grounded", ReleaseDate = Convert.ToDateTime("27/09/2022"), PublisherId = 10, Description = "The world is a vast, beautiful and dangerous place – especially when you have been shrunk to the size of an ant. Can you thrive alongside the hordes of giant insects, fighting to survive the perils of the backyard?"},
            new Game { Id = 14, Price = 21.0m, DateAdded = new DateTime(2022,10,15), Name = "New World", ReleaseDate = Convert.ToDateTime("28/09/2021"), PublisherId = 12, Description = "Explore a thrilling, open-world MMO filled with danger and opportunity where you'll forge a new destiny on the supernatural island of Aeternum."},
            new Game { Id = 15, Price = 20.0m, DateAdded = new DateTime(2022,10,18), Name = "Forza Horizon 4", ReleaseDate = Convert.ToDateTime("09/03/2021"), PublisherId = 10, Description = "Dynamic seasons change everything at the world’s greatest automotive festival. Go it alone or team up with others to explore beautiful and historic Britain in a shared open world."},
            new Game { Id = 16, Price = 28.0m, DateAdded = new DateTime(2022,10,22), Name = "NBA 2k23", ReleaseDate = Convert.ToDateTime("08/09/2022"), PublisherId = 13, Description = "Rise to the occasion in NBA 2K23. Showcase your talent in MyCAREER. Pair All-Stars with timeless legends in MyTEAM. Build your own dynasty in MyGM, or guide the NBA in a new direction with MyLEAGUE. Take on NBA or WNBA teams in PLAY NOW and feel true-to-life gameplay."},
            new Game { Id = 17, Price = 20.0m, DateAdded = new DateTime(2022,10,23), Name = "Elden Ring", ReleaseDate = Convert.ToDateTime("25/02/2022"), PublisherId = 14, Description = "THE NEW FANTASY ACTION RPG. Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between."},
            new Game { Id = 18, Price = 19.69m, DateAdded = new DateTime(2022,10,26), Name = "F1 22", ReleaseDate = Convert.ToDateTime("01/07/2022"), PublisherId = 5, Description = "Enter the new era of Formula 1® in EA SPORTS™ F1® 22 the official videogame of the 2022 FIA Formula One World Championship™."},
            new Game { Id = 19, Price = 19.0m, DateAdded = new DateTime(2022,10,19), Name = "The Elder Scrolls Online", ReleaseDate = Convert.ToDateTime("04/04/2014"), PublisherId = 15, Description = "Join over 20 million players in the award-winning online multiplayer RPG and experience limitless adventure in a persistent Elder Scrolls world. Battle, craft, steal, or explore, and combine different types of equipment and abilities to create your own style of play. No game subscription required."},
            new Game { Id = 20, Price = 17.0m, DateAdded = new DateTime(2022,10,19), Name = "Hitman 3", ReleaseDate = Convert.ToDateTime("20/01/2022"), PublisherId = 16, Description = "Death Awaits. Agent 47 returns in HITMAN 3, the dramatic conclusion to the World of Assassination trilogy."}
        };

        public IEnumerable<Publisher> PublisherList { get; } = new List<Publisher>()
        {
            new Publisher { Id = 1, Name = "Activision", Description = "Founded in 1979, Activision continues to disrupt the world of entertainment with its extensive roster of epic blockbuster games -- Pitfall®, Tony Hawk®, Guitar Hero®, Crash Bandicoot™, Skylanders™, Bungie’s Destiny and Call of Duty.® As the leading worldwide developer, publisher and distributor of interactive entertainment and products on consoles, mobile and PC, our “press start” is simple: delight players around the world with innovative, fun, thrilling, and engaging entertainment experiences. Activision is headquartered in Santa Monica, and publishes globally in markets including U.S., Canada, Brazil, Mexico, the United Kingdom, France, Germany, Ireland, Poland, Sweden, Spain, Denmark, the Netherlands, New Zealand, Australia, Singapore, mainland China, Hong Kong and the region of Taiwan."},
            new Publisher { Id = 2, Name = "Ludeon Studios", Description = "Ludeon Studios creates story-oriented simulation games. Ludeon Studios is an independent game developer."},
            new Publisher { Id = 3, Name = "Sega", Description = "Sega Corporation[a] is a Japanese multinational video game and entertainment company headquartered in Shinagawa, Tokyo. Its international branches, Sega of America and Sega Europe, are headquartered in Irvine, California and London, respectively. Its division for the development of both arcade games and home video games, Sega Games, has existed in its current state since 2020; from 2015 to that point, the two had made up separate entities known as Sega Games and Sega Interactive Co., Ltd. Sega is a subsidiary of Sega Sammy Holdings. From 1983 until 2001, Sega also developed video game consoles."},
            new Publisher { Id = 4, Name = "Warner Bros. Games", Description = "Warner Bros. Interactive Entertainment (WBIE; also known as Warner Bros. Games or WB Games) is an American video game publisher based in Burbank, California, and part of the newly-formed Global Streaming and Interactive Entertainment unit of Warner Bros. Discovery.[2] WBIE was founded on January 14, 2004 under Warner Bros. and transferred to the Home Entertainment division when that company was formed in October 2005. WBIE manages the wholly owned game development studios TT Games, Rocksteady Studios, NetherRealm Studios, Monolith Productions, WB Games Boston, Avalanche Software, and WB Games Montréal, among others.[2]"},
            new Publisher { Id = 5, Name = "EA", Description = "Electronic Arts Inc. (EA) is an American video game company headquartered in Redwood City, California. Founded in May 1982 by Apple employee Trip Hawkins, the company was a pioneer of the early home computer game industry and promoted the designers and programmers responsible for its games as \"software artists.\" EA published numerous games and some productivity software for personal computers, all of which were developed by external individuals or groups until 1987's Skate or Die!. The company shifted toward internal game studios, often through acquisitions, such as Distinctive Software becoming EA Canada in 1991."},
            new Publisher { Id = 6, Name = "Rockstar Games", Description = "Rockstar Games, Inc. is an American video game publisher based in New York City. The company was established in December 1998 as a subsidiary of Take-Two Interactive, using the assets Take-Two had previously acquired from BMG Interactive. Founding members of the company were Terry Donovan, Gary Foreman, Dan and Sam Houser, and Jamie King, who worked for Take-Two at the time, and of which the Houser brothers were previously executives at BMG Interactive. Sam Houser heads the studio as president.[3]"},
            new Publisher { Id = 7, Name = "Paradox Interactive", Description = "Paradox Interactive AB is a video game publisher based in Stockholm, Sweden. The company started out as the video game division of Target Games and then Paradox Entertainment (now Cabinet Entertainment) before being spun out into an independent company in 2004. Through a combination of expanding internal studios, founding new studios and purchasing independent developers, the company has grown to comprise nine first-party development studios, including their flagship Paradox Development Studio, and acts as publisher for games from other developers."},
            new Publisher { Id = 8, Name = "Facepunch Studios", Description = "Facepunch Studios Ltd is a British video game developer and publisher headquartered in Walsall, England, founded in June 2004 and incorporated on 17 March 2009 by Garry Newman.[2] The company is most known for its sandbox video game Garry's Mod and survival game Rust.[3] Facepunch is currently developing a successor to Garry's Mod titled s&box.[4]"},
            new Publisher { Id = 9, Name = "Behaviour Interactive", Description = "Behaviour Interactive Inc. is a Canadian video game development studio specializing in the production of 2D and 3D action/adventure games for home video game consoles, handheld game consoles, PCs and mobile. Based in Montreal, the company is the producer and publisher of the action-horror game, Dead by Daylight."},
            new Publisher { Id = 10, Name = "Xbox Game Studios", Description = "Xbox Game Studios (previously known as Microsoft Studios, Microsoft Game Studios, and Microsoft Games) is an American video game publisher and part of Microsoft Gaming division based in Redmond, Washington. It was established in March 2000, spun out from an internal Games Group, for the development and publishing of video games for Microsoft Windows. It has since expanded to include games and other interactive entertainment for the namesake Xbox platforms, Windows Mobile and other mobile platforms, and web-based portals."},
            new Publisher { Id = 11, Name = "CD PROJEKT", Description = "CD Projekt S.A. (Polish: [ˌt͡sɛˈdɛ ˈprɔjɛkt]) is a Polish video game developer, publisher and distributor based in Warsaw, founded in May 1994 by Marcin Iwiński and Michał Kiciński. Iwiński and Kiciński were video game retailers before they founded the company, which initially acted as a distributor of foreign video games for the domestic market. The department responsible for developing original games, CD Projekt Red (stylised as CD PROJEKT RED), best known for The Witcher series, was formed in 2002. In 2008, CD Projekt launched the digital distribution service Good Old Games, now known as GOG.com."},
            new Publisher { Id = 12, Name = "Amazon games", Description = "Amazon Games (formerly Amazon Game Studios) is an American video game company and division of the online retailing company Amazon that primarily focuses on publishing video games developed within the company's development divisions."},
            new Publisher { Id = 13, Name = "2K", Description = "2K is an American video game publisher based in Novato, California. 2K was founded under Take-Two Interactive in January 2005 through the 2K Games and 2K Sports labels, following Take-Two Interactive's acquisition of Visual Concepts that same month. Originally based in New York City, it moved to Novato in 2007. A third label, 2K Play, was added in September 2007. 2K is governed by David Ismailer as president and Phil Dixon as COO. A motion capture studio for 2K is based in Petaluma, California.[1]"},
            new Publisher { Id = 14, Name = "FromSoftware Inc", Description = "FromSoftware, Inc. is a Japanese video game development company founded in November 1986. The company is best known for their Armored Core and Dark Souls series, with Demon's Souls, Bloodborne, Sekiro, and Elden Ring being closely related to the latter."},
            new Publisher { Id = 15, Name = "Bethesda", Description = "Bethesda Softworks LLC is an American video game publisher based in Rockville, Maryland. The company was founded by Christopher Weaver in 1986 as a division of Media Technology Limited, and in 1999 became a subsidiary of ZeniMax Media. In its first fifteen years, it was a video game developer and self-published its titles. In 2001, Bethesda spun off its own in-house development team into Bethesda Game Studios, and Bethesda Softworks retained only its publishing function. In 2021, Microsoft purchased ZeniMax, maintaining that the company will continue to operate as a separate business.[1]"},
            new Publisher { Id = 16, Name = "IO Interactive", Description = "IO Interactive A/S (IOI) is a Danish video game developer based in Copenhagen, best known for creating and developing the Hitman and Kane and Lynch franchises. IO Interactive's most recent game is Hitman 3, which was released in January 2021. The company was founded in September 1998 as a joint venture between the seven-man development team of Reto-Moto and film studio Nordisk Film. IO Interactive was acquired by publisher Eidos Interactive for £23 million in March 2004, which saw itself acquired by Square Enix and renamed as Square Enix Europe in 2009. In May 2017, Square Enix ceased funding for IO Interactive and started seeking a buyer for the studio. IO Interactive performed a management buyout in June 2017, becoming independent and regaining the rights to their Hitman and Freedom Fighters franchises. IO Interactive employs 200 people as of January 2021 and operates two subsidiary studios: IOI Malmö in Malmö, Sweden; and IOI Barcelona in Barcelona, Spain."}
        };

        public IEnumerable<Category> CategoryList { get; } = new List<Category>()
        {
            new Category { Id = 1, Name = "FPS", Description = ""},
            new Category { Id = 2, Name = "Shooter", Description = ""},
            new Category { Id = 3, Name = "Multiplayer", Description = ""},
            new Category { Id = 4, Name = "Military", Description = ""},
            new Category { Id = 5, Name = "Action", Description = ""},
            new Category { Id = 6, Name = "Sim", Description = ""},
            new Category { Id = 7, Name = "Base Building", Description = ""},
            new Category { Id = 8, Name = "Survival", Description = ""},
            new Category { Id = 9, Name = "Strategy", Description = ""},
            new Category { Id = 10, Name = "RPG", Description = ""},
            new Category { Id = 11, Name = "Anime", Description = ""},
            new Category { Id = 12, Name = "Singleplayer", Description = ""},
            new Category { Id = 13, Name = "MOBA", Description = ""},
            new Category { Id = 14, Name = "SuperHero", Description = ""},
            new Category { Id = 15, Name = "Open World", Description = ""},
            new Category { Id = 16, Name = "RPG", Description = ""},
            new Category { Id = 17, Name = "Sports", Description = ""},
            new Category { Id = 18, Name = "Football", Description = ""},
            new Category { Id = 19, Name = "Simulation", Description = ""},
            new Category { Id = 20, Name = "Western", Description = ""},
            new Category { Id = 21, Name = "Economy", Description = ""},
            new Category { Id = 22, Name = "Historical", Description = ""},
            new Category { Id = 23, Name = "Horror", Description = ""},
            new Category { Id = 24, Name = "Racing", Description = ""},
            new Category { Id = 25, Name = "Sci-fi", Description = ""},
            new Category { Id = 26, Name = "MMO", Description = ""},
            new Category { Id = 27, Name = "Basketball", Description = ""},
            new Category { Id = 28, Name = "Fantasy", Description = ""},
            new Category { Id = 29, Name = "Stealth", Description = ""}
        };
        public IEnumerable<GameCategory> GameCategoryList { get; } = new List<GameCategory>()
        {
            new GameCategory { GameId = 1, CategoryId = 1},
            new GameCategory { GameId = 1, CategoryId = 2},
            new GameCategory { GameId = 1, CategoryId = 3},
            new GameCategory { GameId = 1, CategoryId = 4},
            new GameCategory { GameId = 1, CategoryId = 5},
            new GameCategory { GameId = 2, CategoryId = 6},
            new GameCategory { GameId = 2, CategoryId = 7},
            new GameCategory { GameId = 2, CategoryId = 8},
            new GameCategory { GameId = 2, CategoryId = 9},
            new GameCategory { GameId = 3, CategoryId = 10},
            new GameCategory { GameId = 3, CategoryId = 11},
            new GameCategory { GameId = 3, CategoryId = 12},
            new GameCategory { GameId = 4, CategoryId = 5},
            new GameCategory { GameId = 4, CategoryId = 14},
            new GameCategory { GameId = 4, CategoryId = 15},
            new GameCategory { GameId = 4, CategoryId = 16},
            new GameCategory { GameId = 5, CategoryId = 17},
            new GameCategory { GameId = 5, CategoryId = 18},
            new GameCategory { GameId = 6, CategoryId = 17},
            new GameCategory { GameId = 6, CategoryId = 18},
            new GameCategory { GameId = 6, CategoryId = 19},
            new GameCategory { GameId = 7, CategoryId = 2},
            new GameCategory { GameId = 7, CategoryId = 12},
            new GameCategory { GameId = 7, CategoryId = 15},
            new GameCategory { GameId = 7, CategoryId = 16},
            new GameCategory { GameId = 7, CategoryId = 20},
            new GameCategory { GameId = 8, CategoryId = 9},
            new GameCategory { GameId = 8, CategoryId = 21},
            new GameCategory { GameId = 8, CategoryId = 22},
            new GameCategory { GameId = 9, CategoryId = 8},
            new GameCategory { GameId = 9, CategoryId = 3},
            new GameCategory { GameId = 9, CategoryId = 15},
            new GameCategory { GameId = 10, CategoryId = 23},
            new GameCategory { GameId = 10, CategoryId = 3},
            new GameCategory { GameId = 10, CategoryId = 8},
            new GameCategory { GameId = 11, CategoryId = 3},
            new GameCategory { GameId = 11, CategoryId = 17},
            new GameCategory { GameId = 11, CategoryId = 24},
            new GameCategory { GameId = 11, CategoryId = 15},
            new GameCategory { GameId = 12, CategoryId = 25},
            new GameCategory { GameId = 12, CategoryId = 1},
            new GameCategory { GameId = 12, CategoryId = 2},
            new GameCategory { GameId = 12, CategoryId = 5},
            new GameCategory { GameId = 12, CategoryId = 10},
            new GameCategory { GameId = 12, CategoryId = 15},
            new GameCategory { GameId = 12, CategoryId = 12},
            new GameCategory { GameId = 13, CategoryId = 8},
            new GameCategory { GameId = 13, CategoryId = 3},
            new GameCategory { GameId = 13, CategoryId = 7},
            new GameCategory { GameId = 14, CategoryId = 15},
            new GameCategory { GameId = 14, CategoryId = 10},
            new GameCategory { GameId = 14, CategoryId = 26},
            new GameCategory { GameId = 15, CategoryId = 24},
            new GameCategory { GameId = 15, CategoryId = 15},
            new GameCategory { GameId = 15, CategoryId = 3},
            new GameCategory { GameId = 16, CategoryId = 6},
            new GameCategory { GameId = 16, CategoryId = 17},
            new GameCategory { GameId = 16, CategoryId = 27},
            new GameCategory { GameId = 17, CategoryId = 15},
            new GameCategory { GameId = 17, CategoryId = 16},
            new GameCategory { GameId = 17, CategoryId = 5},
            new GameCategory { GameId = 17, CategoryId = 28},
            new GameCategory { GameId = 18, CategoryId = 24},
            new GameCategory { GameId = 18, CategoryId = 17},
            new GameCategory { GameId = 18, CategoryId = 6},
            new GameCategory { GameId = 19, CategoryId = 26},
            new GameCategory { GameId = 19, CategoryId = 10},
            new GameCategory { GameId = 19, CategoryId = 15},
            new GameCategory { GameId = 20, CategoryId = 29},
            new GameCategory { GameId = 20, CategoryId = 5},
            new GameCategory { GameId = 20, CategoryId = 2},
            new GameCategory { GameId = 20, CategoryId = 3}
        }
        .OrderBy(x => x.GameId).ThenBy(x => x.CategoryId);
        public IEnumerable<AppUser> AppUsers { get;} = new List<AppUser>()
        {
            new AppUser {
                UserName = "user1",
                Email = "user1@testmail.com",
                EmailConfirmed = true,
                RefreshToken = new RefreshToken
                {
                    IsRevoked = false,
                    DateAdded = DateTime.UtcNow,
                    DateExpire = DateTime.UtcNow.AddMonths(6),
                    Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString(),
                }
            },
            new AppUser {
                UserName = "user2",
                Email = "user2@testmail.com",
                EmailConfirmed = true,
                RefreshToken = new RefreshToken
                {
                    IsRevoked = false,
                    DateAdded = DateTime.UtcNow,
                    DateExpire = DateTime.UtcNow.AddMonths(6),
                    Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString(),
                }
            },
            new AppUser {
                UserName = "user3",
                Email = "user3@testmail.com",
                EmailConfirmed = true,
                RefreshToken = new RefreshToken
                {
                    IsRevoked = false,
                    DateAdded = DateTime.UtcNow,
                    DateExpire = DateTime.UtcNow.AddMonths(6),
                    Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString(),
                }
            },
        };
    }
}
