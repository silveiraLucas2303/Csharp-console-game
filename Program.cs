//fazer equação para largura da box
//aplicar as variáveis de configurações da caixa para a showFood


/*DECLARAÇÕES INICIAS*/
Random random = new Random();

/*CONFIGURAÇÕES INICIAS*/
bool testData = false;
int boxHeight = 29;
int boxWidth = 0;
int boxY = 11; //max = 11

/*Available player and food strings */
string[] states = { "('-')", "(^-^)", "(X_X)" };
string[] foods = { "@@@@@", "$$$$$", "#####" };

// Current player string displayed in the Console
string player = states[0];
//string food = foods[0];

maximizeTerminal();
 InitializeGame();


if(testData)
{
    Console.SetCursorPosition(136 - player.Length, 39);
    Console.Write("12345");


}

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

//imprime area principal do jogo
void printBox()
{
    int y = boxY;
    int h = boxHeight;
    int w = boxWidth;

    string space = new string(' ', 19 + w);
    string line = new string('-', 116 - w * 2);

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

void InitializeGame()
{
    printBox();
    ShowFood();
    Console.SetCursorPosition(20, 12);
    Console.Write(player);
}


void ShowFood()
{
    // Update food to a random index
    int food = random.Next(0, foods.Length);

    // Update food position to a random location inside of the box
    int foodX = random.Next(20, 136 - player.Length); 
    int foodY = random.Next(12, 39);

    // Display the food at the location
    Console.SetCursorPosition(foodX, foodY); //metodo da classe Conosole que configura o local do cursor 
    Console.Write(foods[food]);
}
