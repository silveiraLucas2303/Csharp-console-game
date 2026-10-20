//fazer equação para largura da box


//espera até terminal estar maximizado

/*CONFIGURAÇÕES INICIAS*/

int boxHeight = 29;
int boxWidth = 0;
int boxY = 11; //max = 11

maximizeTerminal();
printBox();




Console.ReadKey();

//verifica se o terminal está em tela cheia
bool isTerminalMaximized() 
{
    return 41 == Console.WindowHeight && 156 == Console.WindowWidth;
}

//garante que o jogo começe apenas quando o terminal estiver maximizado
void maximizeTerminal()
{
    int i = 1;

    while (!isTerminalMaximized())
    {

        i++;
        Console.SetCursorPosition(0, 0);
        Console.Write($"\rPara começar o jogo maximize o terminal!");
        Console.SetCursorPosition(0, 1);
        Console.Write("\r" + i);
        Thread.Sleep(500);
    }
    Console.Clear();

}

void printBox()
{
    int y = boxY;
    int h = boxHeight;
    int w = boxWidth;

    string space = new string(' ', 19 + w);
    string line = new string('-', 116 - w *2);

    Console.SetCursorPosition(0, y);
    Console.Write(space);
    Console.Write('+');
    Console.Write(line);
    Console.Write('+');
    Console.Write(space);

    for (int i = 1; i < h; i++)
    {
        Console.SetCursorPosition(19 + w, y + i);
        Console.WriteLine("|");
        Console.SetCursorPosition(136 - w, y + i);
        Console.WriteLine("|");
    }

    Console.SetCursorPosition(0, (y + h));
    Console.Write(space);
    Console.Write('+');
    Console.Write(line);
    Console.Write('+');
    Console.Write(space);
}