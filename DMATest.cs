DMA.DMAInit("DNF.exe");

int x, y, x1 = 0, y1 = 0, i = 0;
var s = new Stopwatch();
while (true)
{
    s.Restart();
    x = DMA.ReadInt(0x14DF7D8C8);
    y = DMA.ReadInt(0x14DF7D8CC);
    s.Stop();
    Console.WriteLine($"{i}  {x} {y}");
    if (x1 != x || y1 != y)
    {
        x1 = x;
        y1 = y;
        //Console.WriteLine($"X:{x1} , Y: {y1} 耗时: {s.Elapsed.TotalMilliseconds:F3} 毫秒");
    }
    i++;
}
