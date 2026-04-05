//string names = "Ann, Bob, Alice, Eve, Charlie";
//string players = string.Join(", ", names.Split(',').Select((n, i) => (i+1).ToString() + ". " + n.Trim()));
//Console.WriteLine(players);

//string people = "Jason Puncheon, 9; Jos Hooiveld, 24; Kelvin Davis, 27; Luke Shaw, 19; Gaston Ramirez, 20; Adam Lallana, 10";
//var peopleAges = people.Split(';').Select(p => p).OrderByDescending(p => Int32.Parse(p.Split(',')[1].Trim()));
//Console.WriteLine(string.Join("; ", peopleAges));

string albumTimes = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";
int totalTime = albumTimes.Split(',').Select(t =>
{
    string[] song = t.Split(':');
    return Int32.Parse(song[0]) * 60 + Int32.Parse(song[1]);
}).Sum();
var minutes = totalTime / 60;
var seconds = totalTime % 60;
Console.WriteLine(string.Join(":", minutes, seconds));

string people = "Jason Puncheon, 9; Jos Hooiveld, 24; Kelvin Davis, 27; Luke Shaw, 19; Gaston Ramirez, 20; Adam Lallana, 10";
//var peopleAges = people.Split(';').Select(p => p).OrderByDescending(p => Int32.Parse(p.Split(',')[1].Trim()));
var peopleAges = people
    .Split(';')
    .Select(p => p.Trim())
    .Select(p => p.Split(','))
    .ToDictionary(parts => parts[0].Trim(), parts => int.Parse(parts[1].Trim()));
