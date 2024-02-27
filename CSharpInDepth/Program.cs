using CSharpInDepth;

await Quiz.Question1_A_Async();
Console.WriteLine();
await Quiz.Question1_B_Async();

Separator();

try
{
    var result = await Quiz.Question2_A_Async();
    Console.WriteLine(result);
}
catch (Exception e)
{
    Console.WriteLine("Caught by top-level exception handler: " + e);
}

Console.WriteLine();
try
{
    var result = await Quiz.Question2_B_Async();
    Console.WriteLine(result);
}
catch (Exception e)
{
    Console.WriteLine("Caught by top-level exception handler: " + e);
}

Separator();

await Quiz.Question3_A_Async();
Console.WriteLine();
await Quiz.Question3_B_Async();

static void Separator() => Console.WriteLine(new string('-', 80));
